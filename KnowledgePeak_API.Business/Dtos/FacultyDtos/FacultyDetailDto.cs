namespace KnowledgePeak_API.Business.Dtos.FacultyDtos;

public record FacultyDetailDto
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public DateTime CreateTime { get; set; }
}
