using KnowledgePeak_API.Business.Dtos.TeacherDtos;
using KnowledgePeak_API.Business.Dtos.TokenDtos;

namespace KnowledgePeak_API.Business.Services.Interfaces;

public interface ITeacherService
{
    Task CreateAsync(TeacherCreateDto dto);
    Task<TokenResponseDto> Login(TeacherLoginDto dto);
    Task AddFaculty(TeacherAddFacultyDto dto, string userName);
    Task AddSpeciality(TeacherAddSpecialitiyDto dto, string userName);
    Task AddLesson(TeacherAddLessonDto dto,  string userName);
}
