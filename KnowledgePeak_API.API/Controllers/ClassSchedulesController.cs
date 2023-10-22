using KnowledgePeak_API.Business.Dtos.ClassScheduleDtos;
using KnowledgePeak_API.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgePeak_API.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClassSchedulesController : ControllerBase
{
    readonly IClassScheduleService _service;

    public ClassSchedulesController(IClassScheduleService service)
    {
        _service = service;
    }

    [HttpGet("[action]")]
    [Authorize(Roles = "SuperAdmin")]
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "Teacher")]
    [Authorize(Roles = "Student")]
    [Authorize(Roles = "Tutor")]
    [Authorize(Roles = "Director")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAllAync(true));
    }

    [HttpGet("[action]/{id}")]
    [Authorize(Roles = "SuperAdmin")]
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "Teacher")]
    [Authorize(Roles = "Student")]
    [Authorize(Roles = "Tutor")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _service.GetByIdAsync(id, true));
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "Tutor")]
    public async Task<IActionResult> Create([FromForm] ClassScheduleCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return Ok();
    }
    [HttpPut("[action]/{id}")]
    [Authorize(Roles = "Tutor")]
    public async Task<IActionResult> Update([FromForm] ClassScheduleUpdateDto dto, int id)
    {
        await _service.UpdateAsync(id, dto);
        return Ok();
    }

    [HttpDelete("[action]/{id}")]
    [Authorize(Roles = "Tutor")]
    [Authorize(Roles = "SuperAdmin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }

    [HttpPatch("[action]/{id}")]
    [Authorize(Roles = "Tutor")]
    [Authorize(Roles = "SuperAdmin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        await _service.SoftDeleteAsync(id);
        return Ok();
    }

    [HttpPatch("[action]/{id}")]
    [Authorize(Roles = "Tutor")]
    [Authorize(Roles = "SuperAdmin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RevertSoftDelete(int id)
    {
        await _service.RevertSoftDeleteAsync(id);
        return Ok();
    }
}
