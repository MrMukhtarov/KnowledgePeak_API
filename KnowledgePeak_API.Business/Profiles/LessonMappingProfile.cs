using AutoMapper;
using KnowledgePeak_API.Business.Dtos.LessonDtos;
using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.Profiles;

public class LessonMappingProfile : Profile
{
    public LessonMappingProfile()
    {
        CreateMap<LessonCreateDto, Lesson>();
        CreateMap<LessonUpdateDto, Lesson>();
        CreateMap<Lesson, LessonDetailDto>();
        CreateMap<Lesson, LessonListItemDto>();
    }
}
