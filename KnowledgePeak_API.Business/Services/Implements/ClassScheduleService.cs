using AutoMapper;
using KnowledgePeak_API.Business.Dtos.ClassScheduleDtos;
using KnowledgePeak_API.Business.Exceptions.ClassSchedules;
using KnowledgePeak_API.Business.Exceptions.Commons;
using KnowledgePeak_API.Business.Exceptions.Group;
using KnowledgePeak_API.Business.Exceptions.Room;
using KnowledgePeak_API.Business.Exceptions.Teacher;
using KnowledgePeak_API.Business.Exceptions.Tutor;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using KnowledgePeak_API.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KnowledgePeak_API.Business.Services.Implements;

public class ClassScheduleService : IClassScheduleService
{
    readonly IClassScheduleRepository _repo;
    readonly IMapper _mapper;
    readonly IHttpContextAccessor _accessor;
    readonly string _userId;
    readonly UserManager<Tutor> _tutor;
    readonly IGroupRepository _group;
    readonly ILessonRepository _lesson;
    readonly IClassTimeRepository _classTime;
    readonly IRoomRepository _room;
    readonly UserManager<Teacher> _teacher;

    public ClassScheduleService(IClassScheduleRepository repo, IMapper mapper, IHttpContextAccessor accessor,
        UserManager<Tutor> tutor, IGroupRepository group, ILessonRepository lesson, IClassTimeRepository classTime,
        IRoomRepository room, UserManager<Teacher> teacher)
    {
        _repo = repo;
        _mapper = mapper;
        _accessor = accessor;
        _userId = _accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        _tutor = tutor;
        _group = group;
        _lesson = lesson;
        _classTime = classTime;
        _room = room;
        _teacher = teacher;
    }

    public async Task CreateAsync(ClassScheduleCreateDto dto)
    {
        if (string.IsNullOrEmpty(_userId)) throw new ArgumentNullException();
        var tutor = await _tutor.Users.AnyAsync(u => u.Id == _userId);
        if (tutor == false) throw new UserNotFoundException<Tutor>();
        var tutors = await _tutor.Users.Include(t => t.ClassSchedules).ThenInclude(g => g.Group).SingleOrDefaultAsync(u => u.Id == _userId);
        if (tutors == null) throw new UserNotFoundException<Tutor>();

        var group = await _group.GetSingleAsync(g => g.Id == dto.GroupId && g.IsDeleted == false, "ClassSchedules", "Students");
        if (group == null) throw new NotFoundException<Group>();

        var lesson = await _lesson.GetSingleAsync(l => l.Id == dto.LessonId && l.IsDeleted == false);
        if (lesson == null) throw new NotFoundException<Lesson>();

        var classTime = await _classTime.GetSingleAsync(c => c.Id == dto.ClassTimeId && c.IsDeleted == false);
        if (classTime == null) throw new NotFoundException<ClassTime>();

        var room = await _room.GetSingleAsync(r => r.Id == dto.RoomId && r.IsDeleted == false);
        if (room == null) throw new NotFoundException<Room>();

        var teacher = await _teacher.Users.Include(t => t.TeacherLessons)
            .ThenInclude(t => t.Lesson).SingleOrDefaultAsync(r => r.Id == dto.TeacherId && r.IsDeleted == false);
        if (teacher == null) throw new UserNotFoundException<Teacher>();

        if (room.IsEmpty == false) throw new RoomNotEmptyException();
        if (room.Capacity < group.Students.Count()) throw new TheGroupsNumberOfStudentsExceedsTheRoomsCapacityException();

        if (!tutors.Groups.Any(g => g.Id == dto.GroupId)) throw new ThisGroupDoesNotBelongAmongTheTutorsGroupsException();

        foreach (var item in group.ClassSchedules)
        {
            if (item.ScheduleDate == dto.ScheduleDate && item.ClassTimeId == dto.ClassTimeId)
                throw new GroupThisDayScheduleNotEmptyException();
        }
        if(!teacher.TeacherLessons.Any(t => t.LessonId == dto.LessonId)) throw new TeacherDoesNotTeachThisLessonException();
        var repo = await _repo.GetAll().ToListAsync();
        foreach (var item in repo)
        {
            if (item.TeacherId == teacher.Id && item.ScheduleDate == dto.ScheduleDate
                && item.ClassTimeId == dto.ClassTimeId)
                throw new TeacherNotEmptyThisDateException();

            if (item.RoomId == room.Id && item.ScheduleDate == dto.ScheduleDate
                && item.ClassTimeId == dto.ClassTimeId) throw new RoomNotEmptyException();
        }

        if (dto.ScheduleDate < DateTime.Now) throw new TheProgramCannotbeWritteninThePastException();

        var map = _mapper.Map<ClassSchedule>(dto);
        tutors.ClassSchedules.Add(map);
        await _repo.CreateAsync(map);
        await _repo.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<ClassSchedule>();
        var schedule = await _repo.FIndByIdAsync(id);
        if (schedule == null) throw new NotFoundException<ClassSchedule>();
        if (DateTime.Now.Day == schedule.ScheduleDate.Day) throw new ClassScheduleNotRemoveDuringLessonException();
        await _repo.DeleteAsync(id);
        await _repo.SaveAsync();
    }

    public async Task<ICollection<ClassScheduleListItemDto>> GetAllAync(bool takeAll)
    {
        if (takeAll)
        {
            var data = _repo.GetAll("Lesson", "ClassTime", "Tutor", "Group", "Room", "Teacher");
            return _mapper.Map<ICollection<ClassScheduleListItemDto>>(data);
        }
        else
        {
            var data = _repo.FindAll(a => a.IsDeleted == false, "Lesson", "ClassTime", "Tutor", "Group", "Room", "Teacher");
            return _mapper.Map<ICollection<ClassScheduleListItemDto>>(data);
        }
    }

    public async Task<ClassScheduleDetailDto> GetByIdAsync(int id, bool takeAll)
    {
        if (id <= 0) throw new IdIsNegativeException<ClassSchedule>();
        if (takeAll)
        {
            var data = await _repo.GetSingleAsync(a => a.Id == id, "Lesson", "ClassTime", "Tutor", "Group", "Room", "Teacher");
            if (data == null) throw new NotFoundException<ClassSchedule>();
            return _mapper.Map<ClassScheduleDetailDto>(data);
        }
        else
        {
            var data = await _repo.GetSingleAsync(a => a.Id == id && a.IsDeleted == false, "Lesson", "ClassTime", "Tutor", "Group", "Room", "Teacher");
            if (data == null) throw new NotFoundException<ClassSchedule>();
            return _mapper.Map<ClassScheduleDetailDto>(data);
        }
    }

    public async Task RevertSoftDeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<ClassSchedule>();
        var schedule = await _repo.FIndByIdAsync(id);
        if (schedule == null) throw new NotFoundException<ClassSchedule>();
        _repo.RevertSoftDelete(schedule);
        await _repo.SaveAsync();
    }

    public async Task SoftDeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<ClassSchedule>();
        var schedule = await _repo.FIndByIdAsync(id);
        if (schedule == null) throw new NotFoundException<ClassSchedule>();
        if (DateTime.Now.Day == schedule.ScheduleDate.Day) throw new ClassScheduleNotRemoveDuringLessonException();
        _repo.SoftDelete(schedule);
        await _repo.SaveAsync();
    }

    public async Task UpdateAsync(int id, ClassScheduleUpdateDto dto)
    {
        if (string.IsNullOrEmpty(_userId)) throw new ArgumentNullException();
        var tutor = await _tutor.Users.FirstOrDefaultAsync(u => u.Id == _userId);
        if (tutor == null) throw new UserNotFoundException<Tutor>();

        if (id <= 0) throw new IdIsNegativeException<ClassSchedule>();
        var classSchedule = await _repo.GetSingleAsync(a => a.Id == id && a.IsDeleted == false,
            "Lesson", "ClassTime", "Tutor", "Group", "Room", "Teacher");
        if (classSchedule == null) throw new NotFoundException<ClassSchedule>();

        if (classSchedule.ScheduleDate < DateTime.Now) throw new TheLessonCannotBeDeletedAsItBelongsToAPastTimeException();

        var group = await _group.GetSingleAsync(a => a.Id == dto.GroupId && a.IsDeleted == false, "ClassSchedules", "Students");
        if (group == null) throw new NotFoundException<Group>();

        var lesson = await _lesson.GetSingleAsync(a => a.Id == dto.LessonId && a.IsDeleted == false);
        if (lesson == null) throw new NotFoundException<Lesson>();

        var classTime = await _classTime.GetSingleAsync(a => a.Id == dto.ClassTimeId && a.IsDeleted == false);
        if (classTime == null) throw new NotFoundException<ClassTime>();

        var room = await _room.GetSingleAsync(a => a.Id == dto.RoomId && a.IsDeleted == false);
        if (room == null) throw new NotFoundException<Room>();

        var teacher = await _teacher.Users.Include(a => a.TeacherLessons).ThenInclude(a => a.Lesson)
            .SingleOrDefaultAsync(u => u.Id == dto.TeacherId && u.IsDeleted == false);
        if (teacher == null) throw new UserNotFoundException<Teacher>();
        if (room.IsEmpty == false) throw new RoomNotEmptyException();
        if (room.Capacity < group.Students.Count()) throw new TheGroupsNumberOfStudentsExceedsTheRoomsCapacityException();

        foreach (var item in group.ClassSchedules)
        {
            if (item.ScheduleDate == dto.ScheduleDate && item.ClassTimeId == dto.ClassTimeId && id != item.Id)
                throw new GroupThisDayScheduleNotEmptyException();
        }
        if(!teacher.TeacherLessons.Any(t => t.LessonId == dto.LessonId)) throw new TeacherDoesNotTeachThisLessonException();
        var repo = await _repo.GetAll().ToListAsync();
        foreach (var item in repo)
        {
            if (item.TeacherId == teacher.Id && item.ScheduleDate == dto.ScheduleDate
                && item.ClassTimeId == dto.ClassTimeId && id != item.Id)
                throw new TeacherNotEmptyThisDateException();

            if (item.RoomId == room.Id && item.ScheduleDate == dto.ScheduleDate
                && item.ClassTimeId == dto.ClassTimeId && id != item.Id) throw new RoomNotEmptyException();
        }
        if (dto.ScheduleDate < DateTime.Now) throw new TheProgramCannotbeWritteninThePastException();
        _mapper.Map(dto, classSchedule);
        await _repo.SaveAsync();
    }
}
