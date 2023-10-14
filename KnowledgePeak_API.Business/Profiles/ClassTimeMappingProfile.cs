using AutoMapper;
using KnowledgePeak_API.Business.Dtos.ClassTimeDtos;
using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.Profiles;

internal class ClassTimeMappingProfile : Profile
{
    public ClassTimeMappingProfile()
    {
        CreateMap<ClassTime, ClassTimeDetailItemDto>();
        CreateMap<ClassTime, ClassTimeListItemDto>();
        CreateMap<ClassTImeCreateDto, ClassTime>();
        CreateMap<ClassTimeUpdateDto, ClassTime>();
    }
}
