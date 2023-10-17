using KnowledgePeak_API.Core.Enums;

namespace KnowledgePeak_API.Core.Entities;

public class Student : AppUser
{
    public Status Status { get; set; }
    public Group? Group { get; set; }
    public int? GroupId { get; set; }
    public double? Avarage { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int Course { get; set; }
    public bool IsDeleted { get; set; }
    public ICollection<Grade> Grades { get; set; }
}
