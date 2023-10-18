using KnowledgePeak_API.Business.Dtos.StudentHistoryDtos;

namespace KnowledgePeak_API.Business.Services.Interfaces;

public interface IStudentHistoryService
{
    Task<ICollection<StudentHistoryListItemDto>> GetAllAsync();
    Task<StudentHistoryDetailDto> GetByIdAsync(int id);
    Task CreateAsync(StudentHistoryCreateDto dto);
}
