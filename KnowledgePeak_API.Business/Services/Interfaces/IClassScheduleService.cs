using KnowledgePeak_API.Business.Dtos.ClassScheduleDtos;

namespace KnowledgePeak_API.Business.Services.Interfaces;

public interface IClassScheduleService
{
    Task CreateAsync(ClassScheduleCreateDto dto);
}
