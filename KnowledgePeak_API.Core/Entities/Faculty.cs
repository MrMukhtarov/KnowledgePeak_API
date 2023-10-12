using KnowledgePeak_API.Core.Entities.Commons;

namespace KnowledgePeak_API.Core.Entities;

public class Faculty : BaseEntity
{
    public string Name { get; set; }
    public string ShortName { get; set; }
    public DateTime CreateTime { get; set; }
    public ICollection<Speciality>? Specialities { get; set; }
    public ICollection<TeacherFaculty> TeacherFaculties { get; set; }
    public ICollection<Room> Rooms { get; set; }
}
