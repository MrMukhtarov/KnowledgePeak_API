using AutoMapper;
using KnowledgePeak_API.Business.Dtos.StudentDtos;
using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.Profiles;

public class StudentMappingProfile : Profile
{
    public StudentMappingProfile()
    {
        CreateMap<StudentCreateDto, Student>().ReverseMap();
        CreateMap<Student, StudentListItemDto>().ReverseMap();
        CreateMap<Student, StudentDetailDto>().ReverseMap();
        CreateMap<StudentUpdateDto, Student>().ReverseMap();
        CreateMap<StudentAdminUpdateDto, Student>().ReverseMap();
        CreateMap<Student, StudentSingleItemDto>().ReverseMap();
    }
}
