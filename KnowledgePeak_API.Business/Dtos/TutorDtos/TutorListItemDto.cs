using KnowledgePeak_API.Business.Dtos.GroupDtos;

namespace KnowledgePeak_API.Business.Dtos.TutorDtos;

public record TutorListItemDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsDeleted { get; set; }
    public IEnumerable<string> Roles { get; set; }
    public ICollection<GroupSingleDetailDto> Groups { get; set; }
}
