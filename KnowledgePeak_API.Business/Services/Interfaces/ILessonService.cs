using KnowledgePeak_API.Business.Dtos.LessonDtos;

namespace KnowledgePeak_API.Business.Services.Interfaces;

public interface ILessonService
{
    Task<IEnumerable<LessonListItemDto>> GetAllAsync(bool takeAll);
    Task<LessonDetailDto> GetByIdAsync(int id, bool takekAll);
    Task CreateAsync(LessonCreateDto dto);
    Task UpdateAsync(int id, LessonUpdateDto dto);
    Task DeleteAsync(int id);
    Task SoftDeleteAsync(int id);
    Task RevertSoftDeleteAsync(int id);
    Task<int> LessonCount();
}
