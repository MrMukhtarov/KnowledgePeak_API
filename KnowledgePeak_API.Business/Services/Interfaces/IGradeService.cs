using KnowledgePeak_API.Business.Dtos.GradeDtos;

namespace KnowledgePeak_API.Business.Services.Interfaces;

public interface IGradeService
{
    Task<ICollection<GradeListItemDto>> GetAllAsync();
    Task<GradeDetailDto> GetByIdAsyc(int id);
    Task CreateAsync(GradeCreateDto dto);
    Task UpdateAsync(GradeUpdateDto dto);
}
