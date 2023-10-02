using KnowledgePeak_API.Business.Dtos.GroupDtos;
using KnowledgePeak_API.Business.Dtos.LessonDtos;

namespace KnowledgePeak_API.Business.Dtos.SpecialityDtos;

public record SpecialityDetailDto
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public int? FacultyId { get; set; }
    public DateTime CreateTime { get; set; }
    public IEnumerable<SpecialityLessonDto> LessonSpecialities { get; set; }
    public IEnumerable<GroupListItemDto> Groups { get; set; }

}
