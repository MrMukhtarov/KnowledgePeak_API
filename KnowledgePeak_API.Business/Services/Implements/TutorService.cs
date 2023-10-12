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
using KnowledgePeak_API.Business.Dtos.GroupDtos;
using KnowledgePeak_API.Business.Dtos.RoleDtos;
using KnowledgePeak_API.Business.Exceptions.Role;
using KnowledgePeak_API.DAL.Repositories.Interfaces;
using KnowledgePeak_API.Business.Exceptions.Tutor;
using KnowledgePeak_API.Business.Dtos.SpecialityDtos;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace KnowledgePeak_API.Business.Services.Implements;

public class TutorService : ITutorService
{
    readonly UserManager<Tutor> _userManager;
    readonly UserManager<AppUser> _appUserManager;
    readonly IMapper _mapper;
    readonly IFileService _file;
    readonly ITokenService _token;
    readonly RoleManager<IdentityRole> _role;
    readonly IGroupRepository _group;
    readonly ISpecialityRepository _special;
    readonly string _userId;
    readonly IHttpContextAccessor _accessor;

    public TutorService(UserManager<Tutor> userManager, UserManager<AppUser> appUserManager,
        IMapper mapper, IFileService file, ITokenService token, RoleManager<IdentityRole> role, IGroupRepository group,
        ISpecialityRepository special, IHttpContextAccessor accessor)
    {
        _userManager = userManager;
        _appUserManager = appUserManager;
        _mapper = mapper;
        _file = file;
        _token = token;
        _role = role;
        _group = group;
        _special = special;
        _accessor = accessor;
        _userId = accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    public async Task AddGroup(TutorAddGroupDto dto)
    {
        var user = await _userManager.Users.Include(u => u.Groups)
            .SingleOrDefaultAsync(u => u.UserName == dto.userName);
        if (user == null) throw new UserNotFoundException<Tutor>();

        user.Groups.Clear();
        if (dto.GroupIds != null)
        {
            foreach (var item in dto.GroupIds)
            {
                var group = await _group.FIndByIdAsync(item);
                if (group == null) throw new NotFoundException<Group>();
                user.Groups.Add(group);
            }
        }
        _mapper.Map(user, dto);
        var res = await _userManager.UpdateAsync(user);
        if (!res.Succeeded) throw new UserProfileUpdateException();
    }

    public async Task AddRoleAsync(AddRoleDto dto)
    {
        if (string.IsNullOrEmpty(dto.userName)) throw new ArgumentNullException();
        var user = await _userManager.FindByNameAsync(dto.userName);
        if (user == null) throw new UserNotFoundException<Tutor>();

        var role = _role.FindByNameAsync(dto.roleName);
        if (role == null) throw new NotFoundException<RoleManager<IdentityRole>>();

        var res = await _userManager.AddToRoleAsync(user, dto.roleName);
        if (!res.Succeeded) throw new AddRoleFailesException();
    }

    public async Task AddSpeciality(TutorAddSpecialityDto dto)
    {
        var user = await _userManager.Users.Include(u => u.Speciality)
            .SingleOrDefaultAsync(u => u.UserName == dto.UserName);
        if (user == null) throw new UserNotFoundException<Tutor>();

        if (user.SpecialityId != null) throw new TutorSpecialityIsNotBeNullException();

        var speciality = await _special.GetSingleAsync(s => s.Id == dto.SpecialityId);
        if (speciality == null) throw new NotFoundException<Speciality>();

        user.SpecialityId = dto.SpecialityId;
        var res = await _userManager.UpdateAsync(user);
        if (!res.Succeeded) throw new UserProfileUpdateException();
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

    public async Task<ICollection<TutorListItemDto>> GetAllAsync(bool takeAll)
    {
        ICollection<TutorListItemDto> list = new List<TutorListItemDto>();
        if (takeAll)
        {
            foreach (var item in await _userManager.Users.Include(t => t.Groups).Include(t => t.Speciality).ToListAsync())
            {
                list.Add(new TutorListItemDto
                {
                    Email = item.Email,
                    ImageUrl = item.ImageUrl,
                    IsDeleted = item.IsDeleted,
                    Name = item.Name,
                    Surname = item.Surname,
                    UserName = item.UserName,
                    Speciality = _mapper.Map<SpecialityInfoDto>(item.Speciality),
                    Roles = await _userManager.GetRolesAsync(item),
                    Groups = _mapper.Map<ICollection<GroupSingleDetailDto>>(item.Groups)
                });
            }
        }
        else
        {
            foreach (var item in await _userManager.Users.Include(t => t.Groups).Include(t => t.Speciality)
                .Where(u => u.IsDeleted == false).ToListAsync())
            {
                list.Add(new TutorListItemDto
                {
                    Email = item.Email,
                    ImageUrl = item.ImageUrl,
                    IsDeleted = item.IsDeleted,
                    Name = item.Name,
                    Surname = item.Surname,
                    UserName = item.UserName,
                    Speciality = _mapper.Map<SpecialityInfoDto>(item.Speciality),
                    Roles = await _userManager.GetRolesAsync(item),
                    Groups = _mapper.Map<ICollection<GroupSingleDetailDto>>(item.Groups)
                });
            }
        }
        return list;
    }

    public async Task<TutorDetailDto> GetByIdAsync(string userName, bool takeAll)
    {
        TutorDetailDto tutor = new TutorDetailDto();
        if (takeAll)
        {
            var user = await _userManager.Users.Include(u => u.Groups).Include(u => u.Speciality)
                .SingleOrDefaultAsync(u => u.UserName == userName);
            if (user == null) throw new UserNotFoundException<Tutor>();
            tutor = new TutorDetailDto
            {
                Email = user.Email,
                ImageUrl = user.ImageUrl,
                IsDeleted = user.IsDeleted,
                Name = user.Name,
                Surname = user.Surname,
                UserName = user.UserName,
                Speciality = _mapper.Map<SpecialityInfoDto>(user.Speciality),
                Roles = await _userManager.GetRolesAsync(user),
                Groups = _mapper.Map<ICollection<GroupSingleDetailDto>>(user.Groups)
            };
        }
        else
        {
            var user = await _userManager.Users.Include(u => u.Groups).Include(u => u.Speciality)
                .Where(u => u.IsDeleted == false).FirstOrDefaultAsync(u => u.UserName == userName);
            if (user == null) throw new UserNotFoundException<Tutor>();
            tutor = new TutorDetailDto
            {
                Email = user.Email,
                ImageUrl = user.ImageUrl,
                IsDeleted = user.IsDeleted,
                Name = user.Name,
                Surname = user.Surname,
                UserName = user.UserName,
                Speciality = _mapper.Map<SpecialityInfoDto>(user.Speciality),
                Roles = await _userManager.GetRolesAsync(user),
                Groups = _mapper.Map<ICollection<GroupSingleDetailDto>>(user.Groups)
            };
        }
        return tutor;
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
        if (string.IsNullOrEmpty(token)) throw new ArgumentNullException();
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshToken == token);
        if (user == null) throw new UserNotFoundException<Tutor>();

        if (user.RefreshTokenExpiresDate < DateTime.UtcNow.AddHours(4)) throw new
                RefreshTokenExpiresDateException();
        return _token.CreateTutorToken(user);
    }

    public async Task RemoveRole(RemoveRoleDto dto)
    {
        var user = await _userManager.FindByNameAsync(dto.userName);
        if (user == null) throw new UserNotFoundException<Tutor>();

        var role = await _role.FindByNameAsync(dto.roleName);
        if (role == null) throw new NotFoundException<RoleManager<IdentityRole>>();

        var res = await _userManager.RemoveFromRoleAsync(user, dto.roleName);
        if (!res.Succeeded) throw new RoleRemoveFailedException();
    }

    public async Task RevertSoftDeleteAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null) throw new UserNotFoundException<Tutor>();
        user.IsDeleted = false;
        user.Status = Status.Work;
        user.EndDate = null;
        user.StartDate = DateTime.UtcNow.AddHours(4);
        var res = await _userManager.UpdateAsync(user);
        if (!res.Succeeded) throw new SoftDeleteInvalidException<Tutor>();
    }

    public async Task SoftDeleteAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null) throw new UserNotFoundException<Tutor>();
        user.IsDeleted = true;
        user.Status = Status.OutOfWork;
        user.EndDate = DateTime.UtcNow.AddHours(4);

        var res = await _userManager.UpdateAsync(user);
        if (!res.Succeeded) throw new SoftDeleteInvalidException<Tutor>();
    }

    public async Task UpdateProfileAsync(TutorUpdateProfileDto dto)
    {
        if (!await _userManager.Users.AnyAsync(u => u.Id == _userId)) throw new UserNotFoundException<Tutor>();
        var user = await _userManager.FindByIdAsync(_userId);
        if (user == null) throw new UserNotFoundException<Tutor>();

        if (dto.ImageFile != null)
        {
            if (user.ImageUrl != null)
                _file.Delete(user.ImageUrl);
            if (!dto.ImageFile.IsTypeValid("image")) throw new FileTypeInvalidExveption();
            if (!dto.ImageFile.IsSizeValid(3)) throw new FileSizeInvalidException();
            user.ImageUrl = await _file.UploadAsync(dto.ImageFile, RootConstants.TutorImageRoot);
        }
        if (await _appUserManager.Users.AnyAsync(u => (u.UserName == dto.UserName && u.Id != _userId) ||
        (u.Email == dto.Email && u.Id != _userId))) throw new UserExistException();

        _mapper.Map(dto, user);
        var res = await _userManager.UpdateAsync(user);
        if (!res.Succeeded) throw new UserProfileUpdateException();
    }

    public async Task UpdateProfileFromAdminAsync(TutorUpdateProfileFromAdminDto dto, string userName)
    {
        var user = await _userManager.Users.Include(t => t.Speciality).Include(t => t.Groups)
            .SingleOrDefaultAsync(u => u.UserName == userName);
        if (user == null) throw new UserNotFoundException<Tutor>();

        if (dto.ImageFile != null)
        {
            if (user.ImageUrl != null)
                _file.Delete(user.ImageUrl);
            if (!dto.ImageFile.IsTypeValid("image")) throw new FileTypeInvalidExveption();
            if (!dto.ImageFile.IsSizeValid(3)) throw new FileSizeInvalidException();
            user.ImageUrl = await _file.UploadAsync(dto.ImageFile, RootConstants.TutorImageRoot);
        }
        if (await _appUserManager.Users.AnyAsync(u => (u.UserName == dto.UserName && u.Id != user.Id) ||
        (u.Email == dto.Email && u.Id != user.Id))) throw new UserExistException();

        if (dto.Status == Status.OutOfWork)
            await SoftDeleteAsync(dto.UserName);
        if (dto.Status == Status.Work)
            await RevertSoftDeleteAsync(dto.UserName);

        if (dto.SpecialityId != null)
        {
            var spec = await _special.GetSingleAsync(s => s.Id == dto.SpecialityId);
            if (spec == null) throw new NotFoundException<Speciality>();
            user.SpecialityId = spec.Id;
        }
        user.Groups.Clear();
        if (dto.GroupIds != null)
        {
            foreach (var item in dto.GroupIds)
            {
                var gr = await _group.FIndByIdAsync(item);
                if (gr == null) throw new NotFoundException<Group>();
                user.Groups.Add(gr);
            }
        }
        _mapper.Map(dto, user);
        var res = await _userManager.UpdateAsync(user);
        if (!res.Succeeded) throw new UserProfileUpdateException();
    }
}
