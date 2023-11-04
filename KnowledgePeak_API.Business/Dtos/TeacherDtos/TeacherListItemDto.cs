using KnowledgePeak_API.Business.Dtos.ClassScheduleDtos;
using KnowledgePeak_API.Business.Dtos.FacultyDtos;
using KnowledgePeak_API.Business.Dtos.GradeDtos;
using KnowledgePeak_API.Business.Dtos.LessonDtos;
using KnowledgePeak_API.Business.Dtos.SpecialityDtos;
using KnowledgePeak_API.Core.Enums;

namespace KnowledgePeak_API.Business.Dtos.TeacherDtos;

public record TeacherListItemDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Description { get; set; }
    public string UserName { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsDeleted { get; set; }
    public double Salary { get; set; }
    public string Email { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Gender Gender { get; set; }
    public IEnumerable<string> Roles { get; set; }
    public ICollection<LessonInfoDto> Lessons { get; set; }
    public ICollection<SpecialityInfoDto> Specialities { get; set; }
    public ICollection<FacultyInfoDto> Faculties { get; set; }
    public ICollection<ClassScheduleTeacherDto> ClassSchedules { get; set; }
    public ICollection<GradeDetailDto> Grades { get; set; }
}
