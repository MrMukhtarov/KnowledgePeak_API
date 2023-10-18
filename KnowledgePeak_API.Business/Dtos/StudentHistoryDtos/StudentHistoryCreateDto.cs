using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.Dtos.StudentHistoryDtos;

public record StudentHistoryCreateDto
{
    public DateTime HistoryDate { get; set; }
    public Grade Grade { get; set; }
    public string Studentid { get; set; }
}
