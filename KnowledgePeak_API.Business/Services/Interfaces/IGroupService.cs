using KnowledgePeak_API.Business.Dtos.GroupDtos;
using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.Services.Interfaces;

public interface IGroupService
{
    Task<IEnumerable<GroupListItemDto>> GetAllAsync(bool takeAll);
    Task<GroupDetailDto> GetByIdAsync(int id, bool takeAll);
    Task CreateAsync(GroupCreateDto dto);
    Task UpdateAsync(int id, GroupUpdateDto dto);
    Task AddStudentsAsync(GroupAddStudentDto dto,int id);
    Task DeleteAsync(int id);
    Task SoftDeleteAsync(int id);
    Task RevertSoftDeleteAsync(int id);
}
