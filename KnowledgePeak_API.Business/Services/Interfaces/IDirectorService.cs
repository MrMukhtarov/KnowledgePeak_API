using KnowledgePeak_API.Business.Dtos.DirectorDtos;
using KnowledgePeak_API.Business.Dtos.RoleDtos;
using KnowledgePeak_API.Business.Dtos.TokenDtos;
using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.Services.Interfaces;

public interface IDirectorService
{
    Task CreateAsync(DirectorCreateDto dto);
    Task<TokenResponseDto> LoginAsync(DIrectorLoginDto dto);
    Task<TokenResponseDto> LoginWithRefreshTokenAsync(string token);
    Task UpdatePrfileAsync(DirectorUpdateDto dto);
    Task<ICollection<DirectorWithRoles>> GetAllAsync();
    Task SoftDeleteAsync(string id);
    Task RevertSoftDeleteAsync(string id);
    Task AddRole(AddRoleDto dto);
    Task RemoveRole(RemoveRoleDto dto);
}
