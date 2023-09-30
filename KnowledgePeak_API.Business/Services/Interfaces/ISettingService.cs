using KnowledgePeak_API.Business.Dtos.SettingDtos;

namespace KnowledgePeak_API.Business.Services.Interfaces;

public interface ISettingService
{
    Task<IEnumerable<SettingDetailDto>> GetAllAsync();
    Task CreateAsync(SettingCreateDto dto);
    Task UpdateAsync(SettingUpdateDto dto, int id);
}
