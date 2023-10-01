using AutoMapper;
using KnowledgePeak_API.Business.Dtos.FacultyDtos;
using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.Profiles;

public class FacultyMappingProfile : Profile
{
    public FacultyMappingProfile()
    {
        CreateMap<FacultyCreateDto, Faculty>();
        CreateMap<FacultyUpdateDto, Faculty>();
        CreateMap<Faculty, FacultyListItemDto>();
        CreateMap<Faculty, FacultyDetailDto>();
    }
}
