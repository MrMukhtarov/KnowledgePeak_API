using KnowledgePeak_API.Business.Dtos.ClassTimeDtos;
using KnowledgePeak_API.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace KnowledgePeak_API.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClassTimeController : ControllerBase
{
    readonly IClassTimeService _service;

    public ClassTimeController(IClassTimeService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize(Roles = "Director,Tutor,Student,Teacher,Admin,SuperAdmin")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("[action]/{id}")]
    [Authorize(Roles = "SuperAdmin")]
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "Teacher")]
    [Authorize(Roles = "Student")]
    [Authorize(Roles = "Tutor")]
    [Authorize(Roles = "Director")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "Tutor")]
    public async Task<IActionResult> Create([FromForm] ClassTImeCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return Ok();
    }

    [HttpPut("[action]/{id}")]
    [Authorize(Roles = "Tutor")]
    public async Task<IActionResult> Update([FromForm] ClassTimeUpdateDto dto, int id)
    {
        await _service.UpdateAsync(dto, id);
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
}
