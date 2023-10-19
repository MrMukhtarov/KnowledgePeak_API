using AutoMapper;
using KnowledgePeak_API.Business.Dtos.StudentDtos;
using KnowledgePeak_API.Business.Dtos.StudentHistoryDtos;
using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.Profiles;

public class StudentHistoryMappingProfile : Profile
{
    public StudentHistoryMappingProfile()
    {
        CreateMap<StudentHistory, StudentHistoryListItemDto>().ReverseMap();
        CreateMap<StudentHistory, StudentHistoryInfoDto>().ReverseMap();
        CreateMap<StudentHistory, StudentHistoryDetailDto>();
        CreateMap<StudentHistoryCreateDto, StudentHistory>();
        CreateMap<StudentHistoryUpdateDto, StudentHistory>();
    }
}
