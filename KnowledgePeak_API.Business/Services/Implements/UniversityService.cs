using AutoMapper;
using KnowledgePeak_API.Business.Dtos.UniversityDtos;
using KnowledgePeak_API.Business.Exceptions.Commons;
using KnowledgePeak_API.Business.Exceptions.University;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using KnowledgePeak_API.DAL.Repositories.Interfaces;

namespace KnowledgePeak_API.Business.Services.Implements;

public class UniversityService : IUniversityService
{
    readonly IUniversityRepository _repo;
    readonly IMapper _mapper;

    public UniversityService(IUniversityRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task CreateAsync(UniversityCreateDto dto)
    {
        var data = _repo.GetAll();
        if (data.Count() > 0) throw new UniversityIsExistException();

        var map = _mapper.Map<University>(dto);

        await _repo.CreateAsync(map);
        await _repo.SaveAsync();
    }

    public async Task<IEnumerable<UniversityDetailDto>> GetAllAsync()
    {
        var data = _repo.GetAll();
        return _mapper.Map<IEnumerable<UniversityDetailDto>>(data);
    }

    public async Task UpdateAsync(int id, UniversityUpdateDto dto)
    {
        if (id <= 0) throw new IdIsNegativeException<University>();
        var entity = await _repo.FIndByIdAsync(id);
        if (entity == null) throw new NotFoundException<University>();

        _mapper.Map(dto, entity);
        await _repo.SaveAsync();
    }
}
