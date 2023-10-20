using KnowledgePeak_API.Business.Dtos.DirectorDtos;
using KnowledgePeak_API.Business.Dtos.RoleDtos;
using KnowledgePeak_API.Business.Exceptions.Commons;
using KnowledgePeak_API.Business.ExternalServices.Interfaces;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgePeak_API.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DirectorAuthsController : ControllerBase
{
    readonly IDirectorService _service;
    readonly UserManager<Director> _userManager;
    readonly IEmailService _emailService;


    public DirectorAuthsController(IDirectorService service, UserManager<Director> userManager, IEmailService emailService)
    {
        _service = service;
        _userManager = userManager;
        _emailService = emailService;
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
    public async Task<IActionResult> Create([FromForm] DirectorCreateDto dto)
    {
        await _service.CreateAsync(dto);
        var director = await _userManager.FindByEmailAsync(dto.Email);
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(director);
        var confirmationLink = Url.Action("ConfirmEmail", "DirectorAuths", new { token, email = dto.Email }, Request.Scheme);
        var message = new Message(new string[] { dto.Email! }, "Confirmation email link", confirmationLink!);
        _emailService.SendEail(message);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromForm] DIrectorLoginDto dto)
    {
        var director = await _userManager.FindByNameAsync(dto.UserName);
        if(director.EmailConfirmed == false)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(director);
            var confirmationLink = Url.Action("ConfirmEmail", "DirectorAuths", new { token, email = director.Email }, Request.Scheme);
            var message = new Message(new string[] { director.Email! }, "Confirmation email link", confirmationLink!);
            _emailService.SendEail(message);
            return StatusCode(StatusCodes.Status201Created);
        }
        return Ok(await _service.LoginAsync(dto));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UpdateAccount([FromForm] DirectorUpdateDto dto)
    {
        await _service.UpdatePrfileAsync(dto);
        return Ok();
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> Delete(string userName)
    {
        await _service.DeleteAsync(userName);
        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync(true));
    }

    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        return Ok(await _service.GetByIdAsync(id, true));
    }
    //[Authorize(Roles = "Admin")]
    [HttpPost("[action]")]
    public async Task<IActionResult> AddRole([FromForm] AddRoleDto dto)
    {
        await _service.AddRole(dto);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("[action]")]
    public async Task<IActionResult> RemoveRole([FromForm] RemoveRoleDto dto)
    {
        await _service.RemoveRole(dto);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> LoginWithRefreshToken(string refreshToken)
    {
        return Ok(await _service.LoginWithRefreshTokenAsync(refreshToken));
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateProfileAdmin([FromForm] DirectorUpdateAdminDto dto, string userName)
    {
        await _service.UpdateProfileAdminAsync(userName, dto);
        return StatusCode(StatusCodes.Status200OK);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> SignOut()
    {
        await _service.SignOut();
        return Ok();
    }
}
