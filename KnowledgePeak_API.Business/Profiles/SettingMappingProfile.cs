using AutoMapper;
using KnowledgePeak_API.Business.Dtos.SettingDtos;
using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.Profiles;

public class SettingMappingProfile : Profile
{
    public SettingMappingProfile()
    {
        CreateMap<SettingUpdateDto, Setting>();
        CreateMap<SettingCreateDto, Setting>();
        CreateMap<Setting, SettingDetailDto>();
    }
}
