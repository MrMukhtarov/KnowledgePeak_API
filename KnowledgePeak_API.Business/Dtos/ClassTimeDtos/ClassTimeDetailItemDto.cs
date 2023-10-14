namespace KnowledgePeak_API.Business.Dtos.ClassTimeDtos;

public record ClassTimeDetailItemDto
{
    public int Id { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public bool IsDelete { get; set; }
}
