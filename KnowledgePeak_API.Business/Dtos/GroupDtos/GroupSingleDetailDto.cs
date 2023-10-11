namespace KnowledgePeak_API.Business.Dtos.GroupDtos;

public record GroupSingleDetailDto
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; }
    public int Limit { get; set; }
}
