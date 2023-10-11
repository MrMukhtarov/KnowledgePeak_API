using KnowledgePeak_API.Business.Dtos.StudentDtos;

namespace KnowledgePeak_API.Business.Dtos.GroupDtos;

public record GroupListItemDto
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; }
    public int Limit { get; set; }
    public ICollection<StudentDetailDto> Students { get; set; }
}
