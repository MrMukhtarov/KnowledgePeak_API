using KnowledgePeak_API.Business.Dtos.RoleDtos;
using KnowledgePeak_API.Business.Dtos.TeacherDtos;
using KnowledgePeak_API.Business.Dtos.TokenDtos;

namespace KnowledgePeak_API.Business.Services.Interfaces;

public interface ITeacherService
{
    Task CreateAsync(TeacherCreateDto dto);
    Task<TokenResponseDto> Login(TeacherLoginDto dto);
    Task UpdateAsync(TeacherUpdateProfileDto dto);
    Task<ICollection<TeacherListItemDto>> GetAllAsync(bool takeAll);
    Task AddRoleAsync(AddRoleDto dto);
    Task RemoveRoleAsync(RemoveRoleDto dto);
    Task<TokenResponseDto> LoginWithRefreshTokenAsync(string token);
    Task AddFaculty(TeacherAddFacultyDto dto, string userName);
    Task AddSpeciality(TeacherAddSpecialitiyDto dto, string userName);
    Task AddLesson(TeacherAddLessonDto dto,  string userName);
    Task SoftDeleteAsync(string userName);
    Task RevertSoftDeleteAsync(string userName);
}
