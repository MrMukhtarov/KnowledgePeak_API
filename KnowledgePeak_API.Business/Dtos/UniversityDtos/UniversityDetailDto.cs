using KnowledgePeak_API.Business.Dtos.DirectorDtos;

namespace KnowledgePeak_API.Business.Dtos.UniversityDtos;

public record UniversityDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DirectorWithRoles Director { get; set; }
}
