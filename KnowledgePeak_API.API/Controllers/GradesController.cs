using KnowledgePeak_API.Business.Dtos.GradeDtos;
using KnowledgePeak_API.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgePeak_API.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GradesController : ControllerBase
{
    readonly IGradeService _service;

    public GradesController(IGradeService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize(Roles = "Student")]
    [Authorize(Roles = "Tutor")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("[action]/{id}")]
    [Authorize(Roles = "Student")]
    [Authorize(Roles = "Tutor")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _service.GetByIdAsyc(id));
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> Create([FromForm] GradeCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return Ok();
    }

    [HttpPut("[action]")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> Update([FromForm] GradeUpdateDto dto)
    {
        await _service.UpdateAsync(dto);
        return Ok();
    }
}
