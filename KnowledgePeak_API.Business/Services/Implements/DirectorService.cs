﻿using AutoMapper;
using KnowledgePeak_API.Business.Constants;
using KnowledgePeak_API.Business.Dtos.DirectorDtos;
using KnowledgePeak_API.Business.Dtos.RoleDtos;
using KnowledgePeak_API.Business.Dtos.TokenDtos;
using KnowledgePeak_API.Business.Exceptions.Commons;
using KnowledgePeak_API.Business.Exceptions.Director;
using KnowledgePeak_API.Business.Exceptions.File;
using KnowledgePeak_API.Business.Exceptions.Role;
using KnowledgePeak_API.Business.Exceptions.Token;
using KnowledgePeak_API.Business.Extensions;
using KnowledgePeak_API.Business.ExternalServices.Interfaces;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using KnowledgePeak_API.Core.Enums;
using KnowledgePeak_API.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Security.Claims;
using System.Text;

namespace KnowledgePeak_API.Business.Services.Implements;

public class DirectorService : IDirectorService
{
    readonly UserManager<Director> _userManager;
    readonly IMapper _mapper;
    readonly IFileService _fileService;
    readonly IUniversityRepository _uniRepo;
    readonly ITokenService _tokenService;
    readonly RoleManager<IdentityRole> _roleManager;
    readonly IHttpContextAccessor _contextAccessor;
    readonly string userId;

    public DirectorService(UserManager<Director> userManager, IMapper mapper,
        IFileService fileService, IUniversityRepository uniRepo, ITokenService tokenService,
        RoleManager<IdentityRole> roleManager, IHttpContextAccessor contextAccessor)
    {
        _userManager = userManager;
        _mapper = mapper;
        _fileService = fileService;
        _uniRepo = uniRepo;
        _tokenService = tokenService;
        _roleManager = roleManager;
        _contextAccessor = contextAccessor;
        userId = _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
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

        if (dto.ImageFile != null)
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
        if (director == null) throw new UserNotFoundException<Director>();

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

    public async Task<TokenResponseDto> LoginAsync(DIrectorLoginDto dto)
    {
        var director = await _userManager.FindByNameAsync(dto.UserName);
        if (director == null) throw new UserNotFoundException<Director>();

        var result = await _userManager.CheckPasswordAsync(director, dto.Password);
        if (result == false) throw new UserNotFoundException<Director>();


        return _tokenService.CreateDirectorToken(director);
    }

    public async Task AddRole(AddRoleDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.userName);
        if (user == null) throw new UserNotFoundException<AppUser>();

        if (!await _roleManager.RoleExistsAsync(dto.roleName)) throw new NotFoundException<IdentityUser>();

        var result = await _userManager.AddToRoleAsync(user, dto.roleName);
        if (!result.Succeeded) throw new AddRoleFailesException();
    }

    public async Task RemoveRole(RemoveRoleDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.userName);
        if (user == null) throw new UserNotFoundException<AppUser>();

        if (!await _roleManager.RoleExistsAsync(dto.roleName)) throw new NotFoundException<IdentityUser>();

        var result = await _userManager.RemoveFromRoleAsync(user, dto.roleName);
        if (!result.Succeeded) throw new RoleRemoveFailedException();
    }

    public async Task<ICollection<DirectorWithRoles>> GetAllAsync()
    {
        ICollection<DirectorWithRoles> directors = new List<DirectorWithRoles>();
        foreach (var user in await _userManager.Users.ToListAsync())
        {
            var director = new DirectorWithRoles
            {
                Name = user.Name,
                ImageUrl = user.ImageUrl,
                Surname = user.Surname,
                UserName = user.UserName,
                Roles = await _userManager.GetRolesAsync(user),
            };
            directors.Add(director);
        }
        return directors;
    }

    public async Task<TokenResponseDto> LoginWithRefreshTokenAsync(string token)
    {
        if (string.IsNullOrEmpty(token)) throw new ArgumentNullException("token");
        var user = await _userManager.Users.SingleOrDefaultAsync(d => d.RefreshToken == token);
        if (user == null) throw new NotFoundException<AppUser>();

        if (user.RefreshTokenExpiresDate < DateTime.UtcNow.AddHours(4))
            throw new RefreshTokenExpiresDateException();
        return _tokenService.CreateDirectorToken(user);
    }

    public async Task UpdatePrfileAsync(DirectorUpdateDto dto)
    {
        if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException();
        if (!await _userManager.Users.AnyAsync(d => d.Id == userId)) throw new UserNotFoundException<Director>();

        if (dto.ImageFile != null)
        {
            if (!dto.ImageFile.IsSizeValid(3)) throw new FileSizeInvalidException();
            if (!dto.ImageFile.IsTypeValid("image")) throw new FileTypeInvalidExveption();
        }

        var user = await _userManager.FindByIdAsync(userId);

        if (dto.ImageFile != null)
        {
            if (user.ImageUrl != null)
            {
                _fileService.Delete(user.ImageUrl);
            }
            user.ImageUrl = await _fileService.UploadAsync(dto.ImageFile, RootConstants.DirectorImageRoot);
        }

        if (await _userManager.Users.AnyAsync
            (d => (d.UserName == dto.UserName && d.Id != userId) || (d.Email == dto.Email && d.Id != userId)))
            throw new UserExistException();

        _mapper.Map(dto, user);

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded) throw new UserProfileUpdateException();
    }
}
