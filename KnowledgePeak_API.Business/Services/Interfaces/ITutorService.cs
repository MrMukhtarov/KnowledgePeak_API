using KnowledgePeak_API.Business.Dtos.TokenDtos;
using KnowledgePeak_API.Business.Dtos.TutorDtos;

namespace KnowledgePeak_API.Business.Services.Interfaces;

public interface ITutorService
{
    Task CreateAsync(TutorCreateDto dto);
    Task<TokenResponseDto> LoginAsync(TutorLoginDto dto);
    Task<TokenResponseDto> LoginWithRefreshTokenAsync(string token);
}
