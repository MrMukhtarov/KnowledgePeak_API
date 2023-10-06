using KnowledgePeak_API.Business.Dtos.TokenDtos;
using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.ExternalServices.Interfaces;

public interface ITokenService
{
    TokenResponseDto CreateDirectorToken(Director director, int expires = 60);
    TokenResponseDto CreateTeacherToken(Teacher teacher, int expires = 60);
    string CreateRefreshToken();
}
