using AutoMapper;
using KnowledgePeak_API.Business.Dtos.LessonDtos;
using KnowledgePeak_API.Business.Dtos.SpecialityDtos;
using KnowledgePeak_API.Business.Exceptions.Commons;
using KnowledgePeak_API.Business.Exceptions.Speciality;
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

        if (entity.FacultyId != null) throw new SpecialityFacultyIsNotEmptyException();

        var faculty = await _faultyRepository.FIndByIdAsync(dto.FacultyId);
        if (faculty == null) throw new NotFoundException<Faculty>();

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
                var isExistLesson = await _lessonRepository.FIndByIdAsync(item);
                if (isExistLesson == null) throw new NotFoundException<Lesson>();

                foreach (var itemss in entity.LessonSpecialities)
                {
                    if (itemss.SpecialityId == item) throw new LessonIsExistSpecialityException();
                }

                ls.Add(new LessonSpeciality { LessonId = item });
            }
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
        var entity = await _repo.FIndByIdAsync(id, "LessonSpecialities", "LessonSpecialities.Lesson");
        if (entity == null) throw new NotFoundException<Speciality>();

        if (entity.LessonSpecialities.Count > 0) throw new SpecialityLessonNotEmptyException();

        await _repo.DeleteAsync(id);
        await _repo.SaveAsync();
    }

    public async Task<IEnumerable<SpecialityListItemDto>> GetAllAsync(bool takeAll)
    {
        List<Lesson> lesson = new();
        var dto = new List<SpecialityListItemDto>();
        var data = _repo.GetAll("Faculty", "LessonSpecialities", "LessonSpecialities.Lesson","Groups");
        if (takeAll)
        {
            foreach (var item in data)
            {
                lesson.Clear();
                foreach (var items in item.LessonSpecialities)
                {
                    lesson.Add(items.Lesson);
                }
                var dtoItem = _mapper.Map<SpecialityListItemDto>(item);
                dtoItem.Lesson = _mapper.Map<IEnumerable<LessonListItemDto>>(lesson);
                dto.Add(dtoItem);
            }
        }
        else
        {
            var additionalEntities = await data.Where(b => b.IsDeleted).ToListAsync();
            foreach (var item in additionalEntities)
            {
                lesson.Clear();
                foreach (var items in item.LessonSpecialities)
                {
                    lesson.Add(items.Lesson);
                }
                var dtoItem = _mapper.Map<SpecialityListItemDto>(item);
                dtoItem.Lesson = _mapper.Map<IEnumerable<LessonListItemDto>>(lesson);
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
                "Faculty", "LessonSpecialities", "LessonSpecialities.Lesson","Groups");
            if (entity == null) throw new NotFoundException<Speciality>();
        }
        else
        {
            entity = await _repo.GetSingleAsync(s => s.Id == id && s.IsDeleted == false, 
                "Faculty", "LessonSpecialities", "LessonSpecialities.Lesson","Groups");
            if (entity == null) throw new NotFoundException<Speciality>();
        }
            return _mapper.Map<SpecialityDetailDto>(entity);
    }

    public async Task RevertSoftDeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<Speciality>();
        var entity = await _repo.FIndByIdAsync(id, "LessonSpecialities", "LessonSpecialities.Lesson");
        if (entity == null) throw new NotFoundException<Speciality>();

        if (entity.LessonSpecialities.Count > 0) throw new SpecialityLessonNotEmptyException();

        _repo.RevertSoftDelete(entity);
        await _repo.SaveAsync();
    }

    public async Task SoftDeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<Speciality>();
        var entity = await _repo.FIndByIdAsync(id, "LessonSpecialities", "LessonSpecialities.Lesson");
        if (entity == null) throw new NotFoundException<Speciality>();

        if (entity.LessonSpecialities.Count > 0) throw new SpecialityLessonNotEmptyException();

        _repo.SoftDelete(entity);
        await _repo.SaveAsync();
    }

    public async Task UpdateAsync(int id, SpecialityUpdateDto dto)
    {
        if (id <= 0) throw new IdIsNegativeException<Speciality>();
        var entity = await _repo.FIndByIdAsync(id, "Faculty", "LessonSpecialities", "LessonSpecialities.Lesson");
        if (entity == null) throw new NotFoundException<Speciality>();

        var exist = await _repo.IsExistAsync(s => s.Name == dto.Name && s.Id != id);
        if (exist) throw new SpecialityNameIsExistException();

        var faculty = await _faultyRepository.FIndByIdAsync(dto.FacultyId);
        if (faculty == null) throw new NotFoundException<Faculty>();

        if(dto.LessonIds != null)
        {
            foreach (var item in dto.LessonIds)
            {
                var existLesson = await _lessonRepository.FIndByIdAsync(id);
                if(existLesson == null) throw new NotFoundException<Lesson>();

                foreach (var items in entity.LessonSpecialities)
                {
                    if (items.LessonId == item) throw new LessonIsExistSpecialityException();
                }
                entity.LessonSpecialities.Add(new LessonSpeciality { LessonId = item });

            }
        }

        var map = _mapper.Map(dto, entity);
        map.FacultyId = dto.FacultyId;
        await _repo.SaveAsync();
    }
}
