using AutoMapper;
using KnowledgePeak_API.Business.Dtos.AdminDtos;
using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.Profiles;

public class AdminMappingProfile : Profile
{
    public AdminMappingProfile()
    {
        CreateMap<AdminCreateDto, Admin>();
        CreateMap<AdminUpdateDto, Admin>();
        CreateMap<Admin, AdminListItemDto>();
        CreateMap<Admin, AdminDetailDto>();
    }
}
