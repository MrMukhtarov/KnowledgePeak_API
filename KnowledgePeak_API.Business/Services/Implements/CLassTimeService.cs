using AutoMapper;
using KnowledgePeak_API.Business.Dtos.ClassTimeDtos;
using KnowledgePeak_API.Business.Exceptions.Commons;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using KnowledgePeak_API.DAL.Repositories.Interfaces;

namespace KnowledgePeak_API.Business.Services.Implements;

public class CLassTimeService : IClassTimeService
{
    readonly IMapper _mapper;
    readonly IClassTimeRepository _repo;

    public CLassTimeService(IMapper mapper, IClassTimeRepository repo)
    {
        _mapper = mapper;
        _repo = repo;
    }
     
    public async Task CreateAsync(ClassTImeCreateDto dto)
    {
        var time = await _repo.IsExistAsync(c => c.StartTime == dto.StartTime || c.EndTime == dto.EndTime);
        if (time == true) throw new IsExistIdException<ClassTime>();

        var map = _mapper.Map<ClassTime>(dto);
        await _repo.CreateAsync(map);
        await _repo.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<ClassTime>();
        var clasTime = await _repo.FIndByIdAsync(id);
        if (clasTime == null) throw new NotFoundException<ClassTime>();
        await _repo.DeleteAsync(id);
        await _repo.SaveAsync();
    }

    public async Task<ICollection<ClassTimeListItemDto>> GetAllAsync()
    {
        return _mapper.Map<ICollection<ClassTimeListItemDto>>(_repo.GetAll());
    }

    public async Task<ClassTimeDetailItemDto> GetByIdAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<ClassTime>();
        var classTime = await _repo.FIndByIdAsync(id);
        if (classTime == null) throw new NotFoundException<ClassTime>();
        return _mapper.Map<ClassTimeDetailItemDto>(classTime);
    }

    public async Task UpdateAsync(ClassTimeUpdateDto dto,int id)
    {
        if (id <= 0) throw new IdIsNegativeException<ClassTime>();
        var classTime = await _repo.FIndByIdAsync(id);
        if (classTime == null) throw new NotFoundException<ClassTime>();

        var exist = await _repo.IsExistAsync(u => (u.StartTime == dto.StartTime && id != u.Id) &&
        (u.EndTime == dto.EndTime && u.Id != id));
        if (exist) throw new IsExistIdException<ClassTime>();
        var map = _mapper.Map(dto, classTime);
        await _repo.SaveAsync();
    }
}
