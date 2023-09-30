using AutoMapper;
using KnowledgePeak_API.Business.Dtos.UniversityDtos;
using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.Profiles;

public class UniversityMappingProfile : Profile
{
    public UniversityMappingProfile()
    {
        CreateMap<UniversityUpdateDto, University>();
        CreateMap<University, UniversityDetailDto>();
    }
}
