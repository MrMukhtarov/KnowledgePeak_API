using AutoMapper;
using KnowledgePeak_API.Business.Dtos.TutorDtos;
using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.Profiles;

public class TutorMappingProfile : Profile
{
    public TutorMappingProfile()
    {
        CreateMap<TutorCreateDto, Tutor>();
        CreateMap<Tutor, TutorListItemDto>().ReverseMap();
        CreateMap<Tutor, TutorDetailDto>().ReverseMap();
        CreateMap<TutorAddGroupDto, Tutor>().ReverseMap();
        CreateMap<TutorAddSpecialityDto, Tutor>().ReverseMap();
        CreateMap<TutorUpdateProfileDto, Tutor>();
        CreateMap<TutorUpdateProfileFromAdminDto, Tutor>();
    }
}
