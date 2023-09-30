using KnowledgePeak_API.Business.Dtos.UniversityDtos;
using KnowledgePeak_API.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgePeak_API.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UniversitysController : ControllerBase
{
    readonly IUniversityService _service;

    public UniversitysController(IUniversityService service)
    {
        _service = service;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromForm] UniversityCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("[action]/{id}")]
    public async Task<IActionResult> Update([FromForm]UniversityUpdateDto dto, int id)
    {
        await _service.UpdateAsync(id, dto);
        return StatusCode(StatusCodes.Status200OK);
    }
}
