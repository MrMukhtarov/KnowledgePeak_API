using KnowledgePeak_API.Business.Dtos.RoleDtos;
using KnowledgePeak_API.Business.Dtos.TeacherDtos;
using KnowledgePeak_API.Business.ExternalServices.Interfaces;
using KnowledgePeak_API.Business.Services.Interfaces;
using KnowledgePeak_API.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgePeak_API.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeacherAuthController : ControllerBase
{
    readonly ITeacherService _service;
    readonly UserManager<Teacher> _userManager;
    readonly IEmailService _emailService;

    public TeacherAuthController(ITeacherService service, UserManager<Teacher> userManager, IEmailService emailService)
    {
        _service = service;
        _userManager = userManager;
        _emailService = emailService;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> CreateTeacher([FromForm] TeacherCreateDto dto)
    {
        await _service.CreateAsync(dto);
        var teacher = await _userManager.FindByEmailAsync(dto.Email);
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(teacher);
        var confirmationLink = Url.Action("ConfirmEmail", "TeacherAuth", new { token, email = dto.Email }, Request.Scheme);
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
    public async Task<IActionResult> Login([FromForm] TeacherLoginDto dto)
    {
        var teacher = await _userManager.FindByNameAsync(dto.UserName);
        if (teacher.EmailConfirmed == false)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(teacher);
            var confirmationLink = Url.Action("ConfirmEmail", "TeacherAuth", new { token, email = teacher.Email }, Request.Scheme);
            var message = new Message(new string[] { teacher.Email! }, "Confirmation email link", confirmationLink!);
            _emailService.SendEail(message);
            return StatusCode(StatusCodes.Status201Created);
        }
        return Ok(await _service.Login(dto));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> LoginWithRefreshToken(string token)
    {
        return Ok(await _service.LoginWithRefreshTokenAsync(token));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UpdateProfile([FromForm] TeacherUpdateProfileDto dto)
    {
        await _service.UpdateAsync(dto);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UpdateProfileAdmin([FromForm] TeacherAdminUpdateDto dto, string id)
    {
        await _service.UpdateAdminAsync(dto,id);
        return Ok();
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> Delete(string userName)
    {
        await _service.DeleteAsync(userName);
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
        await _service.RemoveRoleAsync(dto);
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
        return Ok(await _service.GetByIdAsync(id,true));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddFaculty([FromForm] TeacherAddFacultyDto dto, string userName)
    {
        await _service.AddFaculty(dto,userName);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddSpeciality([FromForm] TeacherAddSpecialitiyDto dto, string userName)
    {
        await _service.AddSpeciality(dto, userName);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddLesson([FromForm] TeacherAddLessonDto dto, string userName)
    {
        await _service.AddLesson(dto, userName);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> SignOut()
    {
        await _service.SignOut();
        return Ok();
    }
}
