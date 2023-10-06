using KnowledgePeak_API.Core.Entities.Commons;

namespace KnowledgePeak_API.Core.Entities;

public class TeacherSpeciality : BaseEntity
{
    public Teacher Teacher { get; set; }
    public string TeacherId { get; set; }
    public Speciality Speciality { get; set; }
    public int SpecialityId { get; set;}
}
