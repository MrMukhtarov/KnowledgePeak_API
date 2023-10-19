using AutoMapper;
using KnowledgePeak_API.Business.Constants;
using KnowledgePeak_API.Business.Dtos.DirectorDtos;
using KnowledgePeak_API.Business.Dtos.RoleDtos;
using KnowledgePeak_API.Business.Dtos.TokenDtos;
using KnowledgePeak_API.Business.Exceptions.Commons;
using KnowledgePeak_API.Business.Exceptions.Director;
using KnowledgePeak_API.Business.Exceptions.File;
using KnowledgePeak_API.Business.Exceptions.Role;
using KnowledgePeak_API.Business.Exceptions.Token;
using KnowledgePeak_API.Business.Extensions;
using KnowledgePeak_API.Business.ExternalServices.Interfaces;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using KnowledgePeak_API.Core.Enums;
using KnowledgePeak_API.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace KnowledgePeak_API.Business.Services.Implements;

public class DirectorService : IDirectorService
{
    readonly UserManager<Director> _userManager;
    readonly UserManager<AppUser> _user;
    readonly IMapper _mapper;
    readonly IFileService _fileService;
    readonly IUniversityRepository _uniRepo;
    readonly ITokenService _tokenService;
    readonly RoleManager<IdentityRole> _roleManager;
    readonly IHttpContextAccessor _contextAccessor;
    readonly string userId;
    readonly SignInManager<Director> _signinManager;
    readonly IConfiguration _config;
    readonly IEmailSender _emailSender;

    public DirectorService(UserManager<Director> userManager, IMapper mapper,
        IFileService fileService, IUniversityRepository uniRepo, ITokenService tokenService,
        RoleManager<IdentityRole> roleManager, IHttpContextAccessor contextAccessor,
        UserManager<AppUser> user, SignInManager<Director> signinManager, IConfiguration config,
        IEmailSender emailSender)
    {
        _userManager = userManager;
        _mapper = mapper;
        _fileService = fileService;
        _uniRepo = uniRepo;
        _tokenService = tokenService;
        _roleManager = roleManager;
        _contextAccessor = contextAccessor;
        userId = _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        _user = user;
        _signinManager = signinManager;
        _config = config;
        _emailSender = emailSender;
    }

    public async Task CreateAsync(DirectorCreateDto dto)
    {
        //var directors = await _userManager.Users.FirstOrDefaultAsync(d => d.IsDeleted == false);
        //if (directors != null) throw new ThereIsaDirectorInTheSystemException();

        if (dto.ImageFile != null)
        {
            if (!dto.ImageFile.IsSizeValid(3)) throw new FileSizeInvalidException();
            if (!dto.ImageFile.IsTypeValid("image")) throw new FileTypeInvalidExveption();
        }

        var uni = _uniRepo.GetAll();

        var unid = await uni.FirstOrDefaultAsync();

        var director = _mapper.Map<Director>(dto);

        director.UniversityId = unid.Id;

        if (dto.ImageFile != null)
        {
            director.ImageUrl = await _fileService.UploadAsync(dto.ImageFile, RootConstants.DirectorImageRoot);
        }

        director.Status = Status.Work;

        if (await _user.Users.AnyAsync(d => d.UserName == dto.UserName || d.Email == dto.Email))
            throw new UserExistException();

        var result = await _userManager.CreateAsync(director, dto.Password);
        if (!result.Succeeded)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in result.Errors)
            {
                sb.Append(item.Description + " ");
            }
            throw new RegisterFailedException(sb.ToString().TrimEnd());
        }
        else
        {
            var userFromDb = await _userManager.FindByNameAsync(dto.UserName);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(userFromDb);

            var urlBuilder = new UriBuilder(_config["ReturnPaths:ConfirmEmail"]);
            var query = HttpUtility.ParseQueryString(urlBuilder.Query);
            query["token"] = token;
            query["userid"] = userFromDb.Id;
            urlBuilder.Query = query.ToString();
            var urlString = urlBuilder.ToString();

            var senderEmail = _config["ReturnPaths:SenderEmail"];

            await _emailSender.SendEmailAsync(senderEmail, userFromDb.Email, "Please Confirm your Email address", urlString);
        }
    }

    public async Task SoftDeleteAsync(string id)
    {
        var director = await _userManager.Users.FirstAsync(d => d.Id == id);
        if (director == null) throw new UserNotFoundException<Director>();

        director.IsDeleted = true;
        director.UniversityId = null;

        await _userManager.UpdateAsync(director);
    }

    public async Task RevertSoftDeleteAsync(string id)
    {
        var director = await _userManager.Users.FirstAsync(d => d.Id == id);
        if (director == null) throw new UserNotFoundException<Director>();

        var directors = await _userManager.Users.FirstOrDefaultAsync(d => d.IsDeleted == false);
        if (directors != null) throw new ThereIsaDirectorInTheSystemException();

        var uni = _uniRepo.GetAll();

        var unid = await uni.FirstOrDefaultAsync();

        director.IsDeleted = false;
        director.UniversityId = unid.Id;

        await _userManager.UpdateAsync(director);
    }

    public async Task<TokenResponseDto> LoginAsync(DIrectorLoginDto dto)
    {
        var director = await _userManager.FindByNameAsync(dto.UserName);
        if (director == null) throw new UserNotFoundException<Director>();

        var result = await _userManager.CheckPasswordAsync(director, dto.Password);
        if (result == false) throw new LoginFailedException<Director>();

        if (director.IsDeleted == true) throw new YourAccountHasBeenSuspendedException();

        return _tokenService.CreateDirectorToken(director);
    }

    public async Task AddRole(AddRoleDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.userName);
        if (user == null) throw new UserNotFoundException<AppUser>();

        if (!await _roleManager.RoleExistsAsync(dto.roleName)) throw new NotFoundException<IdentityUser>();

        var result = await _userManager.AddToRoleAsync(user, dto.roleName);
        if (!result.Succeeded) throw new AddRoleFailesException();
    }

    public async Task RemoveRole(RemoveRoleDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.userName);
        if (user == null) throw new UserNotFoundException<AppUser>();

        if (!await _roleManager.RoleExistsAsync(dto.roleName)) throw new NotFoundException<IdentityUser>();

        var result = await _userManager.RemoveFromRoleAsync(user, dto.roleName);
        if (!result.Succeeded) throw new RoleRemoveFailedException();
    }

    public async Task<ICollection<DirectorWithRoles>> GetAllAsync(bool tekeAll)
    {
        ICollection<DirectorWithRoles> directors = new List<DirectorWithRoles>();
        if (tekeAll)
        {
            foreach (var user in await _userManager.Users.ToListAsync())
            {
                var director = new DirectorWithRoles
                {
                    Name = user.Name,
                    ImageUrl = user.ImageUrl,
                    Surname = user.Surname,
                    UserName = user.UserName,
                    Roles = await _userManager.GetRolesAsync(user),
                    IsDeleted = user.IsDeleted
                };
                directors.Add(director);
            }
        }
        else
        {
            foreach (var user in await _userManager.Users.Where(d => d.IsDeleted == false).ToListAsync())
            {
                var director = new DirectorWithRoles
                {
                    Name = user.Name,
                    ImageUrl = user.ImageUrl,
                    Surname = user.Surname,
                    UserName = user.UserName,
                    Roles = await _userManager.GetRolesAsync(user),
                    IsDeleted = user.IsDeleted
                };
                directors.Add(director);
            }
        }
        return directors;
    }

    public async Task<TokenResponseDto> LoginWithRefreshTokenAsync(string token)
    {
        if (string.IsNullOrEmpty(token)) throw new ArgumentNullException("token");
        var user = await _userManager.Users.SingleOrDefaultAsync(d => d.RefreshToken == token);
        if (user == null) throw new NotFoundException<AppUser>();

        if (user.RefreshTokenExpiresDate < DateTime.UtcNow.AddHours(4))
            throw new RefreshTokenExpiresDateException();
        return _tokenService.CreateDirectorToken(user);
    }

    public async Task UpdatePrfileAsync(DirectorUpdateDto dto)
    {
        if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException();
        if (!await _userManager.Users.AnyAsync(d => d.Id == userId)) throw new UserNotFoundException<Director>();

        if (dto.ImageFile != null)
        {
            if (!dto.ImageFile.IsSizeValid(3)) throw new FileSizeInvalidException();
            if (!dto.ImageFile.IsTypeValid("image")) throw new FileTypeInvalidExveption();
        }

        var user = await _userManager.FindByIdAsync(userId);

        if (dto.ImageFile != null)
        {
            if (user.ImageUrl != null)
            {
                _fileService.Delete(user.ImageUrl);
            }
            user.ImageUrl = await _fileService.UploadAsync(dto.ImageFile, RootConstants.DirectorImageRoot);
        }

        if (await _user.Users.AnyAsync
            (d => (d.UserName == dto.UserName && d.Id != userId) || (d.Email == dto.Email && d.Id != userId)))
            throw new UserExistException();

        _mapper.Map(dto, user);

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded) throw new UserProfileUpdateException();
    }

    public async Task UpdateProfileAdminAsync(string userName, DirectorUpdateAdminDto dto)
    {
        if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentNullException();
        if (!await _userManager.Users.AnyAsync(u => u.Id == userId)) throw new UserNotFoundException<AppUser>();

        var director = await _userManager.FindByNameAsync(userName);
        if (director == null) throw new UserNotFoundException<Director>();

        if (dto.ImageFile != null)
        {
            if (!dto.ImageFile.IsSizeValid(3)) throw new FileSizeInvalidException();
            if (!dto.ImageFile.IsTypeValid("image")) throw new FileTypeInvalidExveption();
        }

        if (dto.ImageFile != null)
        {
            if (director.ImageUrl != null)
            {
                _fileService.Delete(director.ImageUrl);
            }
            director.ImageUrl = await _fileService.UploadAsync(dto.ImageFile, RootConstants.DirectorImageRoot);
        }

        if (await _userManager.Users.AnyAsync
           (d => (d.UserName == dto.UserName && d.Id != userId) || (d.Email == dto.Email && d.Id != userId)))
            throw new UserExistException();

        if (dto.Status == Status.OutOfWork)
        {
            director.EndDate = DateTime.UtcNow.AddHours(4);
            await SoftDeleteAsync(director.Id);
        }
        if (dto.Status == Status.Work)
        {
            director.EndDate = null;
            director.StartDate = DateTime.UtcNow.AddHours(4);
            await RevertSoftDeleteAsync(director.Id);
        }

        _mapper.Map(dto, director);

        var result = await _userManager.UpdateAsync(director);
        if (!result.Succeeded) throw new UserProfileUpdateException();
    }

    public async Task DeleteAsync(string userName)
    {
        if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException("userName");
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null) throw new UserNotFoundException<Director>();

        if (user.ImageUrl != null)
            _fileService.Delete(user.ImageUrl);

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded) throw new UserDeleteProblemException();
    }

    public async Task SignOut()
    {
        await _signinManager.SignOutAsync();
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) throw new UserNotFoundException<Director>();
        user.RefreshToken = null;
        user.RefreshTokenExpiresDate = null;
        var res = await _userManager.UpdateAsync(user);
        if (!res.Succeeded) throw new SIgnOutInvalidException();
    }

    public async Task<DirectorWithRoles> GetByIdAsync(string id, bool takeAll)
    {
        DirectorWithRoles dr = new DirectorWithRoles();
        if (takeAll)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) throw new UserNotFoundException<Director>();
            dr = new DirectorWithRoles
            {
                ImageUrl = user.ImageUrl,
                UserName = user.UserName,
                IsDeleted = user.IsDeleted,
                Name = user.Name,
                Surname = user.Surname,
                Roles = await _userManager.GetRolesAsync(user),
            };
        }
        else
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == id && u.IsDeleted == false);
            if (user == null) throw new UserNotFoundException<Director>();
            dr = new DirectorWithRoles
            {
                ImageUrl = user.ImageUrl,
                UserName = user.UserName,
                IsDeleted = user.IsDeleted,
                Name = user.Name,
                Surname = user.Surname,
                Roles = await _userManager.GetRolesAsync(user),
            };
        }
        return dr;
    }

}
