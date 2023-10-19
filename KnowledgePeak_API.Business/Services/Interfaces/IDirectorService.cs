using KnowledgePeak_API.Business.Dtos.DirectorDtos;
using KnowledgePeak_API.Business.Dtos.RoleDtos;
using KnowledgePeak_API.Business.Dtos.TokenDtos;

namespace KnowledgePeak_API.Business.Services.Interfaces;

public interface IDirectorService
{
    Task CreateAsync(DirectorCreateDto dto);
    Task<TokenResponseDto> LoginAsync(DIrectorLoginDto dto);
    Task<TokenResponseDto> LoginWithRefreshTokenAsync(string token);
    Task UpdatePrfileAsync(DirectorUpdateDto dto);
    Task UpdateProfileAdminAsync(string userName, DirectorUpdateAdminDto dto);
    Task<ICollection<DirectorWithRoles>> GetAllAsync(bool tekeAll);
    Task<DirectorWithRoles> GetByIdAsync(string id, bool takeAll);
    Task DeleteAsync(string userName);
    Task SoftDeleteAsync(string id);
    Task RevertSoftDeleteAsync(string id);
    Task AddRole(AddRoleDto dto);
    Task RemoveRole(RemoveRoleDto dto);
    Task SignOut();
}
