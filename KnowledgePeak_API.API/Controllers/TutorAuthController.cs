using KnowledgePeak_API.Business.Dtos.RoleDtos;
using KnowledgePeak_API.Business.Dtos.TutorDtos;
using KnowledgePeak_API.Business.ExternalServices.Interfaces;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using Microsoft.AspNetCore.Http;
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
    public async Task<IActionResult> Create([FromForm] TutorCreateDto dto)
    {
        await _service.CreateAsync(dto);
        var tutor = await _userManager.FindByEmailAsync(dto.Email);
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(tutor);
        var confirmationLink = Url.Action("ConfirmEmail", "TutorAuth", new { token, email = dto.Email }, Request.Scheme);
        var message = new Message(new string[] { dto.Email! }, "Confirmation email link", confirmationLink!);
        _emailService.SendEail(message);
        return StatusCode(StatusCodes.Status201Created);
    }


    [HttpGet("[action]")]
    public async Task<IActionResult> ConfirmEmail(string token, string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return StatusCode(StatusCodes.Status200OK);
            }
        }
        return StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromForm] TutorLoginDto dto)
    {
        var tutor = await _userManager.FindByNameAsync(dto.UserName);
        if (tutor.EmailConfirmed == false)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(tutor);
            var confirmationLink = Url.Action("ConfirmEmail", "TutorAuth", new { token, email = tutor.Email }, Request.Scheme);
            var message = new Message(new string[] { tutor.Email! }, "Confirmation email link", confirmationLink!);
            _emailService.SendEail(message);
            return StatusCode(StatusCodes.Status201Created);
        }
        return Ok(await _service.LoginAsync(dto));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> LoginWithRefreshToken(string token)
    {
        return Ok(await _service.LoginWithRefreshTokenAsync(token));
    }


    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync(true));
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetSingle(string userName)
    {
        return Ok(await _service.GetByIdAsync(userName, true));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddGroup([FromForm] TutorAddGroupDto dto)
    {
        await _service.AddGroup(dto);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddSpeciality([FromForm] TutorAddSpecialityDto dto)
    {
        await _service.AddSpeciality(dto);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddRole([FromForm] AddRoleDto dto)
    {
        await _service.AddRoleAsync(dto);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> RemoveRole([FromForm] RemoveRoleDto dto)
    {
        await _service.RemoveRole(dto);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UpdateProfile([FromForm] TutorUpdateProfileDto dto)
    {
        await _service.UpdateProfileAsync(dto);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UpdateProfileFromAdmin([FromForm] TutorUpdateProfileFromAdminDto dto, string userName)
    {
        await _service.UpdateProfileFromAdminAsync(dto,userName);
        return Ok();
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> Delete(string userName)
    {
        await _service.DeleteAsync(userName);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> LogOut()
    {
        await _service.SignOut();
        return Ok();
    }

}
