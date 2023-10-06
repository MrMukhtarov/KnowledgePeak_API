using AutoMapper;
using KnowledgePeak_API.Business.Constants;
using KnowledgePeak_API.Business.Dtos.TeacherDtos;
using KnowledgePeak_API.Business.Dtos.TokenDtos;
using KnowledgePeak_API.Business.Exceptions.Commons;
using KnowledgePeak_API.Business.Exceptions.File;
using KnowledgePeak_API.Business.Exceptions.Teacher;
using KnowledgePeak_API.Business.Extensions;
using KnowledgePeak_API.Business.ExternalServices.Interfaces;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using KnowledgePeak_API.Core.Enums;
using KnowledgePeak_API.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

    public TeacherService(IMapper mapper, UserManager<Teacher> userManager,
        IFileService file, UserManager<AppUser> user, IFacultyRepository faculty,
        ISpecialityRepository speciality, ILessonRepository lesson, ITokenService token)
    {
        _mapper = mapper;
        _user = user;
        _userManager = userManager;
        _file = file;
        _faculty = faculty;
        _speciality = speciality;
        _lesson = lesson;
        _token = token;
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
        if (password == false) throw new UserNotFoundException<Teacher>();

        return _token.CreateTeacherToken(teacher);
    }
}
