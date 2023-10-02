using KnowledgePeak_API.Business.Dtos.LessonDtos;
using KnowledgePeak_API.Business.Services.Interfaces;

namespace KnowledgePeak_API.Business.Services.Implements;

public class LessonService : ILessonService
{
    public Task CreateAsync(LessonCreateDto dto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<LessonListItemDto>> GetAllAsync(bool takeAll)
    {
        throw new NotImplementedException();
    }

    public Task<LessonDetailDto> GetByIdAsync(int id, bool takekAll)
    {
        throw new NotImplementedException();
    }

    public Task RevertSoftDeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task SoftDeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(LessonUpdateDto dto)
    {
        throw new NotImplementedException();
    }
}
