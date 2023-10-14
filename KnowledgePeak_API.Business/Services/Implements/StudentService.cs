﻿using AutoMapper;
using KnowledgePeak_API.Business.Constants;
using KnowledgePeak_API.Business.Dtos.GroupDtos;
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
    readonly SignInManager<Student> _signinManager;

    public StudentService(UserManager<Student> userManager, UserManager<AppUser> user, IMapper mapper, IFileService file,
        ITokenService tokenService, IHttpContextAccessor accessor, RoleManager<IdentityRole> role,
        SignInManager<Student> signinManager)
    {
        _userManager = userManager;
        _user = user;
        _mapper = mapper;
        _file = file;
        _tokenService = tokenService;
        _accessor = accessor;
        _userId = _accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        _role = role;
        _signinManager = signinManager;
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
        //map.StartDate = new DateTime(DateTime.Now.Year, 9, 15);
        ////map.EndDate = new DateTime(DateTime.Now.Year + 4, 5, 30);
        map.StartDate = DateTime.Now;
        map.EndDate = DateTime.Now.AddMinutes(1);
        map.IsDeleted = false;

        var result = await _userManager.CreateAsync(map, dto.Password);
        if (!result.Succeeded) throw new RegisterFailedException();
    }

    public async Task<ICollection<StudentListItemDto>> GetAll(bool takeAll)
    {
        ICollection<StudentListItemDto> students = new List<StudentListItemDto>();
        if (takeAll)
        {
            foreach (var item in await _userManager.Users.Include(s => s.Group).ToListAsync())
            {
                var stu = new StudentListItemDto
                {
                    Name = item.Name,
                    SurName = item.Surname,
                    UserName = item.UserName,
                    ImageUrl = item.ImageUrl,
                    Email = item.Email,
                    Gender = item.Gender,
                    Status = item.Status,
                    Age = item.Age,
                    Avarage = item.Avarage,
                    Course = item.Course,
                    Roles = await _userManager.GetRolesAsync(item),
                    Group = _mapper.Map<GroupSingleDetailDto>(item.Group)
                };
                students.Add(stu);
            }
        }
        else
        {
            foreach (var item in await _userManager.Users.Include(s => s.Group).Where(s => s.IsDeleted == false).ToListAsync())
            {
                var stu = new StudentListItemDto
                {
                    Name = item.Name,
                    SurName = item.Surname,
                    UserName = item.UserName,
                    ImageUrl = item.ImageUrl,
                    Email = item.Email,
                    Gender = item.Gender,
                    Status = item.Status,
                    Age = item.Age,
                    Avarage = item.Avarage,
                    Course = item.Course,
                    Roles = await _userManager.GetRolesAsync(item),
                    Group = _mapper.Map<GroupSingleDetailDto>(item.Group)
                };
                students.Add(stu);
            }
        }
        return students;
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

    public async Task RemoveRole(RemoveRoleDto dto)
    {
        if (string.IsNullOrEmpty(dto.userName)) throw new ArgumentNullException();
        var user = await _userManager.FindByNameAsync(dto.userName);
        if (user == null) throw new UserNotFoundException<Student>();

        var role = await _role.FindByNameAsync(dto.roleName);
        if (role == null) throw new NotFoundException<RoleManager<IdentityRole>>();

        var result = await _userManager.RemoveFromRoleAsync(user, dto.roleName);
        if (!result.Succeeded) throw new RoleRemoveFailedException();
    }

    public async Task UpdateAsync(StudentUpdateDto dto)
    {
        if (string.IsNullOrEmpty(_userId)) throw new ArgumentNullException();
        if (!await _userManager.Users.AnyAsync(u => u.Id == _userId)) throw new UserNotFoundException<Student>();
        var user = await _userManager.FindByNameAsync(dto.UserName);
        if (user == null) throw new UserNotFoundException<Student>();

        if (dto.ImageFile != null)
        {
            if (!dto.ImageFile.IsTypeValid("image")) throw new FileTypeInvalidExveption();
            if (!dto.ImageFile.IsSizeValid(3)) throw new FileSizeInvalidException();

            if (user.ImageUrl != null)
                _file.Delete(user.ImageUrl);
            user.ImageUrl = await _file.UploadAsync(dto.ImageFile, RootConstants.StudentImageRoot);
        }

        if (await _user.Users.AnyAsync(u => (u.UserName == dto.UserName && u.Id != user.Id) || (u.Email == dto.Email && u.Id !=
        user.Id))) throw new UserExistException();

        _mapper.Map(dto, user);
        var res = await _userManager.UpdateAsync(user);
        if (!res.Succeeded) throw new UserProfileUpdateException();
    }

    public async Task RevertSoftDeleteAsync(string userId)
    {
        if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException();
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) throw new UserNotFoundException<Student>();
        user.IsDeleted = false;
        user.Status = Status.Student;
        var res = await _userManager.UpdateAsync(user);
        if (!res.Succeeded) throw new RevertSoftDeleteInvalidException<Student>();
    }

    public async Task SoftDeleteAsync(string userId)
    {
        if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException();
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) throw new UserNotFoundException<Student>();
        user.IsDeleted = true;
        user.Status = Status.Fired;
        var res = await _userManager.UpdateAsync(user);
        if (!res.Succeeded) throw new SoftDeleteInvalidException<Student>();
    }

    public async Task UpdatPrfileFromAdmin(string userNeme, StudentAdminUpdateDto dto)
    {
        if (string.IsNullOrEmpty(userNeme)) throw new ArgumentNullException();
        var user = await _userManager.FindByNameAsync(userNeme);
        if (user == null) throw new UserNotFoundException<Student>();

        if (dto.ImageFile != null)
        {
            if (!dto.ImageFile.IsTypeValid("image")) throw new FileTypeInvalidExveption();
            if (!dto.ImageFile.IsSizeValid(3)) throw new FileSizeInvalidException();
            if (user.ImageUrl != null)
                _file.Delete(user.ImageUrl);
            user.ImageUrl = await _file.UploadAsync(dto.ImageFile, RootConstants.StudentImageRoot);
        }

        if (dto.Status == Status.Fired)
            await SoftDeleteAsync(user.Id);
        if (dto.Status == Status.Student)
            await RevertSoftDeleteAsync(user.Id);

        if (await _user.Users.AnyAsync(u => (u.UserName == dto.UserName && u.Id != user.Id) || (dto.Email == u.Email &&
        u.Id != user.Id))) throw new UserExistException();

        _mapper.Map(dto, user);
        var res = await _userManager.UpdateAsync(user);
        if (!res.Succeeded) throw new UserProfileUpdateException();
    }

    public async Task DeleteAsync(string userName)
    {
        if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException();
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null) throw new UserNotFoundException<Student>();

        if (user.ImageUrl != null)
            _file.Delete(user.ImageUrl);

        var res = await _userManager.DeleteAsync(user);
        if (!res.Succeeded) throw new UserDeleteProblemException();
    }

    public async Task SignOut()
    {
        await _signinManager.SignOutAsync();
        var user = await _userManager.FindByIdAsync(_userId);
        if (user == null) throw new UserNotFoundException<Student>();
        user.RefreshToken = null;
        user.RefreshTokenExpiresDate = null;
        var res = await _userManager.UpdateAsync(user);
        if (!res.Succeeded) throw new SIgnOutInvalidException();
    }

    public async Task<StudentDetailDto> GetByIdAsync(string id, bool takeAll)
    {
        StudentDetailDto student = new StudentDetailDto();
        if (takeAll)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new UserNotFoundException<Student>();
            student = new StudentDetailDto
            {
                Age = user.Age,
                Avarage = user.Avarage,
                Course = user.Course,
                Email = user.Email,
                Gender = user.Gender,
                ImageUrl = user.ImageUrl,
                Name = user.Name,
                Status = user.Status,
                SurName = user.Surname,
                UserName = user.UserName
            };
        }
        else
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == id && u.IsDeleted == false);
            if (user == null) throw new UserNotFoundException<Student>();
            student = new StudentDetailDto
            {
                Age = user.Age,
                Avarage = user.Avarage,
                Course = user.Course,
                Email = user.Email,
                Gender = user.Gender,
                ImageUrl = user.ImageUrl,
                Name = user.Name,
                Status = user.Status,
                SurName = user.Surname,
                UserName = user.UserName
            };
        }
        return student;
    }
}
