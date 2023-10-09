using KnowledgePeak_API.Business.Dtos.RoleDtos;
using KnowledgePeak_API.Business.Dtos.StudentDtos;
using KnowledgePeak_API.Business.Dtos.TokenDtos;

namespace KnowledgePeak_API.Business.Services.Interfaces;

public interface IStudentService
{
    Task CreateAsync(StudentCreateDto dto);
    Task<TokenResponseDto> LoginAsync(StudentLoginDto dto);
    Task<TokenResponseDto> LoginWithRefreshToken(string token);
    Task<StudentListItemDto> GetAll();
    Task AddRole(AddRoleDto dto);
    Task CheckGraduate();
}
