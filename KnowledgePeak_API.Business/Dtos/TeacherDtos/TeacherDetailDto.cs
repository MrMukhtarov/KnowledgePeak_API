using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.Dtos.TeacherDtos;

public record TeacherDetailDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? ImageUrl { get; set; }
    public string Email { get; set; }
    public double Age { get; set; }
}
