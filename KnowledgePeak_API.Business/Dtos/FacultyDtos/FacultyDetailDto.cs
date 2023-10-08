using KnowledgePeak_API.Business.Dtos.SpecialityDtos;
using KnowledgePeak_API.Business.Dtos.TeacherDtos;

namespace KnowledgePeak_API.Business.Dtos.FacultyDtos;

public record FacultyDetailDto
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public DateTime CreateTime { get; set; }
    public ICollection<SpecialityListItemDto> Specialities { get; set; }
    public List<TeacherFacultyDto> TeacherFaculties { get; set; }
}
