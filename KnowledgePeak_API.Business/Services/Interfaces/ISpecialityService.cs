using KnowledgePeak_API.Business.Dtos.SpecialityDtos;

namespace KnowledgePeak_API.Business.Services.Interfaces;

public interface ISpecialityService
{
    Task<IEnumerable<SpecialityListItemDto>> GetAllAsync(bool takeAll);
    Task<SpecialityDetailDto> GetBydIdAsync(int id, bool takeAll);
    Task CreateAsync(SpecialityCreateDto dto);
    Task AddFacultyAsync(int id, SepcialityAddFacultyDto dto);
    Task AddLessonAsync(int id, SpecialityAddLessonDto dto);
    Task UpdateAsync(int id,SpecialityUpdateDto dto);
    Task DeleteAsync(int id);
    Task SoftDeleteAsync(int id);
    Task RevertSoftDeleteAsync(int id);
}
