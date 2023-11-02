using KnowledgePeak_API.Business.Dtos.RoleDtos;
using KnowledgePeak_API.Business.Dtos.TutorDtos;
using KnowledgePeak_API.Business.ExternalServices.Interfaces;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgePeak_API.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TutorAuthController : ControllerBase
{
    readonly ITutorService _service;
    readonly UserManager<Tutor> _userManager;
    readonly IEmailService _emailService;

    public TutorAuthController(ITutorService service, UserManager<Tutor> userManager, IEmailService emailService)
    {
        _service = service;
        _userManager = userManager;
        _emailService = emailService;
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Create([FromForm] TutorCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromForm] TutorLoginDto dto)
    {
        return Ok(await _service.LoginAsync(dto));
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "Tutor")]
    public async Task<IActionResult> LoginWithRefreshToken(string token)
    {
        return Ok(await _service.LoginWithRefreshTokenAsync(token));
    }

    [HttpGet("[action]")]
    //[Authorize(Roles = "Admin")]
    //[Authorize(Roles = "SuperAdmin")]
    //[Authorize(Roles = "Director")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync(true));
    }

    [HttpGet("[action]")]
    [Authorize(Roles = "Tutor,Director,Admin")]
    public async Task<IActionResult> GetSingle(string userName)
    {
        return Ok(await _service.GetByIdAsync(userName, true));
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> AddGroup([FromForm] TutorAddGroupDto dto)
    {
        await _service.AddGroup(dto);
        return Ok();
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> AddSpeciality([FromForm] TutorAddSpecialityDto dto)
    {
        await _service.AddSpeciality(dto);
        return Ok();
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "Admin")]
    //[Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> AddRole([FromForm] AddRoleDto dto)
    {
        await _service.AddRoleAsync(dto);
        return Ok();
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> RemoveRole([FromForm] RemoveRoleDto dto)
    {
        await _service.RemoveRole(dto);
        return Ok();
    }

    [HttpPut("[action]")]
    [Authorize(Roles = "Tutor")]
    public async Task<IActionResult> UpdateProfile([FromForm] TutorUpdateProfileDto dto)
    {
        await _service.UpdateProfileAsync(dto);
        return Ok();
    }

    [HttpPut("[action]")]
    //[Authorize(Roles = "Admin")]
    //[Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> UpdateProfileFromAdmin([FromForm] TutorUpdateProfileFromAdminDto dto, string userName)
    {
        await _service.UpdateProfileFromAdminAsync(dto, userName);
        return Ok();
    }

    [HttpDelete("[action]")]
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Delete(string userName)
    {
        await _service.DeleteAsync(userName);
        return Ok();
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "Tutor")]
    public async Task<IActionResult> SignOut()
    {
        await _service.SignOut();
        return Ok();
    }
}
