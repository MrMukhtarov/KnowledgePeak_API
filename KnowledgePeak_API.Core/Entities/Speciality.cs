using KnowledgePeak_API.Core.Entities.Commons;

namespace KnowledgePeak_API.Core.Entities;

public class Speciality : BaseEntity
{
    public string Name { get; set; }
    public string ShortName { get; set; }
    public Faculty? Faculty { get; set; }
    public int? FacultyId { get; set; }
    public DateTime CreateTime { get; set; }
    public List<LessonSpeciality> LessonSpecialities { get; set; }
    public ICollection<Group> Groups { get; set; }
    public ICollection<TeacherSpeciality> TeacherSpecialities { get; set; }
}
