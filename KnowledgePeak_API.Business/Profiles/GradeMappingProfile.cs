using AutoMapper;
using KnowledgePeak_API.Business.Dtos.GradeDtos;
using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.Profiles;

public class GradeMappingProfile : Profile
{
    public GradeMappingProfile()
    {
        CreateMap<GradeCreateDto, Grade>();
        CreateMap<GradeUpdateDto, Grade>();
        CreateMap<Grade, GradeListItemDto>().ReverseMap();
        CreateMap<Grade, GradeDetailDto>();
    }
}
