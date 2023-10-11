using AutoMapper;
using KnowledgePeak_API.Business.Constants;
using KnowledgePeak_API.Business.Dtos.TutorDtos;
using KnowledgePeak_API.Business.Exceptions.Commons;
using KnowledgePeak_API.Business.Exceptions.File;
using KnowledgePeak_API.Business.Extensions;
using KnowledgePeak_API.Business.ExternalServices.Interfaces;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using KnowledgePeak_API.Core.Enums;
using KnowledgePeak_API.Business.Dtos.TokenDtos;
using KnowledgePeak_API.Business.Exceptions.Token;

namespace KnowledgePeak_API.Business.Services.Implements;

public class TutorService : ITutorService
{
    readonly UserManager<Tutor> _userManager;
    readonly UserManager<AppUser> _appUserManager;
    readonly IMapper _mapper;
    readonly IFileService _file;
    readonly ITokenService _token;

    public TutorService(UserManager<Tutor> userManager, UserManager<AppUser> appUserManager,
        IMapper mapper, IFileService file, ITokenService token)
    {
        _userManager = userManager;
        _appUserManager = appUserManager;
        _mapper = mapper;
        _file = file;
        _token = token;
    }

    public async Task CreateAsync(TutorCreateDto dto)
    {
        if (await _appUserManager.Users.AnyAsync(u => u.UserName == dto.UserName || u.Email == dto.Email))
            throw new UserExistException();

        var user = _mapper.Map<Tutor>(dto);

        if (dto.ImageFile != null)
        {
            if (!dto.ImageFile.IsSizeValid(3)) throw new FileSizeInvalidException();
            if (!dto.ImageFile.IsTypeValid("image")) throw new FileTypeInvalidExveption();
            user.ImageUrl = await _file.UploadAsync(dto.ImageFile, RootConstants.TutorImageRoot);
        }
        user.Status = Status.Work;
        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded) throw new LoginFailedException<Tutor>();
    }

    public async Task<TokenResponseDto> LoginAsync(TutorLoginDto dto)
    {
        if (string.IsNullOrEmpty(dto.UserName)) throw new ArgumentNullException();
        var user = await _userManager.FindByNameAsync(dto.UserName);
        if (user == null) throw new UserNotFoundException<Tutor>();

        var res = await _userManager.CheckPasswordAsync(user, dto.Password);
        if (res == false) throw new LoginFailedException<Tutor>();

        return _token.CreateTutorToken(user);
    }

    public async Task<TokenResponseDto> LoginWithRefreshTokenAsync(string token)
    {
        if(string.IsNullOrEmpty(token)) throw new ArgumentNullException();
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshToken == token);
        if (user == null) throw new UserNotFoundException<Tutor>();

        if (user.RefreshTokenExpiresDate < DateTime.UtcNow.AddHours(4)) throw new
                RefreshTokenExpiresDateException();
        return _token.CreateTutorToken(user);
    }
}
