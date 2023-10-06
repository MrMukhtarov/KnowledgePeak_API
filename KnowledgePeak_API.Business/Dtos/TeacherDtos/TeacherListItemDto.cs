using KnowledgePeak_API.Business.Dtos.FacultyDtos;
using KnowledgePeak_API.Business.Dtos.LessonDtos;
using KnowledgePeak_API.Business.Dtos.SpecialityDtos;

namespace KnowledgePeak_API.Business.Dtos.TeacherDtos;

public record TeacherListItemDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string UserName { get; set; }
    public string? ImageUrl { get; set; }
    public IEnumerable<string> Roles { get; set; }
    public ICollection<LessonInfoDto> Lessons { get; set; }
    public ICollection<SpecialityInfoDto> Specialities { get; set; }
    public ICollection<FacultyInfoDto> Faculties { get; set; }
}
