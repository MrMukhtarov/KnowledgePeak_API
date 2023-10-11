using KnowledgePeak_API.Core.Enums;

namespace KnowledgePeak_API.Core.Entities;

public class Tutor : AppUser
{
    public double Salary { get; set; }
    public Status Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Speciality? Speciality { get; set; }
    public int? SpecialityId { get; set; }
    public ICollection<Group> Groups { get; set; }
    public bool IsDeleted { get; set; }
}
