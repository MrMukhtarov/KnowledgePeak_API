using AutoMapper;
using KnowledgePeak_API.Business.Dtos.FacultyDtos;
using KnowledgePeak_API.Business.Exceptions.Commons;
using KnowledgePeak_API.Business.Exceptions.Faculty;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using KnowledgePeak_API.DAL.Repositories.Interfaces;

namespace KnowledgePeak_API.Business.Services.Implements;

public class FacultyService : IFacultyService
{
    readonly IFacultyRepository _repo;
    readonly IMapper _mapper;

    public FacultyService(IFacultyRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task CreateAsync(FacultyCreateDto dto)
    {
        var nameExist = await _repo.IsExistAsync(f => f.Name == dto.Name);
        if (nameExist) throw new FacultyNameIsExistException();

        var map = _mapper.Map<Faculty>(dto);
        await _repo.CreateAsync(map);
        await _repo.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<Faculty>();
        var entity = await _repo.FIndByIdAsync(id);
        if (entity == null) throw new NotFoundException<Faculty>();

        await _repo.DeleteAsync(id);
        await _repo.SaveAsync();
    }

    public async Task<IEnumerable<FacultyListItemDto>> GetAllAsync(bool takeAll)
    {
        if (!takeAll)
        {
            var data = _repo.FindAll(f => f.IsDeleted == true);
            return _mapper.Map<IEnumerable<FacultyListItemDto>>(data);
        }
        else
        {
            return _mapper.Map<IEnumerable<FacultyListItemDto>>(_repo.GetAll());
        }
    }

    public async Task<FacultyDetailDto> GetByIdAsync(int id, bool takeAll)
    {
        if (id <= 0) throw new IdIsNegativeException<Faculty>();
        if (!takeAll)
        {
            var data = await _repo.GetSingleAsync(f => f.Id == id && f.IsDeleted == false);
            if(data == null) throw new NotFoundException<Faculty>();
            return _mapper.Map<FacultyDetailDto>(data);
        }
        else
        {
            var data = await _repo.GetSingleAsync(f => f.Id == id);
            if (data == null) throw new NotFoundException<Faculty>();
            return _mapper.Map<FacultyDetailDto>(data);
        }
    }

    public async Task RevertSoftDeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<Faculty>();
        var entity = await _repo.FIndByIdAsync(id);
        if (entity == null) throw new NotFoundException<Faculty>();

        _repo.RevertSoftDelete(entity);
        await _repo.SaveAsync();
    }

    public async Task SoftDeleteAsync(int id)
    {
        if (id <= 0) throw new IdIsNegativeException<Faculty>();
        var entity = await _repo.FIndByIdAsync(id);
        if (entity == null) throw new NotFoundException<Faculty>();

        _repo.SoftDelete(entity);
        await _repo.SaveAsync();
    }

    public async Task UpdateAsync(int id, FacultyUpdateDto dto)
    {
        if (id <= 0) throw new IdIsNegativeException<Faculty>();
        var entity = await _repo.FIndByIdAsync(id);
        if (entity == null) throw new NotFoundException<Faculty>();

        var exist = await _repo.IsExistAsync(f => f.Name == dto.Name && f.Id != id);
        if (exist) throw new FacultyNameIsExistException();

        _mapper.Map(dto,entity);
        await _repo.SaveAsync();
    }
}
