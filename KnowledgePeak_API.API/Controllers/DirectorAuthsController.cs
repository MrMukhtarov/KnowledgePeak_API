using KnowledgePeak_API.Business.Dtos.DirectorDtos;
using KnowledgePeak_API.Business.Dtos.RoleDtos;
using KnowledgePeak_API.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgePeak_API.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DirectorAuthsController : ControllerBase
{
    readonly IDirectorService _service;

    public DirectorAuthsController(IDirectorService service)
    {
        _service = service;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromForm] DirectorCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromForm] DIrectorLoginDto dto)
    {
        return Ok(await _service.LoginAsync(dto));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> UpdateAccount([FromForm] DirectorUpdateDto dto)
    {
        await _service.UpdatePrfileAsync(dto);
        return Ok();
    }

    [HttpDelete("[action]")]
    public async Task<IActionResult> Delete(string userName)
    {
        await _service.DeleteAsync(userName);
        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync(true));
    }

    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        return Ok(await _service.GetByIdAsync(id,true));
    }
    //[Authorize(Roles = "Admin")]
    [HttpPost("[action]")]
    public async Task<IActionResult> AddRole([FromForm] AddRoleDto dto)
    {
        await _service.AddRole(dto);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("[action]")]
    public async Task<IActionResult> RemoveRole([FromForm] RemoveRoleDto dto)
    {
        await _service.RemoveRole(dto);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> LoginWithRefreshToken(string refreshToken)
    {
        return Ok(await _service.LoginWithRefreshTokenAsync(refreshToken));
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateProfileAdmin([FromForm] DirectorUpdateAdminDto dto, string userName)
    {
        await _service.UpdateProfileAdminAsync(userName, dto);
        return StatusCode(StatusCodes.Status200OK);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> SignOut()
    {
        await _service.SignOut();
        return Ok();
    }
}
