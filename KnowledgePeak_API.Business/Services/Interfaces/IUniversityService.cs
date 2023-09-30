using KnowledgePeak_API.Business.Dtos.UniversityDtos;

namespace KnowledgePeak_API.Business.Services.Interfaces;

public interface IUniversityService
{
    Task<IEnumerable<UniversityDetailDto>> GetAllAsync();
    Task CreateAsync(UniversityCreateDto dto);
    Task UpdateAsync(int id, UniversityUpdateDto dto);
}
