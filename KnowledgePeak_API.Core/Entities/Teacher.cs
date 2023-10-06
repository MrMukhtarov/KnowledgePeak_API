using KnowledgePeak_API.Core.Enums;

namespace KnowledgePeak_API.Core.Entities;

public class Teacher : AppUser
{
    public string Description { get; set; }
    public double Salary { get; set; }
    public Status Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsDeleted { get; set; }
    public ICollection<TeacherFaculty> TeacherFaculties { get; set; }
    public ICollection<TeacherSpeciality> TeacherSpecialities { get; set; }
    public ICollection<TeacherLesson> TeacherLessons { get; set; }
}
