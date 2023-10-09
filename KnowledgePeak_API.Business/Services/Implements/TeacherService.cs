 using AutoMapper;
using KnowledgePeak_API.Business.Constants;
using KnowledgePeak_API.Business.Dtos.FacultyDtos;
using KnowledgePeak_API.Business.Dtos.LessonDtos;
using KnowledgePeak_API.Business.Dtos.RoleDtos;
using KnowledgePeak_API.Business.Dtos.SpecialityDtos;
using KnowledgePeak_API.Business.Dtos.TeacherDtos;
using KnowledgePeak_API.Business.Dtos.TokenDtos;
using KnowledgePeak_API.Business.Exceptions.Commons;
using KnowledgePeak_API.Business.Exceptions.File;
using KnowledgePeak_API.Business.Exceptions.Role;
using KnowledgePeak_API.Business.Exceptions.Teacher;
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
using System.Security.Claims;

namespace KnowledgePeak_API.Business.Services.Implements;

public class TeacherService : ITeacherService
{
    readonly IMapper _mapper;
    readonly IFileService _file;
    readonly UserManager<AppUser> _user;
    readonly UserManager<Teacher> _userManager;
    readonly IFacultyRepository _faculty;
    readonly ISpecialityRepository _speciality;
    readonly ILessonRepository _lesson;
    readonly ITokenService _token;
    readonly RoleManager<IdentityRole> _roleManager;
    readonly IHttpContextAccessor _accessor;
    readonly string userId;

    public TeacherService(IMapper mapper, UserManager<Teacher> userManager,
        IFileService file, UserManager<AppUser> user, IFacultyRepository faculty,
        ISpecialityRepository speciality, ILessonRepository lesson, ITokenService token,
        RoleManager<IdentityRole> roleManager, IHttpContextAccessor accessor)
    {
        _mapper = mapper;
        _user = user;
        _userManager = userManager;
        _file = file;
        _faculty = faculty;
        _speciality = speciality;
        _lesson = lesson;
        _token = token;
        _roleManager = roleManager;
        _accessor = accessor;
        userId = _accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
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

        var result = await _userManager.CreateAsync(teacher, dto.Password);
        if (!result.Succeeded) throw new RegisterFailedException();
    }

    public async Task AddFaculty(TeacherAddFacultyDto dto, string userName)
    {
        if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentNullException();

        var teacher = await _userManager.Users.Include(u => u.TeacherFaculties)
            .FirstOrDefaultAsync(u => u.UserName == userName);
        if (teacher == null) throw new UserNotFoundException<Teacher>();

        if (dto.FacultyIds != null)
        {
            foreach (var id in dto.FacultyIds)
            {
                var faculty = await _faculty.FIndByIdAsync(id);
                if (faculty == null) throw new NotFoundException<Faculty>();

                foreach (var item in teacher.TeacherFaculties)
                {
                    if (id == item.FacultyId) throw new IsExistException();
                }
                teacher.TeacherFaculties.Add(new TeacherFaculty { FacultyId = id });
            }
        }
        _mapper.Map<Teacher>(teacher);
        var result = await _userManager.UpdateAsync(teacher);
        if (!result.Succeeded) throw new TeacherAddRelationProblemException<Faculty>();
    }

    public async Task AddSpeciality(TeacherAddSpecialitiyDto dto, string userName)
    {
        if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentNullException(nameof(userName));
        var teacher = await _userManager.Users.Include(t => t.TeacherSpecialities)
            .FirstOrDefaultAsync(t => t.UserName == userName);
        if (teacher == null) throw new UserNotFoundException<Teacher>();

        if (dto.TeacherSpecialities != null)
        {
            foreach (var id in dto.TeacherSpecialities)
            {
                var special = await _speciality.FIndByIdAsync(id);
                if (special == null) throw new NotFoundException<Speciality>();

                foreach (var item in teacher.TeacherSpecialities)
                {
                    if (id == item.SpecialityId) throw new IsExistException("Speciality");
                }
                teacher.TeacherSpecialities.Add(new TeacherSpeciality { SpecialityId = id });
            }
        }
        _mapper.Map<Teacher>(teacher);
        var result = await _userManager.UpdateAsync(teacher);
        if (!result.Succeeded) throw new TeacherAddRelationProblemException<Speciality>();
    }

    public async Task AddLesson(TeacherAddLessonDto dto, string userName)
    {
        if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentNullException(nameof(userName));
        var teacher = await _userManager.Users.Include(t => t.TeacherLessons)
            .FirstOrDefaultAsync(t => t.UserName == userName);
        if (teacher == null) throw new UserNotFoundException<Teacher>();

        if (dto.LessonIds != null)
        {
            foreach (var id in dto.LessonIds)
            {
                var lesson = await _lesson.FIndByIdAsync(id);
                if (lesson == null) throw new NotFoundException<Lesson>();

                foreach (var item in teacher.TeacherLessons)
                {
                    if (item.LessonId == id) throw new IsExistException("Lesson");
                }
                teacher.TeacherLessons.Add(new TeacherLesson { LessonId = id });
            }
        }
        _mapper.Map<Teacher>(teacher);
        var result = await _userManager.UpdateAsync(teacher);
        if (!result.Succeeded) throw new TeacherAddRelationProblemException<Lesson>();
    }

    public async Task<TokenResponseDto> Login(TeacherLoginDto dto)
    {
        var teacher = await _userManager.FindByNameAsync(dto.UserName);
        if (teacher == null) throw new UserNotFoundException<Teacher>();

        var password = await _userManager.CheckPasswordAsync(teacher, dto.Password);
        if (password == false) throw new LoginFailedException<Teacher>();

        return _token.CreateTeacherToken(teacher);
    }

    public async Task AddRoleAsync(AddRoleDto dto)
    {
        var teacher = await _userManager.FindByNameAsync(dto.userName);
        if (teacher == null) throw new UserNotFoundException<Teacher>();

        var roleExits = await _roleManager.RoleExistsAsync(dto.roleName);
        if (roleExits == false) throw new NotFoundException<IdentityRole>();

        var result = await _userManager.AddToRoleAsync(teacher, dto.roleName);
        if (!result.Succeeded) throw new AddRoleFailesException();
    }

    public async Task<ICollection<TeacherListItemDto>> GetAllAsync(bool takeAll)
    {
        ICollection<TeacherListItemDto> teachers = new List<TeacherListItemDto>();
        if (takeAll)
        {
            foreach (var user in await _userManager.Users.
                Include(u => u.TeacherLessons).ThenInclude(u => u.Lesson)
                .Include(u => u.TeacherFaculties).ThenInclude(u => u.Faculty)
                .Include(u => u.TeacherSpecialities).ThenInclude(u => u.Speciality)
                .ToListAsync())
            {
                var teacher = new TeacherListItemDto
                {
                    Name = user.Name,
                    UserName = user.UserName,
                    Surname = user.Surname,
                    ImageUrl = user.ImageUrl,
                    IsDeleted = user.IsDeleted,
                    Roles = await _userManager.GetRolesAsync(user),
                    Lessons = user.TeacherLessons.
                    Select(teacherLesson => _mapper.Map<LessonInfoDto>(teacherLesson.Lesson)).ToList(),
                    Faculties = user.TeacherFaculties.
                    Select(teacherFaculty => _mapper.Map<FacultyInfoDto>(teacherFaculty.Faculty)).ToList(),
                    Specialities = user.TeacherSpecialities.
                    Select(teacherSpeciality => _mapper.Map<SpecialityInfoDto>(teacherSpeciality.Speciality)).ToList()
                };
                teachers.Add(teacher);
            }
        }
        else
        {
            foreach (var user in await _userManager.Users.
                Include(u => u.TeacherLessons).ThenInclude(u => u.Lesson)
                .Include(u => u.TeacherFaculties).ThenInclude(u => u.Faculty)
                .Include(u => u.TeacherSpecialities).ThenInclude(u => u.Speciality).
                Where(u => u.IsDeleted == false).ToListAsync())
            {
                var teacher = new TeacherListItemDto
                {
                    Name = user.Name,
                    UserName = user.UserName,
                    Surname = user.Surname,
                    ImageUrl = user.ImageUrl,
                    IsDeleted = user.IsDeleted,
                    Roles = await _userManager.GetRolesAsync(user),
                    Lessons = user.TeacherLessons.
                    Select(teacherLesson => _mapper.Map<LessonInfoDto>(teacherLesson.Lesson)).ToList(),
                    Faculties = user.TeacherFaculties.
                    Select(teacherFaculty => _mapper.Map<FacultyInfoDto>(teacherFaculty.Faculty)).ToList(),
                    Specialities = user.TeacherSpecialities.
                    Select(teacherSpeciality => _mapper.Map<SpecialityInfoDto>(teacherSpeciality.Speciality)).ToList()
                };
                teachers.Add(teacher);
            }
        }
        return teachers;
    }

    public async Task RemoveRoleAsync(RemoveRoleDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.userName);
        if (user == null) throw new UserNotFoundException<Teacher>();

        if (!await _roleManager.RoleExistsAsync(dto.roleName)) throw new NotFoundException<IdentityRole>();

        var result = await _userManager.RemoveFromRoleAsync(user, dto.roleName);
        if (!result.Succeeded) throw new RoleRemoveFailedException();
    }

    public async Task<TokenResponseDto> LoginWithRefreshTokenAsync(string token)
    {
        if (string.IsNullOrEmpty(token)) throw new ArgumentNullException("token");
        var user = await _userManager.Users.SingleOrDefaultAsync(t => t.RefreshToken == token);
        if (user == null) throw new NotFoundException<Teacher>();

        if (user.RefreshTokenExpiresDate < DateTime.UtcNow.AddHours(4)) throw new RefreshTokenExpiresDateException();
        return _token.CreateTeacherToken(user);
    }

    public async Task SoftDeleteAsync(string userName)
    {
        if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException("username");
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null) throw new UserNotFoundException<Teacher>();

        user.IsDeleted = true;
        user.EndDate = DateTime.UtcNow.AddHours(4);
        user.Status = Status.OutOfWork;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded) throw new UserProfileUpdateException();
    }

    public async Task RevertSoftDeleteAsync(string userName)
    {
        if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException("username");
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null) throw new UserNotFoundException<Teacher>();

        user.IsDeleted = false;
        user.StartDate = DateTime.UtcNow.AddHours(4);
        user.EndDate = null;
        user.Status = Status.Work;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded) throw new UserProfileUpdateException();
    }

    public async Task UpdateAsync(TeacherUpdateProfileDto dto)
    {
        if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException();
        if (!await _userManager.Users.AnyAsync(d => d.Id == userId)) throw new UserNotFoundException<Teacher>();

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
                _file.Delete(user.ImageUrl);
            }
            user.ImageUrl = await _file.UploadAsync(dto.ImageFile, RootConstants.TeacherImageRoot);
        }

        if (await _user.Users.AnyAsync
            (d => (d.UserName == dto.UserName && d.Id != userId) || (d.Email == dto.Email && d.Id != userId)))
            throw new UserExistException();

        _mapper.Map(dto, user);

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded) throw new UserProfileUpdateException();
    }

    public async Task UpdateAdminAsync(TeacherAdminUpdateDto dto, string id)
    {
        if (string.IsNullOrEmpty(id)) throw new ArgumentNullException("userId");
        if (!await _userManager.Users.AnyAsync(u => u.Id == id)) throw new UserNotFoundException<Teacher>();

        var user = await _userManager.Users
            .Include(u => u.TeacherLessons).ThenInclude(u => u.Lesson)
            .Include(u => u.TeacherFaculties).ThenInclude(u => u.Faculty)
            .Include(u => u.TeacherSpecialities).ThenInclude(u => u.Speciality)
            .SingleOrDefaultAsync(u => u.Id == id);
        if (user == null) throw new UserNotFoundException<Teacher>();

        if (dto.ImageFile != null)
        {
            if (!dto.ImageFile.IsSizeValid(3)) throw new FileSizeInvalidException();
            if (!dto.ImageFile.IsTypeValid("image")) throw new FileTypeInvalidExveption();
        }

        if (await _user.Users.AnyAsync
           (d => (d.UserName == dto.UserName && d.Id != id) || (d.Email == dto.Email && d.Id != id)))
            throw new UserExistException();

        user.TeacherLessons.Clear();
        if (dto.LessonIds != null)
        {
            foreach (var lsid in dto.LessonIds)
            {
                var lesson = await _lesson.FIndByIdAsync(lsid);
                if (lesson == null) throw new NotFoundException<Lesson>();
                user.TeacherLessons.Add(new TeacherLesson { LessonId = lsid });
            }
        }

        user.TeacherFaculties.Clear();
        if (dto.FacultyIds != null)
        {
            foreach (var fid in dto.FacultyIds)
            {
                var faculty = await _faculty.FIndByIdAsync(fid);
                if (faculty == null) throw new NotFoundException<Faculty>();
                user.TeacherFaculties.Add(new TeacherFaculty { FacultyId = fid });
            }
        }

        user.TeacherSpecialities.Clear();
        if (dto.SpecialityIds != null)
        {
            foreach (var sid in dto.SpecialityIds)
            {
                var speciality = await _speciality.FIndByIdAsync(sid);
                if (speciality == null) throw new NotFoundException<Speciality>();
                user.TeacherSpecialities.Add(new TeacherSpeciality { SpecialityId = sid });
            }
        }

        if (dto.Status == Status.OutOfWork)
        {
            await SoftDeleteAsync(dto.UserName);
        }
        if (dto.Status == Status.Work)
        {
            await RevertSoftDeleteAsync(dto.UserName);
        }

        _mapper.Map(dto, user);

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded) throw new UserProfileUpdateException(dto.UserName);
    }

    public async Task DeleteAsync(string userName)
    {
        if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException(nameof(userName));
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null) throw new UserNotFoundException<Teacher>();

        var res = await _userManager.DeleteAsync(user);
        if (!res.Succeeded) throw new UserDeleteProblemException(userName);
    }
}
