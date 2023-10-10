using AutoMapper;
using KnowledgePeak_API.Business.Dtos.GroupDtos;
using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.Profiles;

public class GroupMappingProfile : Profile
{
    public GroupMappingProfile()
    {
        CreateMap<GroupCreateDto, Group>();
        CreateMap<GroupUpdateDto, Group>();
        CreateMap<GroupAddStudentDto, Group>().ReverseMap();
        CreateMap<Group, GroupDetailDto>();
        CreateMap<Group, GroupListItemDto>();
    }
}
