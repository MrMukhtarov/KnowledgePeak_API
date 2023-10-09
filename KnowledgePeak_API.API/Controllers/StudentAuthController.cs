using KnowledgePeak_API.Business.Dtos.RoleDtos;
using KnowledgePeak_API.Business.Dtos.StudentDtos;
using KnowledgePeak_API.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgePeak_API.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentAuthController : ControllerBase
{
    readonly IStudentService _service;

    public StudentAuthController(IStudentService service)
    {
        _service = service;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromForm] StudentCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromForm] StudentLoginDto dto)
    {
        return Ok(await _service.LoginAsync(dto));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> LoginWithRefreshToken(string token)
    {
        return Ok(await _service.LoginWithRefreshToken(token));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddRole([FromForm]AddRoleDto dto)
    {
        await _service.AddRole(dto);
        return Ok();
    }
}
