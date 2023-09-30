using AutoMapper;
using KnowledgePeak_API.Business.Dtos.UniversityDtos;
using KnowledgePeak_API.Business.Services.Interfaces;
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

    public async Task<IEnumerable<UniversityDetailDto>> GetAllAsync()
    {
        var data = _repo.GetAll();
        return _mapper.Map<IEnumerable<UniversityDetailDto>>(data);
    }

    public Task UpdateAsync(int id, UniversityUpdateDto dto)
    {
        throw new NotImplementedException();
    }
}
