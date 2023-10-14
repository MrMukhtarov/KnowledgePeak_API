using KnowledgePeak_API.Business.Dtos.ClassScheduleDtos;
using KnowledgePeak_API.Business.Services.Interfaces;
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

    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromForm] ClassScheduleCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return Ok();
    }
}
