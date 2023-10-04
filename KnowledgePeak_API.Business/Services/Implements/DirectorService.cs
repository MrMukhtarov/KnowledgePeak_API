using AutoMapper;
using KnowledgePeak_API.Business.Constants;
using KnowledgePeak_API.Business.Dtos.DirectorDtos;
using KnowledgePeak_API.Business.Exceptions.Commons;
using KnowledgePeak_API.Business.Exceptions.Director;
using KnowledgePeak_API.Business.Exceptions.File;
using KnowledgePeak_API.Business.Extensions;
using KnowledgePeak_API.Business.ExternalServices.Interfaces;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using KnowledgePeak_API.Core.Enums;
using KnowledgePeak_API.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace KnowledgePeak_API.Business.Services.Implements;

public class DirectorService : IDirectorService
{
    readonly UserManager<Director> _userManager;
    readonly IMapper _mapper;
    readonly IFileService _fileService;
    readonly IUniversityRepository _uniRepo;

    public DirectorService(UserManager<Director> userManager, IMapper mapper,
        IFileService fileService, IUniversityRepository uniRepo)
    {
        _userManager = userManager;
        _mapper = mapper;
        _fileService = fileService;
        _uniRepo = uniRepo;
    }

    public async Task CreateAsync(DirectorCreateDto dto)
    {
        var directors = await _userManager.Users.FirstOrDefaultAsync(d => d.IsDeleted == false);
        if (directors != null) throw new ThereIsaDirectorInTheSystemException();

        if (dto.ImageFile != null)
        {
            if (!dto.ImageFile.IsSizeValid(3)) throw new FileSizeInvalidException();
            if (!dto.ImageFile.IsTypeValid("image")) throw new FileTypeInvalidExveption();
        }

        var uni = _uniRepo.GetAll();

        var unid = await uni.FirstOrDefaultAsync();

        var director = _mapper.Map<Director>(dto);

        director.UniversityId = unid.Id;

        if(dto.ImageFile != null)
        {
            director.ImageUrl = await _fileService.UploadAsync(dto.ImageFile, RootConstants.DirectorImageRoot);
        }

        director.Status = Status.Work;

        if (await _userManager.Users.AnyAsync(d => d.UserName == dto.UserName || d.Email == dto.Email))
            throw new UserExistException();

        var result = await _userManager.CreateAsync(director, dto.Password);
        if (!result.Succeeded)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in result.Errors)
            {
                sb.Append(item.Description + " ");
            }
            throw new RegisterFailedException(sb.ToString().TrimEnd());
        }
    }

    public async Task SoftDeleteAsync(string id)
    {
        var director = await _userManager.Users.FirstAsync(d => d.Id == id);
        if(director == null) throw new UserNotFoundException<Director>();

        director.IsDeleted = true;
        director.UniversityId = null;

        await _userManager.UpdateAsync(director);
    }

    public async Task RevertSoftDeleteAsync(string id)
    {
        var director = await _userManager.Users.FirstAsync(d => d.Id == id);
        if (director == null) throw new UserNotFoundException<Director>();

        var directors = await _userManager.Users.FirstOrDefaultAsync(d => d.IsDeleted == false);
        if (directors != null) throw new ThereIsaDirectorInTheSystemException();

        var uni = _uniRepo.GetAll();

        var unid = await uni.FirstOrDefaultAsync();

        director.IsDeleted = false;
        director.UniversityId = unid.Id;

        await _userManager.UpdateAsync(director);
    }
}
