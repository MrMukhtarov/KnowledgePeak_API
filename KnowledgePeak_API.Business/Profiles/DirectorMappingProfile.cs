using AutoMapper;
using KnowledgePeak_API.Business.Dtos.DirectorDtos;
using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.Profiles;

public class DirectorMappingProfile : Profile
{
    public DirectorMappingProfile()
    {
        CreateMap<DirectorCreateDto, Director>();
        CreateMap<DirectorUpdateDto, Director>();
        CreateMap<Director, DirectorWithRoles>();
        CreateMap<DirectorUpdateAdminDto, Director>();
    }
}
