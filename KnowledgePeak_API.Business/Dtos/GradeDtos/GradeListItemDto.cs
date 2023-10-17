using KnowledgePeak_API.Business.Dtos.LessonDtos;
using KnowledgePeak_API.Business.Dtos.StudentDtos;
using KnowledgePeak_API.Business.Dtos.TeacherDtos;
using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.Dtos.GradeDtos;

public record GradeListItemDto
{
    public int Id { get; set; }
    public DateTime GradeDate { get; set; }
    public TeacherInfoDto Teacher { get; set; }
    public StudentSingleItemDto Student { get; set; }
    public LessonInfoDto Lesson { get; set; }
    public double Point { get; set; }
    public string Review { get; set; }
}
