using AutoMapper;
using KnowledgePeak_API.Business.Dtos.LessonDtos;
using KnowledgePeak_API.Business.Exceptions.Commons;
using KnowledgePeak_API.Business.Exceptions.Lesson;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using KnowledgePeak_API.DAL.Repositories.Interfaces;

namespace KnowledgePeak_API.Business.Services.Implements;

public class LessonService : ILessonService
{
    readonly ILessonRepository _repo;
    readonly IMapper _mapper;

    public LessonService(ILessonRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task CreateAsync(LessonCreateDto dto)
    {
        var exist = await _repo.IsExistAsync(l => l.Name == dto.Name);
        if (exist) throw new LessonNameIsExistException();

        var map = _mapper.Map<Lesson>(dto);
        await _repo.CreateAsync(map);
        await _repo.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<Lesson>();
        var entity = await _repo.FIndByIdAsync(id);
        if(entity == null) throw new NotFoundException<Lesson>();

        await _repo.DeleteAsync(id);
        await _repo.SaveAsync();
    }

    public async Task<IEnumerable<LessonListItemDto>> GetAllAsync(bool takeAll)
    {
        if (takeAll)
        {
            var data = _repo.GetAll();
            return _mapper.Map<IEnumerable<LessonListItemDto>>(data);
        }
        else
        {
            var data = _repo.FindAll(l => l.IsDeleted == false);
            return _mapper.Map<IEnumerable<LessonListItemDto>>(data);
        }
    }

    public async Task<LessonDetailDto> GetByIdAsync(int id, bool takekAll)
    {
        if (id <= 0) throw new IdIsNegativeException<Lesson>();

        if (takekAll)
        {
            var entity = await _repo.FIndByIdAsync(id);
            if (entity == null) throw new NotFoundException<Lesson>();
            return _mapper.Map<LessonDetailDto>(entity);
        }
        else
        {
            var entity = await _repo.GetSingleAsync(l => l.Id == id && l.IsDeleted == false);
            if (entity == null) throw new NotFoundException<Lesson>();
            return _mapper.Map<LessonDetailDto>(entity);
        }
    }

    public async Task RevertSoftDeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<Lesson>();
        var entity = await _repo.FIndByIdAsync(id);
        if (entity == null) throw new NotFoundException<Lesson>();

        _repo.RevertSoftDelete(entity);
        await _repo.SaveAsync();
    }

    public async Task SoftDeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<Lesson>();
        var entity = await _repo.FIndByIdAsync(id);
        if (entity == null) throw new NotFoundException<Lesson>();

        _repo.SoftDelete(entity);
        await _repo.SaveAsync();
    }

    public async Task UpdateAsync(int id, LessonUpdateDto dto)
    {
        if (id <= 0) throw new IdIsNegativeException<Lesson>();
        var entity = await _repo.FIndByIdAsync(id);
        if (entity == null) throw new NotFoundException<Lesson>();

        var exist = await _repo.IsExistAsync(l => l.Name == dto.Name && l.Id != id);
        if (exist) throw new LessonNameIsExistException();

        _mapper.Map(dto,entity);
        await _repo.SaveAsync();
    }
}
