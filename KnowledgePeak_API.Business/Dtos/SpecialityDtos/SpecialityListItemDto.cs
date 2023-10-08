using KnowledgePeak_API.Business.Dtos.GroupDtos;
using KnowledgePeak_API.Business.Dtos.LessonDtos;
using KnowledgePeak_API.Business.Dtos.TeacherDtos;

namespace KnowledgePeak_API.Business.Dtos.SpecialityDtos;

public record SpecialityListItemDto
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public int? FacultyId { get; set; }
    public DateTime CreateTime { get; set; }
    public IEnumerable<LessonListItemDto> Lesson { get; set; }
    public IEnumerable<GroupListItemDto> Groups { get; set; }
    public List<TeacherDetailDto> Teacher { get; set; }
}
