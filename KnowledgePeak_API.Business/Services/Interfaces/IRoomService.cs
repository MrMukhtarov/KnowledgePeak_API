using KnowledgePeak_API.Business.Dtos.RoomDtos;

namespace KnowledgePeak_API.Business.Services.Interfaces;

public interface IRoomService
{
    Task<ICollection<RoomListItemDto>> GetAllAsync(bool takeAll);
    Task<RoomDetailItemDto> GetByIdAsync(int id, bool takeAll);
    Task CreateAsync(RoomCreateDto dto);
    Task UpdateAsync(int id, RoomUpdateDto dto);
    Task DeleteAsync(int id);
    Task SoftDeleteAsync(int id);
    Task RevertSoftDeleteAsync(int id);
}
