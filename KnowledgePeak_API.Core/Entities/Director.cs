using KnowledgePeak_API.Core.Enums;

namespace KnowledgePeak_API.Core.Entities;

public class Director : AppUser
{
    public string Description { get; set; }
    public bool IsDeleted { get; set; }
    public decimal Salary { get; set; }
    public Status Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public University? University { get; set; }
    public int? UniversityId { get; set; }
}
