namespace KnowledgePeak_API.Business.Dtos.LessonDtos;

public record LessonDetailDto
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
}
