using AutoMapper;
using KnowledgePeak_API.Business.Dtos.GradeDtos;
using KnowledgePeak_API.Business.Dtos.StudentHistoryDtos;
using KnowledgePeak_API.Business.Exceptions.Commons;
using KnowledgePeak_API.Business.Exceptions.Grade;
using KnowledgePeak_API.Business.Exceptions.Teacher;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using KnowledgePeak_API.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KnowledgePeak_API.Business.Services.Implements;

public class GradeService : IGradeService
{
    readonly IGradeRepository _repo;
    readonly IMapper _mapper;
    readonly string? _userId;
    readonly IHttpContextAccessor _accessor;
    readonly UserManager<Student> _student;
    readonly UserManager<Teacher> _teacher;
    readonly ILessonRepository _lesson;
    readonly IStudentHistoryService _studentHistoryService;

    public GradeService(IGradeRepository repo, IMapper mapper, IHttpContextAccessor accessor,
        UserManager<Student> student, UserManager<Teacher> teacher, ILessonRepository lesson,
        IStudentHistoryService studentHistoryService)
    {
        _repo = repo;
        _mapper = mapper;
        _accessor = accessor;
        _student = student;
        _teacher = teacher;
        _userId = _accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        _lesson = lesson;
        _studentHistoryService = studentHistoryService;
    }

    public async Task CreateAsync(GradeCreateDto dto)
    {
        if (string.IsNullOrEmpty(_userId)) throw new ArgumentNullException();
        var teacher = await _teacher.Users.Include(t => t.TeacherLessons).ThenInclude(t => t.Lesson)
            .SingleOrDefaultAsync(u => u.Id == _userId);
        if (teacher == null) throw new UserNotFoundException<Teacher>();

        var stu = await _student.FindByIdAsync(dto.StudentId);
        if (stu == null) throw new UserNotFoundException<Student>();

        var lesson = await _lesson.FIndByIdAsync(dto.LessonId);
        if (lesson == null) throw new NotFoundException<Lesson>();

        if (!teacher.TeacherLessons.Any(a => a.LessonId == dto.LessonId)) throw new TeacherDoesNotTeachThisLessonException();
        var map = _mapper.Map<Grade>(dto);
        map.TeacherId = _userId;

        var avarage = await _repo.GetAll().Where(s => s.StudentId == dto.StudentId).ToListAsync();
        if (avarage.Count == 0)
        {
            stu.Avarage = dto.Point;
        }
        else
        {
            double sum = avarage.Sum(g => g.Point);
            sum += dto.Point;
            int count = avarage.Count() + 1;
            double result = sum / count;
            stu.Avarage = result;
        }
        await _student.UpdateAsync(stu);
        await _repo.CreateAsync(map);
        await _repo.SaveAsync();

        StudentHistoryCreateDto history = new StudentHistoryCreateDto();
        history.HistoryDate = DateTime.Now;
        history.Studentid = dto.StudentId;
        history.Grade = map;
        await _studentHistoryService.CreateAsync(history);
    }

    public async Task<ICollection<GradeListItemDto>> GetAllAsync()
    {
        var data = _repo.GetAll("Teacher", "Student", "Lesson");
        var map = _mapper.Map<ICollection<GradeListItemDto>>(data);
        return map;
    }

    public async Task<GradeDetailDto> GetByIdAsyc(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<Grade>();
        var grade = await _repo.FIndByIdAsync(id, "Teacher", "Student", "Lesson");
        if (grade == null) throw new NotFoundException<Grade>();
        return _mapper.Map<GradeDetailDto>(grade);
    }

    public async Task UpdateAsync(GradeUpdateDto dto)
    {
        if (string.IsNullOrEmpty(_userId)) throw new ArgumentNullException();
        var teacher = await _teacher.Users.Include(t => t.TeacherLessons).ThenInclude(t => t.Lesson).
            SingleOrDefaultAsync(u => u.Id == _userId);
        if (teacher == null) throw new UserNotFoundException<Teacher>();

        if (dto.id <= 0) throw new IdIsNegativeException<Grade>();
        var grade = await _repo.FIndByIdAsync(dto.id, "StudentHistory");
        if (grade == null) throw new NotFoundException<Grade>();

        if (DateTime.Now > grade.GradeDate.AddMinutes(15))
            throw new ItsBennFiftyMinutesSinceTheGradeWasGivenException();

        var stu = await _student.FindByIdAsync(dto.StudentId);
        if (stu == null || stu.IsDeleted == true) throw new UserNotFoundException<Student>();

        var lesson = await _lesson.FIndByIdAsync(dto.LessonId);
        if (lesson == null) throw new NotFoundException<Lesson>();

        var map = _mapper.Map(dto, grade);
        map.TeacherId = _userId;

        if(teacher.TeacherLessons != null)
        {
            if(!teacher.TeacherLessons.Any(t => t.LessonId == dto.LessonId)) throw new TeacherDoesNotTeachThisLessonException();
        }
       
        var avarage = await _repo.GetAll().Where(s => s.StudentId == dto.StudentId).ToListAsync();
        if (avarage.Count == 0)
        {
            stu.Avarage = dto.Point;
        }
        else
        {
            double sum = avarage.Sum(g => g.Point);
            sum += dto.Point;
            int count = avarage.Count() + 1;
            double result = sum / count;
            stu.Avarage = result;
        }
        await _student.UpdateAsync(stu);
        await _repo.SaveAsync();

        StudentHistoryUpdateDto history = new StudentHistoryUpdateDto();
        history.Grade = map;
        await _studentHistoryService.UpdateAsync(grade.StudentHistory.Id, history);
    }
}
