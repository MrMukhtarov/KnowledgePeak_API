using AutoMapper;
using KnowledgePeak_API.Business.Dtos.FacultyDtos;
using KnowledgePeak_API.Business.Dtos.LessonDtos;
using KnowledgePeak_API.Business.Dtos.SpecialityDtos;
using KnowledgePeak_API.Business.Dtos.TeacherDtos;
using KnowledgePeak_API.Business.Exceptions.Commons;
using KnowledgePeak_API.Business.Exceptions.Speciality;
using KnowledgePeak_API.Business.Exceptions.Tutor;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using KnowledgePeak_API.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KnowledgePeak_API.Business.Services.Implements;

public class SpecialityService : ISpecialityService
{
    readonly ISpecialityRepository _repo;
    readonly IMapper _mapper;
    readonly IFacultyRepository _faultyRepository;
    readonly ILessonRepository _lessonRepository;

    public SpecialityService(ISpecialityRepository repo, IMapper mapper,
        IFacultyRepository faultyRepository, ILessonRepository lessonRepository)
    {
        _repo = repo;
        _mapper = mapper;
        _faultyRepository = faultyRepository;
        _lessonRepository = lessonRepository;
    }

    public async Task AddFacultyAsync(int id, SepcialityAddFacultyDto dto)
    {
        if (id <= 0) throw new IdIsNegativeException<Speciality>();
        var entity = await _repo.FIndByIdAsync(id, "Faculty");
        if (entity == null) throw new NotFoundException<Speciality>();

       if(dto.FacultyId != null)
        {
            var faculty = await _faultyRepository.GetSingleAsync(f => f.Id == dto.FacultyId && f.IsDeleted == false);
            if (faculty == null) throw new NotFoundException<Faculty>();
        }
        else
        {
            entity.FacultyId = null;
        }

        entity.FacultyId = dto.FacultyId;
        await _repo.SaveAsync();
    }

    public async Task AddLessonAsync(int id, SpecialityAddLessonDto dto)
    {
        if (id <= 0) throw new IdIsNegativeException<Speciality>();
        var entity = await _repo.FIndByIdAsync(id, "LessonSpecialities", "LessonSpecialities.Lesson");
        if (entity == null) throw new NotFoundException<Speciality>();

        List<LessonSpeciality> ls = new();
        if (dto.LessonIds != null)
        {
            foreach (var item in dto.LessonIds)
            {
                var isExistLesson = await _lessonRepository.GetSingleAsync(l => l.Id == item && l.IsDeleted == false);
                if (isExistLesson == null) throw new NotFoundException<Lesson>();

                foreach (var itemss in entity.LessonSpecialities)
                {
                    if (itemss.SpecialityId == item) throw new LessonIsExistSpecialityException();
                }

                ls.Add(new LessonSpeciality { LessonId = item });
            }
        }
        else
        {
            entity.LessonSpecialities.Clear();
        }
        var map = _mapper.Map<Speciality>(entity);
        map.LessonSpecialities = ls;
        await _repo.SaveAsync();
    }

    public async Task CreateAsync(SpecialityCreateDto dto)
    {
        var data = await _repo.IsExistAsync(s => s.Name == dto.Name);
        if (data) throw new SpecialityNameIsExistException();

        var map = _mapper.Map<Speciality>(dto);
        await _repo.CreateAsync(map);
        await _repo.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<Speciality>();
        var entity = await _repo.FIndByIdAsync(id, "LessonSpecialities", "LessonSpecialities.Lesson",

            "TeacherSpecialities", "TeacherSpecialities.Teacher", "Tutors", "Groups");
        if (entity == null) throw new NotFoundException<Speciality>();

        if (entity.LessonSpecialities.Count > 0) throw new SpecialityLessonNotEmptyException();
        if (entity.TeacherSpecialities.Count > 0) throw new SpecialityTeacherNotEmptyException();
        if (entity.Tutors.Count > 0) throw new SpecialityTutorsNotEmptyException();

        await _repo.DeleteAsync(id);
        await _repo.SaveAsync();
    }

    public async Task<IEnumerable<SpecialityListItemDto>> GetAllAsync(bool takeAll)
    {
        List<Lesson> lesson = new();
        List<Teacher> teacher = new();
        var dto = new List<SpecialityListItemDto>();
        var data = _repo.GetAll("Faculty", "LessonSpecialities", "LessonSpecialities.Lesson", "Groups"
            , "TeacherSpecialities", "TeacherSpecialities.Teacher","Tutors");
        if (takeAll)
        {
            foreach (var item in data)
            {
                lesson.Clear();
                teacher.Clear();
                foreach (var items in item.LessonSpecialities)
                {
                    lesson.Add(items.Lesson);
                }
                foreach (var items in item.TeacherSpecialities)
                {
                    teacher.Add(items.Teacher);
                }
                var dtoItem = _mapper.Map<SpecialityListItemDto>(item);
                dtoItem.Lesson = _mapper.Map<IEnumerable<LessonListItemDto>>(lesson);
                dtoItem.Teacher = _mapper.Map<List<TeacherDetailDto>>(teacher);
                dto.Add(dtoItem);
            }
        }
        else
        {
            var additionalEntities = await data.Where(b => b.IsDeleted).ToListAsync();
            foreach (var item in additionalEntities)
            {
                lesson.Clear();
                teacher.Clear();
                foreach (var items in item.LessonSpecialities)
                {
                    lesson.Add(items.Lesson);
                }
                foreach (var items in item.TeacherSpecialities)
                {
                    teacher.Add(items.Teacher);
                }
                var dtoItem = _mapper.Map<SpecialityListItemDto>(item);
                dtoItem.Lesson = _mapper.Map<IEnumerable<LessonListItemDto>>(lesson);
                dtoItem.Teacher = _mapper.Map<List<TeacherDetailDto>>(teacher);
                dto.Add(dtoItem);
            }
        }
        return dto;
    }

    public async Task<SpecialityDetailDto> GetBydIdAsync(int id, bool takeAll)
    {
        if (id <= 0) throw new IdIsNegativeException<Speciality>();

        Speciality? entity;

        if (takeAll)
        {
            entity = await _repo.GetSingleAsync(s => s.Id == id,
                "Faculty", "LessonSpecialities", "LessonSpecialities.Lesson", "Groups",
                "TeacherSpecialities", "TeacherSpecialities.Teacher","Tutors");
            if (entity == null) throw new NotFoundException<Speciality>();
        }
        else
        {
            entity = await _repo.GetSingleAsync(s => s.Id == id && s.IsDeleted == false,
                "Faculty", "LessonSpecialities", "LessonSpecialities.Lesson", "Groups"
                , "TeacherSpecialities", "TeacherSpecialities.Teacher", "Tutors");
            if (entity == null) throw new NotFoundException<Speciality>();
        }
        return _mapper.Map<SpecialityDetailDto>(entity);
    }

    public async Task RevertSoftDeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<Speciality>();
        var entity = await _repo.FIndByIdAsync(id, "LessonSpecialities", "LessonSpecialities.Lesson");
        if (entity == null) throw new NotFoundException<Speciality>();

        _repo.RevertSoftDelete(entity);
        await _repo.SaveAsync();
    }

    public async Task SoftDeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<Speciality>();
        var entity = await _repo.FIndByIdAsync(id, "LessonSpecialities", "LessonSpecialities.Lesson",
            "TeacherSpecialities", "TeacherSpecialities.Teacher", "Tutors");
        if (entity == null) throw new NotFoundException<Speciality>();

        if (entity.LessonSpecialities.Count > 0) throw new SpecialityLessonNotEmptyException();
        if (entity.TeacherSpecialities.Count > 0) throw new SpecialityTeacherNotEmptyException();
        if (entity.Tutors.Count > 0) throw new SpecialityTutorsNotEmptyException();

        _repo.SoftDelete(entity);
        await _repo.SaveAsync();
    }

    public async Task<int> SpecialityCount()
    {
        var data = await _repo.GetAll().ToListAsync();
        return data.Count();
    }

    public async Task UpdateAsync(int id, SpecialityUpdateDto dto)
    {
        if (id <= 0) throw new IdIsNegativeException<Speciality>();
        var entity = await _repo.FIndByIdAsync(id, "Faculty", "LessonSpecialities", "LessonSpecialities.Lesson", "Groups");
        if (entity == null) throw new NotFoundException<Speciality>();

        var exist = await _repo.IsExistAsync(s => s.Name == dto.Name && s.Id != id);
        if (exist) throw new SpecialityNameIsExistException();

       if(dto.FacultyId != null)
        {
            var faculty = await _faultyRepository.GetSingleAsync(f => f.Id == dto.FacultyId && f.IsDeleted == false);
            if (faculty == null) throw new NotFoundException<Faculty>();
        }

        entity.LessonSpecialities.Clear();
        if (dto.LessonIds != null)
        {
            foreach (var item in dto.LessonIds)
            {
                var existLesson = await _lessonRepository.GetSingleAsync(f => f.Id == item && f.IsDeleted == false);
                if (existLesson == null) throw new NotFoundException<Lesson>();
                entity.LessonSpecialities.Add(new LessonSpeciality { LessonId = item });
            }
        }
        else
        {
            entity.LessonSpecialities.Clear();
        }
        var map = _mapper.Map(dto, entity);
        await _repo.SaveAsync();
    }
}
