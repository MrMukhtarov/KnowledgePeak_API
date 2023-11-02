using KnowledgePeak_API.Core.Entities.Commons;

namespace KnowledgePeak_API.Core.Entities;

public class Group : BaseEntity
{
    public string Name { get; set; }
    public int Limit { get; set; }
    public Speciality? Speciality { get; set; }
    public int? SpecialityId { get; set; }
    public ICollection<Student>? Students { get; set; }
    public Tutor? Tutor { get; set; }
    public string? TutorId { get; set; }
    public ICollection<ClassSchedule> ClassSchedules { get; set; }
}
