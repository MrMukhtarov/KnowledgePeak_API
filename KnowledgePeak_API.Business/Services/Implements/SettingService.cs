using AutoMapper;
using KnowledgePeak_API.Business.Constants;
using KnowledgePeak_API.Business.Dtos.SettingDtos;
using KnowledgePeak_API.Business.Exceptions.Commons;
using KnowledgePeak_API.Business.Exceptions.File;
using KnowledgePeak_API.Business.Exceptions.Setting;
using KnowledgePeak_API.Business.Extensions;
using KnowledgePeak_API.Business.ExternalServices.Interfaces;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using KnowledgePeak_API.DAL.Repositories.Interfaces;
using System;

namespace KnowledgePeak_API.Business.Services.Implements;

public class SettingService : ISettingService
{
    readonly ISettingRepository _repo;
    readonly IMapper _mapper;
    readonly IFileService _fileService;

    public SettingService(ISettingRepository repo, IMapper mapper,
        IFileService fileService)
    {
        _repo = repo;
        _mapper = mapper;
        _fileService = fileService;
    }

    public async Task CreateAsync(SettingCreateDto dto)
    {
        var data = _repo.GetAll();
        if (data.Count() > 0) throw new SettingIsExistException();

        if (dto.HeaderLogoFile != null)
        {
            if (!dto.HeaderLogoFile.IsTypeValid("image")) throw new FileTypeInvalidExveption();
            if (!dto.HeaderLogoFile.IsSizeValid(3)) throw new FileSizeInvalidException();
        }
        if (dto.FooterLogoFile != null)
        {
            if (!dto.FooterLogoFile.IsTypeValid("image")) throw new FileTypeInvalidExveption();
            if (!dto.FooterLogoFile.IsSizeValid(3)) throw new FileSizeInvalidException();
        }

        var setting = _mapper.Map<Setting>(dto);

        if (dto.HeaderLogoFile != null)
        {
            setting.HeaderLogo = await _fileService.UploadAsync(dto.HeaderLogoFile, RootConstants.SettingImageRoot);
        }
        if (dto.FooterLogoFile != null)
        {
            setting.FooterLogo = await _fileService.UploadAsync(dto.FooterLogoFile, RootConstants.SettingImageRoot);
        }

        await _repo.CreateAsync(setting);
        await _repo.SaveAsync();

    }

    public async Task<IEnumerable<SettingDetailDto>> GetAllAsync()
    {
        var data = _repo.GetAll();
        return _mapper.Map<IEnumerable<SettingDetailDto>>(data);
    }

    public async Task UpdateAsync(SettingUpdateDto dto, int id)
    {
        if (id <= 0) throw new IdIsNegativeException<Setting>();
        var entity = await _repo.FIndByIdAsync(id);
        if (entity == null) throw new NotFoundException<Setting>();

        if (dto.HeaderLogoFile != null)
        {
            _fileService.Delete(entity.HeaderLogo);
            if (!dto.HeaderLogoFile.IsTypeValid("image")) throw new FileTypeInvalidExveption();
            if (!dto.HeaderLogoFile.IsSizeValid(3)) throw new FileSizeInvalidException();
            entity.HeaderLogo = await _fileService.UploadAsync(dto.HeaderLogoFile, RootConstants.SettingImageRoot);
        }
        if (dto.FooterLogoFile != null)
        {
            _fileService.Delete(entity.FooterLogo);
            if (!dto.FooterLogoFile.IsTypeValid("image")) throw new FileTypeInvalidExveption();
            if (!dto.FooterLogoFile.IsSizeValid(3)) throw new FileSizeInvalidException();
            entity.FooterLogo = await _fileService.UploadAsync(dto.FooterLogoFile, RootConstants.SettingImageRoot);
        }

        _mapper.Map(dto, entity);

        await _repo.SaveAsync();
    }
}
