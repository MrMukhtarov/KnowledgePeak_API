using KnowledgePeak_API.Business.Dtos.GradeDtos;
using KnowledgePeak_API.Business.Dtos.LessonDtos;

namespace KnowledgePeak_API.Business.Dtos.StudentHistoryDtos;

public record StudentHistoryInfoDto
{
    public int Id { get; set; }
    public DateTime HistoryDate { get; set; }
    public LessonInfoDto Lesson { get; set; }
    public GradeInfoForStudentDto Grade { get; set; }
}
