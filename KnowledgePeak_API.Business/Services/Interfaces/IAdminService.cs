using KnowledgePeak_API.Business.Dtos.AdminDtos;
using KnowledgePeak_API.Business.Dtos.RoleDtos;
using KnowledgePeak_API.Business.Dtos.TokenDtos;

namespace KnowledgePeak_API.Business.Services.Interfaces;

public interface IAdminService
{
    Task<ICollection<AdminListItemDto>> GetAllAsync();
    Task CreateAsync(AdminCreateDto dto);
    Task<TokenResponseDto> LoginAsync(AdminLoginDto dto);
    Task<TokenResponseDto> LoginWithRefreshTokenAsync(string token);
    Task<AdminDetailDto> GetByIdAsync(string id);
    Task DeleteAsync(string userName);
    Task UpdateAsync(AdminUpdateDto dto);
    Task UpdateProfileAdminAsync(string userName, AdminUpdateDto dto);
    Task AddRole(AddRoleDto dto);
    Task RemoveRole(RemoveRoleDto dto);
    Task SignOut();
}
