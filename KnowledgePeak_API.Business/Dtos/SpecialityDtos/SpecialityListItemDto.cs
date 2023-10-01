using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.Dtos.SpecialityDtos;

public record SpecialityListItemDto
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public int? FacultyId { get; set; }
    public DateTime CreateTime { get; set; }
}
