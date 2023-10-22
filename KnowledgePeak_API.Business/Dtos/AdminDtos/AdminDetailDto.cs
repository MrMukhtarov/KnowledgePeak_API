using KnowledgePeak_API.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Dtos.AdminDtos;

public record AdminDetailDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public string? ImageFile { get; set; }
    public Gender Gender { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public IEnumerable<string> Roles { get; set; }
}
