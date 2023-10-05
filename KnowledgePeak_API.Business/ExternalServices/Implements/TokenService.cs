using KnowledgePeak_API.Business.Dtos.TokenDtos;
using KnowledgePeak_API.Business.ExternalServices.Interfaces;
using KnowledgePeak_API.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KnowledgePeak_API.Business.ExternalServices.Implements;

public class TokenService : ITokenService
{
    readonly IConfiguration _configuration;
    readonly UserManager<Director> _userManager;

    public TokenService(IConfiguration configuration, UserManager<Director> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    public TokenResponseDto CreateDirectorToken(Director director, int expires = 60)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, director.UserName),
            new Claim(ClaimTypes.NameIdentifier, director.Id),
            new Claim(ClaimTypes.Email, director.Email),
            new Claim(ClaimTypes.GivenName, director.Name),
            new Claim(ClaimTypes.Surname, director.Surname)
        };

        foreach (var userRole in _userManager.GetRolesAsync(director).Result)
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration["Jwt:SigninKey"]));
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtSecurity = new JwtSecurityToken
            (
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                DateTime.UtcNow.AddHours(4),
                DateTime.UtcNow.AddHours(4).AddMinutes(expires),
                credentials
            );
        JwtSecurityTokenHandler jwtHandler = new JwtSecurityTokenHandler();
        string token = jwtHandler.WriteToken(jwtSecurity);
        return new()
        {
            Token = token,
            Expires = jwtSecurity.ValidTo,
            Username = director.UserName,
        };
    }
}
