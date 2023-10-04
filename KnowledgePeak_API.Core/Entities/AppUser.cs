using KnowledgePeak_API.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace KnowledgePeak_API.Core.Entities;

public class AppUser : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? ImageUrl { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }
}
