namespace KnowledgePeak_API.Business.Dtos.RoomDtos;

public record RoomInfoDto
{
    public string RoomNumber { get; set; }
    public int Capacity { get; set; }
    public bool IsEmpty { get; set; }
}
