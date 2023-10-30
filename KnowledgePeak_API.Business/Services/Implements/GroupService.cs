using AutoMapper;
using KnowledgePeak_API.Business.Dtos.GroupDtos;
using KnowledgePeak_API.Business.Exceptions.Commons;
using KnowledgePeak_API.Business.Exceptions.Group;
using KnowledgePeak_API.Business.Exceptions.Teacher;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using KnowledgePeak_API.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KnowledgePeak_API.Business.Services.Implements;

public class GroupService : IGroupService
{
    readonly IGroupRepository _repo;
    readonly IMapper _mapper;
    readonly ISpecialityRepository _specialityRepo;
    readonly UserManager<Student> _stuManager;
    readonly IClassScheduleRepository _classScheduleRepository;

    public GroupService(IGroupRepository repo, IMapper mapper,
        ISpecialityRepository specialityRepo, UserManager<Student> stuManager,
        IClassScheduleRepository classScheduleRepository)
    {
        _repo = repo;
        _mapper = mapper;
        _specialityRepo = specialityRepo;
        _stuManager = stuManager;
        _classScheduleRepository = classScheduleRepository;
    }

    public async Task AddStudentsAsync(GroupAddStudentDto dto, int id)
    {
        var group = await _repo.GetSingleAsync(a => a.Id == id && a.IsDeleted == false, "Students");
        if (group == null) throw new NotFoundException<Group>();
        foreach (var name in dto.UserName)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException();
            var stu = await _stuManager.Users.SingleOrDefaultAsync(u => u.UserName == name && u.IsDeleted == false);
            if (stu == null) throw new UserNotFoundException<Student>();
            if (group.Limit <= group.Students.Count()) throw new GroupLimitIsFullException();
            stu.GroupId = id;
            group.Students.Add(stu);
        }
        _mapper.Map<Group>(dto);
        await _repo.SaveAsync();
    }

    public async Task CreateAsync(GroupCreateDto dto)
    {
        var exist = await _repo.IsExistAsync(g => g.Name == dto.Name);
        if (exist) throw new GroupNameIsExistException();

        var checkSpecialityId = await _specialityRepo.GetSingleAsync(s => s.Id == dto.SpecialityId && s.IsDeleted == false);
        if (checkSpecialityId == null) throw new NotFoundException<Speciality>();

        var map = _mapper.Map<Group>(dto);
        await _repo.CreateAsync(map);
        await _repo.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<Group>();
        var entity = await _repo.FIndByIdAsync(id, "Students", "ClassSchedules");
        if (entity == null) throw new NotFoundException<Group>();

        if (entity.Students.Count() > 0) throw new GroupStudentsIsNotEmptyException();
        if (entity.ClassSchedules.Count() > 0) throw new TheGroupHasSchedulesTheyCannotDetele();

        await _repo.DeleteAsync(id);
        await _repo.SaveAsync();
    }

    public async Task SoftDeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<Group>();
        var entity = await _repo.FIndByIdAsync(id, "Students", "ClassSchedules", "ClassSchedules.ClassTime");
        if (entity == null) throw new NotFoundException<Group>();

        if (entity.Students.Count() > 0) throw new GroupStudentsIsNotEmptyException();
        var schedule = await _classScheduleRepository.GetAll().ToListAsync();
        if (entity.ClassSchedules.Count() < 0)
        {
            foreach (var item in schedule)
            {
                var dateTimeStr = item.ClassTime.StartTime;
                var userTime = DateTime.Parse(dateTimeStr);
                var timeNow = DateTime.Now;
                foreach (var items in entity.ClassSchedules)
                {
                    if (item.GroupId == id && userTime <= timeNow && item.ScheduleDate.Day == timeNow.Day
                        && item.IsDeleted == false) throw new GroupHasAClassTodayException();
                    if (item.GroupId == id && item.ScheduleDate.Day > timeNow.Day && item.IsDeleted == false)
                        throw new GroupHasClassSchedulesInTheComingDaysException();
                }
            }
        }
        _repo.SoftDelete(entity);
        await _repo.SaveAsync();
    }

    public async Task<IEnumerable<GroupListItemDto>> GetAllAsync(bool takeAll)
    {
        if (takeAll)
        {
            var entity = _repo.GetAll("Students", "ClassSchedules", "ClassSchedules.Tutor", "ClassSchedules.Teacher",
                "ClassSchedules.ClassTime", "ClassSchedules.Lesson", "ClassSchedules.Room");
            return _mapper.Map<IEnumerable<GroupListItemDto>>(entity);
        }
        else
        {
            var entity = _repo.FindAll(g => g.IsDeleted == false, "Students", "ClassSchedules", "ClassSchedules.Tutor",
                "ClassSchedules.Teacher", "ClassSchedules.ClassTime", "ClassSchedules.Lesson", "ClassSchedules.Room");
            return _mapper.Map<IEnumerable<GroupListItemDto>>(entity);
        }
    }

    public async Task<GroupDetailDto> GetByIdAsync(int id, bool takeAll)
    {
        if (id <= 0) throw new IdIsNegativeException<Group>();
        if (!takeAll)
        {
            var entity = await _repo.FIndByIdAsync(id, "Students", "ClassSchedules", "ClassSchedules.Tutor",
                "ClassSchedules.Teacher", "ClassSchedules.ClassTime", "ClassSchedules.Lesson", "ClassSchedules.Room");
            if (entity == null) throw new NotFoundException<Group>();
            return _mapper.Map<GroupDetailDto>(entity);
        }
        else
        {
            var entity = await _repo.GetSingleAsync(g => g.Id == id && g.IsDeleted == false, "Students", "ClassSchedules", "ClassSchedules.Tutor",
                "ClassSchedules.Teacher", "ClassSchedules.ClassTime", "ClassSchedules.Lesson", "ClassSchedules.Room");
            if (entity == null) throw new NotFoundException<Group>();
            return _mapper.Map<GroupDetailDto>(entity);
        }
    }

    public async Task RevertSoftDeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<Group>();
        var entity = await _repo.FIndByIdAsync(id);
        if (entity == null) throw new NotFoundException<Group>();

        _repo.RevertSoftDelete(entity);
        await _repo.SaveAsync();
    }

    public async Task UpdateAsync(int id, GroupUpdateDto dto)
    {
        if (id <= 0) throw new IdIsNegativeException<Group>();
        var entity = await _repo.FIndByIdAsync(id, "Students");
        if (entity == null) throw new NotFoundException<Group>();

        if (entity.Students != null)
        {
            entity.Students.Clear();
            if (dto.UserName != null)
            {
                foreach (var item in dto.UserName)
                {
                    if (string.IsNullOrEmpty(item)) throw new ArgumentNullException();
                    var stu = await _stuManager.Users.SingleOrDefaultAsync(s => s.UserName == item && s.IsDeleted == false);
                    if (stu == null) throw new UserNotFoundException<Student>();
                    if (dto.UserName.Count() > dto.Limit) throw new GroupLimitIsFullException();
                    entity.Students.Add(stu);
                }
            }
        }
        var checkSpecialityId = await _specialityRepo.FIndByIdAsync(dto.SpecialityId);
        if (checkSpecialityId == null) throw new NotFoundException<Speciality>();

        _mapper.Map(dto, entity);
        await _repo.SaveAsync();
    }

    public async Task<int> GroupCount()
    {
        var data = await _repo.GetAll().ToListAsync();
        return data.Count();
    }
}
