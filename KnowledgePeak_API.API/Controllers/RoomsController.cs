using KnowledgePeak_API.Business.Dtos.RoomDtos;
using KnowledgePeak_API.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgePeak_API.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoomsController : ControllerBase
{
    readonly IRoomService _serive;

    public RoomsController(IRoomService serive)
    {
        _serive = serive;
    }

    [HttpGet]
    [Authorize(Roles = "Director,Student,Tutor,Teacher,SuperAdmin,Admin")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _serive.GetAllAsync(true));
    }

    [Authorize(Roles = "Director,Student,Tutor,Teacher,SuperAdmin,Admin")]
    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _serive.GetByIdAsync(id, true));
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> Create([FromForm] RoomCreateDto dto)
    {
        await _serive.CreateAsync(dto);
        return Ok();
    }

    [HttpPut("[action]/{id}")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> Update([FromForm] RoomUpdateDto dto, int id)
    {
        await _serive.UpdateAsync(id, dto);
        return Ok();
    }

    [HttpDelete("[action]/{id}")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _serive.DeleteAsync(id);
        return Ok();
    }

    [HttpPatch("[action]/{id}")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        await _serive.SoftDeleteAsync(id);
        return Ok();
    }

    [HttpPatch("[action]/{id}")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> RevertSoftDelete(int id)
    {
        await _serive.RevertSoftDeleteAsync(id);
        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Count()
    {
        return Ok(await _serive.RoomCount());
    }
}
