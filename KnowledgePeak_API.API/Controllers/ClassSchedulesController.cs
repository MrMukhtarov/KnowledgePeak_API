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

    [HttpGet("[action]")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAllAync(true));
    }

    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _service.GetByIdAsync(id,true));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromForm] ClassScheduleCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return Ok();
    }
    [HttpPut("[action]/{id}")]
    public async Task<IActionResult> Update([FromForm] ClassScheduleUpdateDto dto, int id)
    {
        await _service.UpdateAsync(id,dto);
        return Ok();
    }

}
