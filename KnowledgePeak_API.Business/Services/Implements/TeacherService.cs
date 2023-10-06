using AutoMapper;
using KnowledgePeak_API.Business.Constants;
using KnowledgePeak_API.Business.Dtos.TeacherDtos;
using KnowledgePeak_API.Business.Exceptions.Commons;
using KnowledgePeak_API.Business.Exceptions.File;
using KnowledgePeak_API.Business.Extensions;
using KnowledgePeak_API.Business.ExternalServices.Interfaces;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using KnowledgePeak_API.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KnowledgePeak_API.Business.Services.Implements;

public class TeacherService : ITeacherService
{
    readonly IMapper _mapper;
    readonly IFileService _file;
    readonly UserManager<AppUser> _user;
    readonly UserManager<Teacher> _userManager;

    public TeacherService(IMapper mapper, UserManager<Teacher> userManager,
        IFileService file, UserManager<AppUser> user)
    {
        _mapper = mapper;
        _user = user;
        _userManager = userManager;
        _file = file;
    }

    public async Task CreateAsync(TeacherCreateDto dto)
    {
        var teacher = _mapper.Map<Teacher>(dto);

        if (dto.ImageFile != null)
        {
            if (!dto.ImageFile.IsTypeValid("image")) throw new FileTypeInvalidExveption();
            if (!dto.ImageFile.IsSizeValid(3)) throw new FileSizeInvalidException();

            teacher.ImageUrl = await _file.UploadAsync(dto.ImageFile, RootConstants.TeacherImageRoot);
        }

        teacher.Status = Status.Work;

        if (await _user.Users.AnyAsync(u => u.Email == dto.Email || u.UserName == dto.UserName))
            throw new UserExistException();

        var result = await _userManager.CreateAsync(teacher,dto.Password);
        if (!result.Succeeded) throw new RegisterFailedException();
    }
}
