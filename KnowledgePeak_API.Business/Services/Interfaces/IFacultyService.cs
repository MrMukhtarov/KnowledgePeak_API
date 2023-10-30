using KnowledgePeak_API.Business.Dtos.FacultyDtos;

namespace KnowledgePeak_API.Business.Services.Interfaces;

public interface IFacultyService
{
    Task<IEnumerable<FacultyListItemDto>> GetAllAsync(bool takeAll);
    Task<FacultyDetailDto> GetByIdAsync(int id, bool takeAll);
    Task CreateAsync(FacultyCreateDto dto);
    Task UpdateAsync(int id,FacultyUpdateDto dto);
    Task DeleteAsync(int id);
    Task SoftDeleteAsync(int id);
    Task RevertSoftDeleteAsync(int id);
    Task<int> FacultyCount();
}
