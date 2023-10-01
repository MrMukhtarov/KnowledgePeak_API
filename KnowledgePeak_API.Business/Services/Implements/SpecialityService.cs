using AutoMapper;
using KnowledgePeak_API.Business.Dtos.SpecialityDtos;
using KnowledgePeak_API.Business.Exceptions.Commons;
using KnowledgePeak_API.Business.Exceptions.Speciality;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using KnowledgePeak_API.DAL.Repositories.Interfaces;

namespace KnowledgePeak_API.Business.Services.Implements;

public class SpecialityService : ISpecialityService
{
    readonly ISpecialityRepository _repo;
    readonly IMapper _mapper;
    readonly IFacultyRepository _faultyRepository;

    public SpecialityService(ISpecialityRepository repo, IMapper mapper,
        IFacultyRepository faultyRepository)
    {
        _repo = repo;
        _mapper = mapper;
        _faultyRepository = faultyRepository;
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
        var entity = await _repo.FIndByIdAsync(id);
        if (entity == null) throw new NotFoundException<Speciality>();

        await _repo.DeleteAsync(id);
        await _repo.SaveAsync();
    }

    public async Task<IEnumerable<SpecialityListItemDto>> GetAllAsync(bool takeAll)
    {
        if (takeAll)
        {
            var data = _repo.GetAll("Faculty");
            return _mapper.Map<IEnumerable<SpecialityListItemDto>>(data);
        }
        else
        {
            var data = _repo.FindAll(s => s.IsDeleted == false, "Faculty");
            return _mapper.Map<IEnumerable<SpecialityListItemDto>>(data);
        }
    }

    public async Task<SpecialityDetailDto> GetBydIdAsync(int id, bool takeAll)
    {
        if (id <= 0) throw new IdIsNegativeException<Speciality>();


        if (takeAll)
        {
            var entity = await _repo.GetSingleAsync(s => s.Id == id, "Faculty");
            if (entity == null) throw new NotFoundException<Speciality>();
            return _mapper.Map<SpecialityDetailDto>(entity);
        }
        else
        {
            var entity = await _repo.GetSingleAsync(s => s.Id == id && s.IsDeleted == false, "Faculty");
            if (entity == null) throw new NotFoundException<Speciality>();
            return _mapper.Map<SpecialityDetailDto>(entity);
        }
    }

    public async Task RevertSoftDeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<Speciality>();
        var entity = await _repo.FIndByIdAsync(id);
        if (entity == null) throw new NotFoundException<Speciality>();

        _repo.RevertSoftDelete(entity);
        await _repo.SaveAsync();
    }

    public async Task SoftDeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<Speciality>();
        var entity = await _repo.FIndByIdAsync(id);
        if (entity == null) throw new NotFoundException<Speciality>();

        _repo.SoftDelete(entity);
        await _repo.SaveAsync();
    }

    public async Task UpdateAsync(int id, SpecialityUpdateDto dto)
    {
        if (id <= 0) throw new IdIsNegativeException<Speciality>();
        var entity = await _repo.FIndByIdAsync(id, "Faculty");
        if (entity == null) throw new NotFoundException<Speciality>();

        var exist = await _repo.IsExistAsync(s => s.Name == dto.Name && s.Id != id);
        if (exist) throw new SpecialityNameIsExistException();

        var faculty = await _faultyRepository.FIndByIdAsync(dto.FacultyId);
        if (faculty == null) throw new NotFoundException<Faculty>();

        var map = _mapper.Map(dto, entity);
        map.FacultyId = dto.FacultyId;
        await _repo.SaveAsync();
    }
}
