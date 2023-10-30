using AutoMapper;
using KnowledgePeak_API.Business.Constants;
using KnowledgePeak_API.Business.Dtos.AdminDtos;
using KnowledgePeak_API.Business.Dtos.DirectorDtos;
using KnowledgePeak_API.Business.Dtos.RoleDtos;
using KnowledgePeak_API.Business.Dtos.TokenDtos;
using KnowledgePeak_API.Business.Exceptions.Commons;
using KnowledgePeak_API.Business.Exceptions.File;
using KnowledgePeak_API.Business.Exceptions.Role;
using KnowledgePeak_API.Business.Exceptions.Token;
using KnowledgePeak_API.Business.Extensions;
using KnowledgePeak_API.Business.ExternalServices.Interfaces;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace KnowledgePeak_API.Business.Services.Implements;

public class AdminService : IAdminService
{
    readonly UserManager<Admin> _userManager;
    readonly UserManager<AppUser> _user;
    readonly IMapper _mapper;
    readonly IFileService _file;
    readonly SignInManager<Admin> _signinManager;
    readonly ITokenService _tokenService;
    readonly string? _userId;
    readonly IHttpContextAccessor _accessor;
    readonly RoleManager<IdentityRole> _roleManager;
    readonly IConfiguration _config;

    public AdminService(UserManager<Admin> userManager, IMapper mapper, IFileService file, ITokenService tokenService,
        SignInManager<Admin> signinManager, IHttpContextAccessor accessor, UserManager<AppUser> user, RoleManager<IdentityRole> roleManager, IConfiguration config)
    {
        _userManager = userManager;
        _mapper = mapper;
        _file = file;
        _tokenService = tokenService;
        _signinManager = signinManager;
        _accessor = accessor;
        _userId = _accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        _user = user;
        _roleManager = roleManager;
        _config = config;
    }

    public async Task<ICollection<AdminListItemDto>> GetAllAsync()
    {
        ICollection<AdminListItemDto> admins = new List<AdminListItemDto>();
        foreach (var user in await _userManager.Users.ToListAsync())
        {
            var admin = new AdminListItemDto
            {
                Name = user.Name,
                ImageFile = _config["Jwt:Issuer"] + "wwwroot/" + user.ImageUrl,
                Surname = user.Surname,
                UserName = user.UserName,
                Roles = await _userManager.GetRolesAsync(user),
                Age = user.Age,
                Email = user.Email,
                Gender = user.Gender
            };
            admins.Add(admin);
        }
        return admins;
    }

    public async Task CreateAsync(AdminCreateDto dto)
    {
        if (await _user.Users.AnyAsync(u => u.Email == dto.Email && u.UserName == dto.UserName))
            throw new UserExistException();

        var map = _mapper.Map<Admin>(dto);
        if (dto.ImageFile != null)
        {
            if (!dto.ImageFile.IsSizeValid(3)) throw new FileSizeInvalidException();
            if (!dto.ImageFile.IsTypeValid("image")) throw new FileTypeInvalidExveption();
            map.ImageUrl = await _file.UploadAsync(dto.ImageFile, RootConstants.AdminImageRoot);
        }
        var result = await _userManager.CreateAsync(map, dto.Password);
        if (!result.Succeeded) throw new RegisterFailedException();
    }

    public async Task DeleteAsync(string userName)
    {
        if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException();
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null) throw new UserNotFoundException<Admin>();
        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded) throw new UserDeleteProblemException();
    }

    public async Task<AdminDetailDto> GetByIdAsync(string id)
    {
        AdminDetailDto dr = new AdminDetailDto();
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) throw new UserNotFoundException<Admin>();
        dr = new AdminDetailDto
        {
            ImageFile = _config["Jwt:Issuer"] + "wwwroot/" + user.ImageUrl,
            UserName = user.UserName,
            Name = user.Name,
            Surname = user.Surname,
            Roles = await _userManager.GetRolesAsync(user),
            Gender = user.Gender,
            Email = user.Email,
            Age = user.Age
        };
        return dr;
    }

    public async Task<TokenResponseDto> LoginAsync(AdminLoginDto dto)
    {
        var admin = await _userManager.FindByNameAsync(dto.UserName);
        if (admin == null) throw new UserNotFoundException<Admin>();

        var result = await _userManager.CheckPasswordAsync(admin, dto.Password);
        if (result == false) throw new LoginFailedException<Admin>();

        return _tokenService.CreateAdminToken(admin);
    }

    public async Task<TokenResponseDto> LoginWithRefreshTokenAsync(string token)
    {
        if (string.IsNullOrEmpty(token)) throw new ArgumentNullException("token");
        var user = await _userManager.Users.SingleOrDefaultAsync(d => d.RefreshToken == token);
        if (user == null) throw new NotFoundException<Admin>();

        if (user.RefreshTokenExpiresDate < DateTime.UtcNow.AddHours(4))
            throw new RefreshTokenExpiresDateException();
        return _tokenService.CreateAdminToken(user);
    }

    public async Task SignOut()
    {
        await _signinManager.SignOutAsync();
        var user = await _userManager.FindByIdAsync(_userId);
        if (user == null) throw new UserNotFoundException<Admin>();
        user.RefreshToken = null;
        user.RefreshTokenExpiresDate = null;
        var res = await _userManager.UpdateAsync(user);
        if (!res.Succeeded) throw new SIgnOutInvalidException();
    }

    public async Task UpdateAsync(AdminUpdateDto dto)
    {
        if (string.IsNullOrEmpty(_userId)) throw new ArgumentNullException();
        var user = await _userManager.FindByIdAsync(_userId);
        if (user == null) throw new UserNotFoundException<Admin>();
        var map = _mapper.Map(dto, user);
        if (dto.ImageFile != null)
        {
            if (user.ImageUrl != null)
                _file.Delete(user.ImageUrl);
            if (!dto.ImageFile.IsSizeValid(3)) throw new FileSizeInvalidException();
            if (!dto.ImageFile.IsTypeValid("image")) throw new FileTypeInvalidExveption();
            map.ImageUrl = await _file.UploadAsync(dto.ImageFile, RootConstants.AdminImageRoot);
        }
        if (await _user.Users.AnyAsync(u => (u.UserName == dto.UserName && u.Id != _userId) || (u.Email == dto.Email
        && u.Id != _userId))) throw new UserExistException();
        var result = await _userManager.UpdateAsync(map);
        if (!result.Succeeded) throw new UserProfileUpdateException();
    }

    public async Task UpdateProfileAdminAsync(string userName, AdminUpdateDto dto)
    {
        var admin = await _userManager.FindByNameAsync(userName);
        if (admin == null) throw new UserNotFoundException<Admin>();

        if (await _userManager.Users.AnyAsync
          (d => (d.UserName == dto.UserName && d.Id != admin.Id) || (d.Email == dto.Email && d.Id != admin.Id)))
            throw new UserExistException();

        var map = _mapper.Map(dto, admin);

        if (dto.ImageFile != null)
        {
            if (admin.ImageUrl != null)
                _file.Delete(admin.ImageUrl);
            if (!dto.ImageFile.IsSizeValid(3)) throw new FileSizeInvalidException();
            if (!dto.ImageFile.IsTypeValid("image")) throw new FileTypeInvalidExveption();
            map.ImageUrl = await _file.UploadAsync(dto.ImageFile, RootConstants.AdminImageRoot);
        }

        var result = await _userManager.UpdateAsync(admin);
        if (!result.Succeeded) throw new UserProfileUpdateException();
    }

    public async Task AddRole(AddRoleDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.userName);
        if (user == null) throw new UserNotFoundException<Admin>();

        if (!await _roleManager.RoleExistsAsync(dto.roleName)) throw new NotFoundException<IdentityRole>();

        var result = await _userManager.AddToRoleAsync(user, dto.roleName);
        if (!result.Succeeded) throw new AddRoleFailesException();
    }

    public async Task RemoveRole(RemoveRoleDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.userName);
        if (user == null) throw new UserNotFoundException<Admin>();

        if (!await _roleManager.RoleExistsAsync(dto.roleName)) throw new NotFoundException<IdentityUser>();

        var result = await _userManager.RemoveFromRoleAsync(user, dto.roleName);
        if (!result.Succeeded) throw new RoleRemoveFailedException();
    }
}
