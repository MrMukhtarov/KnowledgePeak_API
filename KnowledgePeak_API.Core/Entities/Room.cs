using KnowledgePeak_API.Core.Entities.Commons;

namespace KnowledgePeak_API.Core.Entities;

public class Room : BaseEntity
{
    public string RoomNumber { get; set; }
    public int Capacity { get; set; }
    public bool IsEmpty { get; set; }
    public Faculty Faculty { get; set; }
    public int FacultyId { get; set; }
}
