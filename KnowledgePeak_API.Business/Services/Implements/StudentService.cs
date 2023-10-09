using AutoMapper;
using KnowledgePeak_API.Business.Constants;
using KnowledgePeak_API.Business.Dtos.RoleDtos;
using KnowledgePeak_API.Business.Dtos.StudentDtos;
using KnowledgePeak_API.Business.Dtos.TokenDtos;
using KnowledgePeak_API.Business.Exceptions.Commons;
using KnowledgePeak_API.Business.Exceptions.File;
using KnowledgePeak_API.Business.Exceptions.Role;
using KnowledgePeak_API.Business.Exceptions.Token;
using KnowledgePeak_API.Business.Extensions;
using KnowledgePeak_API.Business.ExternalServices.Interfaces;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using KnowledgePeak_API.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KnowledgePeak_API.Business.Services.Implements;

public class StudentService : IStudentService
{
    readonly UserManager<Student> _userManager;
    readonly UserManager<AppUser> _user;
    readonly IMapper _mapper;
    readonly IFileService _file;
    readonly ITokenService _tokenService;
    readonly IHttpContextAccessor _accessor;
    readonly string _userId;
    readonly RoleManager<IdentityRole> _role;

    public StudentService(UserManager<Student> userManager, UserManager<AppUser> user, IMapper mapper, IFileService file,
        ITokenService tokenService, IHttpContextAccessor accessor, RoleManager<IdentityRole> role, Timer timer)
    {
        _userManager = userManager;
        _user = user;
        _mapper = mapper;
        _file = file;
        _tokenService = tokenService;
        _accessor = accessor;
        _userId = _accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        _role = role;
    }

    public async Task AddRole(AddRoleDto dto)
    {
        if (string.IsNullOrEmpty(dto.userName)) throw new ArgumentNullException();
        var user = await _userManager.FindByNameAsync(dto.userName);
        if (user == null) throw new UserNotFoundException<Student>();

        var role = await _role.FindByNameAsync(dto.roleName);
        if (role == null) throw new NotFoundException<RoleManager<IdentityRole>>();

        var result = await _userManager.AddToRoleAsync(user, dto.roleName);
        if (!result.Succeeded) throw new AddRoleFailesException();
    }

    public async Task CreateAsync(StudentCreateDto dto)
    {
        if (await _user.Users.AnyAsync(u => u.UserName == dto.UserName || u.Email == dto.Email))
            throw new UserExistException();

        var map = _mapper.Map<Student>(dto);

        if (dto.ImageFile != null)
        {
            if (!dto.ImageFile.IsSizeValid(3)) throw new FileSizeInvalidException();
            if (!dto.ImageFile.IsTypeValid("image")) throw new FileTypeInvalidExveption();
            map.ImageUrl = await _file.UploadAsync(dto.ImageFile, RootConstants.StudentImageRoot);
        }
        map.Status = Status.Pending;
        map.StartDate = new DateTime(DateTime.Now.Year, 9, 15);
        map.EndDate = new DateTime(DateTime.Now.Year + 4, 5, 30);

        var result = await _userManager.CreateAsync(map, dto.Password);
        if (!result.Succeeded) throw new RegisterFailedException();
    }

    public Task<StudentListItemDto> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<TokenResponseDto> LoginAsync(StudentLoginDto dto)
    {
        var student = await _userManager.FindByNameAsync(dto.UserName);
        if (student == null) throw new UserNotFoundException<Student>();

        var result = await _userManager.CheckPasswordAsync(student, dto.Password);
        if (result == false) throw new LoginFailedException<Student>();

        return _tokenService.CreateStudentToken(student);
    }

    public async Task<TokenResponseDto> LoginWithRefreshToken(string token)
    {
        if (string.IsNullOrEmpty(token)) throw new ArgumentNullException("token");
        var user = await _userManager.Users.SingleOrDefaultAsync(s => s.RefreshToken == token);
        if (user == null) throw new UserNotFoundException<Student>();
        if (user.RefreshTokenExpiresDate < DateTime.UtcNow.AddHours(4)) throw new RefreshTokenExpiresDateException();
        return _tokenService.CreateStudentToken(user);
    }

}
