using KnowledgePeak_API.Core.Entities.Commons;

namespace KnowledgePeak_API.Core.Entities;

public class LessonSpeciality : BaseEntity
{
    public Lesson Lesson { get; set; }
    public int LessonId { get; set; }
    public Speciality Speciality { get; set; }
    public int SpecialityId { get; set; }
}
