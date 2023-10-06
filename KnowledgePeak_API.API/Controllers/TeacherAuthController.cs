using KnowledgePeak_API.Business.Dtos.TeacherDtos;
using KnowledgePeak_API.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgePeak_API.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeacherAuthController : ControllerBase
{
    readonly ITeacherService _service;

    public TeacherAuthController(ITeacherService service)
    {
        _service = service;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> CreateTeacher([FromForm] TeacherCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return StatusCode(StatusCodes.Status201Created);
    }
}
