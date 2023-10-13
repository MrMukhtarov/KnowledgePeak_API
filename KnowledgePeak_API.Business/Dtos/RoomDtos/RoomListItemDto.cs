using KnowledgePeak_API.Business.Dtos.FacultyDtos;

namespace KnowledgePeak_API.Business.Dtos.RoomDtos;

public record RoomListItemDto
{
    public int Id { get; set; }
    public string RoomNumber { get; set; }
    public int Capacity { get; set; }
    public bool IsDeleted { get; set; }
    public FacultyInfoDto Faculty { get; set; }
}
