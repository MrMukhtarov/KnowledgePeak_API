using KnowledgePeak_API.Core.Entities.Commons;

namespace KnowledgePeak_API.Core.Entities;

public class Grade : BaseEntity
{
    public DateTime GradeDate { get; set; }
    public Teacher Teacher { get; set; }
    public string TeacherId { get; set; }
    public Student Student { get; set; }
    public string StudentId { get; set; }
    public Lesson Lesson { get; set; }
    public int LessonId { get; set; }
    public double Point { get; set; }
    public string Review { get; set; }
}
