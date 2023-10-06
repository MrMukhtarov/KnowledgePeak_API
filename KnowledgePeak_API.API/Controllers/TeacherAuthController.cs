using KnowledgePeak_API.Business.Dtos.RoleDtos;
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

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromForm] TeacherLoginDto dto)
    {
        return Ok(await _service.Login(dto));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddRole([FromForm] AddRoleDto dto)
    {
        await _service.AddRoleAsync(dto);
        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync(true));
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
}
