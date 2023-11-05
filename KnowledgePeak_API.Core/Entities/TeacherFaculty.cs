using KnowledgePeak_API.Core.Entities.Commons;

namespace KnowledgePeak_API.Core.Entities;

public class TeacherFaculty : BaseEntity
{
    public Teacher? Teacher { get; set; }
    public string? TeacherId { get; set; }
    public Faculty? Faculty { get; set; }
    public int? FacultyId { get; set; }
}
