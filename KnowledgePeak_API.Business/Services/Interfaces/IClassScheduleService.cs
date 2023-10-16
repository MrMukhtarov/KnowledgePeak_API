using KnowledgePeak_API.Business.Dtos.ClassScheduleDtos;

namespace KnowledgePeak_API.Business.Services.Interfaces;

public interface IClassScheduleService
{
    Task<ICollection<ClassScheduleListItemDto>> GetAllAync(bool takeAll);
    Task<ClassScheduleDetailDto> GetByIdAsync(int id, bool takeAll);
    Task CreateAsync(ClassScheduleCreateDto dto);
    Task UpdateAsync(int id, ClassScheduleUpdateDto dto);
    Task DeleteAsync(int id);
    Task SoftDeleteAsync(int id);
    Task RevertSoftDeleteAsync(int id);
}
