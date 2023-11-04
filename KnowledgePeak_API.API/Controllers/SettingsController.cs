using KnowledgePeak_API.Business.Dtos.SettingDtos;
using KnowledgePeak_API.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace KnowledgePeak_API.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SettingsController : ControllerBase
{
    readonly ISettingService _service;

    public SettingsController(ISettingService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> Create([FromForm] SettingCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("[action]/{id}")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> Update([FromForm] SettingUpdateDto dto, int id)
    {
        await _service.UpdateAsync(dto, id);
        return StatusCode(StatusCodes.Status200OK);
    }
}
