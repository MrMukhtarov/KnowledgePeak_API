using KnowledgePeak_API.Business.Dtos.RoleDtos;
using KnowledgePeak_API.Business.Dtos.TokenDtos;
using KnowledgePeak_API.Business.Dtos.TutorDtos;

namespace KnowledgePeak_API.Business.Services.Interfaces;

public interface ITutorService
{
    Task CreateAsync(TutorCreateDto dto);
    Task<TokenResponseDto> LoginAsync(TutorLoginDto dto);
    Task<TokenResponseDto> LoginWithRefreshTokenAsync(string token);
    Task<ICollection<TutorListItemDto>> GetAllAsync(bool takeAll);
    Task<TutorDetailDto> GetByIdAsync(string userName, bool takeAll);
    Task UpdateProfileAsync(TutorUpdateProfileDto dto);
    Task UpdateProfileFromAdminAsync(TutorUpdateProfileFromAdminDto dto,string userName);
    Task AddGroup(TutorAddGroupDto dto);
    Task AddSpeciality(TutorAddSpecialityDto dto);
    Task AddRoleAsync(AddRoleDto dto);
    Task RemoveRole(RemoveRoleDto dto);
    Task SoftDeleteAsync(string userName);
    Task RevertSoftDeleteAsync(string userName);
}
