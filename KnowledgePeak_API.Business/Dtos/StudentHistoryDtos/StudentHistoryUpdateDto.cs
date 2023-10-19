using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.Dtos.StudentHistoryDtos;

public record StudentHistoryUpdateDto
{
    public Grade Grade { get; set; }
}
