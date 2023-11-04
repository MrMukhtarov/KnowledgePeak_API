using KnowledgePeak_API.Business.Dtos.SpecialityDtos;
using KnowledgePeak_API.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgePeak_API.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpecialitiesController : ControllerBase
{
    readonly ISpecialityService _service;

    public SpecialitiesController(ISpecialityService service)
    {
        _service = service;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAllAsync(true));
    }

    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _service.GetBydIdAsync(id, true));
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> Create([FromForm] SpecialityCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("[action]/{id}")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> AddFaculty([FromForm] SepcialityAddFacultyDto dto, int id)
    {
        await _service.AddFacultyAsync(id, dto);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("[action]/{id}")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> AddLesson([FromForm] SpecialityAddLessonDto dto, int id)
    {
        await _service.AddLessonAsync(id, dto);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("[action]/{id}")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> Update([FromForm] SpecialityUpdateDto dto, int id)
    {
        await _service.UpdateAsync(id, dto);
        return StatusCode(StatusCodes.Status200OK);
    }

    [HttpDelete("[action]/{id}")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return StatusCode(StatusCodes.Status200OK);
    }

    [HttpPatch("[action]/{id}")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        await _service.SoftDeleteAsync(id);
        return StatusCode(StatusCodes.Status200OK);
    }

    [HttpPatch("[action]/{id}")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> RevertSoftDelete(int id)
    {
        await _service.RevertSoftDeleteAsync(id);
        return StatusCode(StatusCodes.Status200OK);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Count()
    {
        return Ok(await _service.SpecialityCount());
    }
}
