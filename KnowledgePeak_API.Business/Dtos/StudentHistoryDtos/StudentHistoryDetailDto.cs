using KnowledgePeak_API.Business.Dtos.GradeDtos;

namespace KnowledgePeak_API.Business.Dtos.StudentHistoryDtos;

public record StudentHistoryDetailDto
{
    public int Id { get; set; }
    public DateTime HistoryDate { get; set; }
    public ICollection<GradeListItemDto> Grades { get; set; }
}
