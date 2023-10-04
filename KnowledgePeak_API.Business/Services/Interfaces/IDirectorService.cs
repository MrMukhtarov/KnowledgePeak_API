using KnowledgePeak_API.Business.Dtos.DirectorDtos;
using KnowledgePeak_API.Business.Dtos.TokenDtos;

namespace KnowledgePeak_API.Business.Services.Interfaces;

public interface IDirectorService
{
    Task CreateAsync(DirectorCreateDto dto);
    Task<TokenResponseDto> LoginAsync(DIrectorLoginDto dto);
    Task SoftDeleteAsync(string id);
    Task RevertSoftDeleteAsync(string id);
}
