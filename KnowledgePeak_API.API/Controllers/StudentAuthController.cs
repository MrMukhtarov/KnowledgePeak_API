using KnowledgePeak_API.Business.Dtos.RoleDtos;
using KnowledgePeak_API.Business.Dtos.StudentDtos;
using KnowledgePeak_API.Business.ExternalServices.Interfaces;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgePeak_API.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentAuthController : ControllerBase
{
    readonly IStudentService _service;
    readonly UserManager<Student> _userManager;
    readonly IEmailService _emailService;

    public StudentAuthController(IStudentService service, UserManager<Student> userManager, IEmailService emailService)
    {
        _service = service;
        _userManager = userManager;
        _emailService = emailService;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromForm] StudentCreateDto dto)
    {
        await _service.CreateAsync(dto);
        var stu = await _userManager.FindByEmailAsync(dto.Email);
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(stu);
        var confirmationLink = Url.Action("ConfirmEmail", "StudentAuth", new { token, email = dto.Email }, Request.Scheme);
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
    public async Task<IActionResult> Login([FromForm] StudentLoginDto dto)
    {
        var stu = await _userManager.FindByNameAsync(dto.UserName);
        if (stu.EmailConfirmed == false)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(stu);
            var confirmationLink = Url.Action("ConfirmEmail", "StudentAuth", new { token, email = stu.Email }, Request.Scheme);
            var message = new Message(new string[] { stu.Email! }, "Confirmation email link", confirmationLink!);
            _emailService.SendEail(message);
            return StatusCode(StatusCodes.Status201Created);
        }
        return Ok(await _service.LoginAsync(dto));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> LoginWithRefreshToken(string token)
    {
        return Ok(await _service.LoginWithRefreshToken(token));
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateProfile([FromForm] StudentUpdateDto dto)
    {
        await _service.UpdateAsync(dto);
        return Ok();
    }

    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateProfileAdmin([FromForm] StudentAdminUpdateDto dto, string userName)
    {
        await _service.UpdatPrfileFromAdmin(userName, dto);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddRole([FromForm] AddRoleDto dto)
    {
        await _service.AddRole(dto);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> RemoveRole([FromForm] RemoveRoleDto dto)
    {
        await _service.RemoveRole(dto);
        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAll(true));
    }

    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        return Ok(await _service.GetByIdAsync(id,true));
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> Delete(string userName)
    {
        await _service.DeleteAsync(userName);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> SignOut()
    {
        await _service.SignOut();
        return Ok();
    }
}
