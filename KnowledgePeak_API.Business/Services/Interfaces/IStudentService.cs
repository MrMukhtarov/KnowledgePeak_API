using KnowledgePeak_API.Business.Dtos.RoleDtos;
using KnowledgePeak_API.Business.Dtos.StudentDtos;
using KnowledgePeak_API.Business.Dtos.TokenDtos;

namespace KnowledgePeak_API.Business.Services.Interfaces;

public interface IStudentService
{
    Task CreateAsync(StudentCreateDto dto);
    Task<TokenResponseDto> LoginAsync(StudentLoginDto dto);
    Task<TokenResponseDto> LoginWithRefreshToken(string token);
    Task<ICollection<StudentListItemDto>> GetAll(bool takeAll);
    Task<StudentDetailDto> GetByIdAsync(string id, bool takeAll);
    Task<StudentDetailDto> GetByUserNameAsync(string userName, bool takeAll);
    Task<int> StudentCount();
    Task UpdateAsync(StudentUpdateDto dto);
    Task AddRole(AddRoleDto dto);
    Task RemoveRole(RemoveRoleDto dto);
    Task SoftDeleteAsync(string userId);
    Task RevertSoftDeleteAsync(string userId);
    Task UpdatPrfileFromAdmin(string userNeme, StudentAdminUpdateDto dto);
    Task DeleteAsync(string userName);
    Task SignOut();
}
