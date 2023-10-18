using KnowledgePeak_API.Business.Dtos.GradeDtos;
using System.Collections;

namespace KnowledgePeak_API.Business.Dtos.StudentHistoryDtos;

public record StudentHistoryListItemDto
{
    public int Id { get; set; }
    public DateTime HistoryDate { get; set; }
    public ICollection<GradeListItemDto> Grade { get; set; }
}
