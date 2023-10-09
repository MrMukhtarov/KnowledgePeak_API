using KnowledgePeak_API.Business.Dtos.TokenDtos;
using KnowledgePeak_API.Business.ExternalServices.Interfaces;
using KnowledgePeak_API.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace KnowledgePeak_API.Business.ExternalServices.Implements;

public class TokenService : ITokenService
{
    readonly IConfiguration _configuration;
    readonly UserManager<Director> _userManager;
    readonly UserManager<Teacher> _teacher;
    readonly UserManager<Student> _student;

    public TokenService(IConfiguration configuration, UserManager<Director> userManager,
        UserManager<Teacher> teacher, UserManager<Student> student)
    {
        _configuration = configuration;
        _userManager = userManager;
        _teacher = teacher;
        _student = student;
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
        string refreshToken = CreateRefreshToken();
        var refreshTokenExpires = jwtSecurity.ValidTo.AddMinutes(expires / 3);
        director.RefreshToken = refreshToken;
        director.RefreshTokenExpiresDate = refreshTokenExpires;
        _userManager.UpdateAsync(director).Wait();
        return new()
        {
            Token = token,
            Expires = jwtSecurity.ValidTo,
            Username = director.UserName,
            RefreshToken = refreshToken,
            RefreshTokenExpires = refreshTokenExpires
        };
    }

    public string CreateRefreshToken()
    {
        byte[] bytes = new byte[32];
        var random = RandomNumberGenerator.Create();
        random.GetBytes(bytes);
        return Convert.ToBase64String(bytes);
    }

    public TokenResponseDto CreateStudentToken(Student student, int expires = 60)
    {
        List<Claim> claims = new List<Claim>()
        {
                new Claim(ClaimTypes.NameIdentifier, student.Id),
                new Claim(ClaimTypes.Name, student.UserName),
                new Claim(ClaimTypes.GivenName, student.Name),
                new Claim(ClaimTypes.Surname, student.Surname),
                new Claim(ClaimTypes.Email, student.Email)
        };
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SigninKey"]));
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
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        string token = tokenHandler.WriteToken(jwtSecurity);
        string refreshToken = CreateRefreshToken();
        var refreshTokenExpires = jwtSecurity.ValidTo.AddMinutes(expires / 3);
        student.RefreshToken = refreshToken;
        student.RefreshTokenExpiresDate = refreshTokenExpires;
        _student.UpdateAsync(student).Wait();
        return new()
        {
            Token = token,
            RefreshTokenExpires = refreshTokenExpires,
            Username = student.UserName,
            RefreshToken = refreshToken,
            Expires = jwtSecurity.ValidTo
        };
    }

    public TokenResponseDto CreateTeacherToken(Teacher teacher, int expires = 60)
    {
        List<Claim> claims = new List<Claim>()
        {
                new Claim(ClaimTypes.NameIdentifier,  teacher.Id),
                new Claim(ClaimTypes.Name, teacher.UserName),
                new Claim(ClaimTypes.GivenName, teacher.Name),
                new Claim(ClaimTypes.Surname, teacher.Surname),
                new Claim(ClaimTypes.Email, teacher.Email),
        };

        foreach (var userRole in _teacher.GetRolesAsync(teacher).Result)
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
            (_configuration["Jwt:SigninKey"]));

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
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        string token = handler.WriteToken(jwtSecurity);
        string refreshToken = CreateRefreshToken();
        var refreshTokenExpires = jwtSecurity.ValidTo.AddMinutes(expires / 3);
        teacher.RefreshToken = refreshToken;
        teacher.RefreshTokenExpiresDate = refreshTokenExpires;
        _teacher.UpdateAsync(teacher).Wait();
        return new()
        {
            Token = token,
            Expires = jwtSecurity.ValidTo,
            Username = teacher.UserName,
            RefreshToken = refreshToken,
            RefreshTokenExpires = refreshTokenExpires
        };
    }
}
