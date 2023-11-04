using KnowledgePeak_API.Business.Dtos.ClassTimeDtos;

namespace KnowledgePeak_API.Business.Services.Interfaces;

public interface IClassTimeService
{
    Task<ICollection<ClassTimeListItemDto>> GetAllAsync();
    Task<ClassTimeDetailItemDto> GetByIdAsync(int id);
    Task CreateAsync(ClassTImeCreateDto dto);
    Task UpdateAsync(ClassTimeUpdateDto dto,int id);
    Task DeleteAsync(int id);
    Task<int> Count();
}
