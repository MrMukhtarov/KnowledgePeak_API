using Microsoft.AspNetCore.Identity;

namespace KnowledgePeak_API.Business.Dtos.DirectorDtos;

public record DirectorWithRoles
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string UserName { get; set; }
    public string? ImageUrl { get; set; }
    public IEnumerable<string> Roles { get; set; }
}
