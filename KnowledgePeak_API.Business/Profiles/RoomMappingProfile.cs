using AutoMapper;
using KnowledgePeak_API.Business.Dtos.RoomDtos;
using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.Profiles;

public class RoomMappingProfile : Profile
{
    public RoomMappingProfile()
    {
        CreateMap<Room, RoomDetailItemDto>();
        CreateMap<Room, RoomListItemDto>();
        CreateMap<RoomCreateDto, Room>();
        CreateMap<RoomUpdateDto, Room>();
        CreateMap<Room, RoomInfoDto>();
    }
}
