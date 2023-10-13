namespace KnowledgePeak_API.Business.Dtos.TutorDtos;

public record TutorInfoDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsDeleted { get; set; }
}
