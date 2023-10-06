using KnowledgePeak_API.Business.Dtos.TeacherDtos;

namespace KnowledgePeak_API.Business.Services.Interfaces;

public interface ITeacherService
{
    Task CreateAsync(TeacherCreateDto dto);
}
