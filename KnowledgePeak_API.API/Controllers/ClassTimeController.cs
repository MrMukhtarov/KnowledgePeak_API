using KnowledgePeak_API.Business.Dtos.ClassTimeDtos;
using KnowledgePeak_API.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromForm] ClassTImeCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return Ok();
    }

    [HttpPut("[action]/{id}")]
    public async Task<IActionResult> Update([FromForm] ClassTimeUpdateDto dto, int id)
    {
        await _service.UpdateAsync(dto, id);
        return Ok();
    }

    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }
}
