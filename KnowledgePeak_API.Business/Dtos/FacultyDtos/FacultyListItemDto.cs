using KnowledgePeak_API.Business.Dtos.GroupDtos;
using KnowledgePeak_API.Business.Dtos.SpecialityDtos;

namespace KnowledgePeak_API.Business.Dtos.FacultyDtos;

public record FacultyListItemDto
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public DateTime CreateTime { get; set; }
    public ICollection<SpecialityListItemDto> Specialities { get; set; }
}
