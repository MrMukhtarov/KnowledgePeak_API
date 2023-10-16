using AutoMapper;
using KnowledgePeak_API.Business.Dtos.ClassScheduleDtos;
using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.Profiles;

public class ClassScheduleMappingProfile : Profile
{
    public ClassScheduleMappingProfile()
    {
        CreateMap<ClassScheduleCreateDto, ClassSchedule>();
        CreateMap<ClassScheduleUpdateDto, ClassSchedule>();
        CreateMap<ClassSchedule, ClassScheduleListItemDto>();
        CreateMap<ClassSchedule, ClassScheduleDetailDto>();
        CreateMap<ClassSchedule, ClassScheduleTeacherDto>();
        CreateMap<ClassSchedule, ClassSchedulesGroupDto>();
        CreateMap<ClassSchedule, ClassScheduleStudentDto>();
        CreateMap<ClassSchedule, ClassScheduleTutorDto>();
    }
}
