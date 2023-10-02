using KnowledgePeak_API.Business.Dtos.GroupDtos;

namespace KnowledgePeak_API.Business.Services.Interfaces;

public interface IGroupService
{
    Task<IEnumerable<GroupListItemDto>> GetAllAsync(bool takeAll);
    Task<GroupDetailDto> GetByIdAsync(int id, bool takeAll);
    Task CreateAsync(GroupCreateDto dto);
    Task GroupAddSpecialityAsync(int id, GroupAddSpecialityDto dto);
    Task UpdateAsync(int id, GroupUpdateDto dto);
    Task DeleteAsync(int id);
    Task SoftDeleteAsync(int id);
    Task RevertSoftDeleteAsync(int id);
}
