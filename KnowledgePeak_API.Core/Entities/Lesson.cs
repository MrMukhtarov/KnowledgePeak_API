using KnowledgePeak_API.Core.Entities.Commons;

namespace KnowledgePeak_API.Core.Entities;

public class Lesson : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public IEnumerable<LessonSpeciality> LessonSpecialities { get; set; }
    public ICollection<TeacherLesson> TeacherLessons { get; set; }
    public ICollection<ClassSchedule> ClassSchedules { get; set; }
    public ICollection<Grade> Grades { get; set; }
}
