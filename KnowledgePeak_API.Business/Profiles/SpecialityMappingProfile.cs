using AutoMapper;
using KnowledgePeak_API.Business.Dtos.SpecialityDtos;
using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.Profiles;

public class SpecialityMappingProfile : Profile
{
    public SpecialityMappingProfile()
    {
        CreateMap<SpecialityCreateDto, Speciality>();
        CreateMap<SpecialityUpdateDto, Speciality>();
        CreateMap<SpecialityCreateDtoValidator, Speciality>().ReverseMap();
        CreateMap<Speciality, SpecialityDetailDto>();
        CreateMap<Speciality, SpecialityListItemDto>();
    }
}
