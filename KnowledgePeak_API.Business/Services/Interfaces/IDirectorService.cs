using KnowledgePeak_API.Business.Dtos.DirectorDtos;

namespace KnowledgePeak_API.Business.Services.Interfaces;

public interface IDirectorService
{
    Task CreateAsync(DirectorCreateDto dto);
    Task SoftDeleteAsync(string id);
    Task RevertSoftDeleteAsync(string id);
}
