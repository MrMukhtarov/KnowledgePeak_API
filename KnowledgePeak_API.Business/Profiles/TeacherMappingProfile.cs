using AutoMapper;
using KnowledgePeak_API.Business.Dtos.TeacherDtos;
using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.Profiles;

public class TeacherMappingProfile : Profile
{
    public TeacherMappingProfile()
    {
        CreateMap<TeacherCreateDto, Teacher>();
        CreateMap<TeacherUpdateProfileDto, Teacher>();
        CreateMap<TeacherDetailDto, Teacher>().ReverseMap();
        CreateMap<TeacherAdminUpdateDto, Teacher>();
        CreateMap<TeacherAddFacultyDto, Teacher>();
        CreateMap<TeacherAddSpecialitiyDto, Teacher>();
        CreateMap<TeacherAddLessonDto, Teacher>();
    }
}
