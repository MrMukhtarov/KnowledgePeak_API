using KnowledgePeak_API.Core.Entities.Commons;

namespace KnowledgePeak_API.Core.Entities;

public class TeacherLesson : BaseEntity
{
    public Teacher? Teacher { get; set; }
    public string? TeacherId { get; set; }
    public Lesson Lesson { get; set; }
    public int LessonId { get; set; }
}
