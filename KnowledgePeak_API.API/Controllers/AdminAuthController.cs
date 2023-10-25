using KnowledgePeak_API.Business.Dtos.AdminDtos;
using KnowledgePeak_API.Business.Dtos.RoleDtos;
using KnowledgePeak_API.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgePeak_API.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminAuthController : ControllerBase
{
    readonly IAdminService _service;

    public AdminAuthController(IAdminService service)
    {
        _service = service;
    }

    [HttpGet("[action]")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Create([FromForm] AdminCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromForm] AdminLoginDto dto)
    {
        return Ok(await _service.LoginAsync(dto));
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "SuperAdmin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAccount([FromForm] AdminUpdateDto dto)
    {
        await _service.UpdateAsync(dto);
        return Ok();
    }

    [HttpDelete("[action]")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Delete(string userName)
    {
        await _service.DeleteAsync(userName);
        return Ok();
    }
    [HttpGet("[action]/{id}")]
    [Authorize(Roles = "SuperAdmin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById(string id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }
    [HttpPost("[action]")]
    [Authorize(Roles = "SuperAdmin")] 
    public async Task<IActionResult> AddRole([FromForm] AddRoleDto dto)
    {
        await _service.AddRole(dto);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> RemoveRole([FromForm] RemoveRoleDto dto)
    {
        await _service.RemoveRole(dto);
        return StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "SuperAdmin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> LoginWithRefreshToken(string refreshToken)
    {
        return Ok(await _service.LoginWithRefreshTokenAsync(refreshToken));
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> UpdateProfileAdmin([FromForm] AdminUpdateDto dto, string userName)
    {
        await _service.UpdateProfileAdminAsync(userName, dto);
        return StatusCode(StatusCodes.Status200OK);
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "SuperAdmin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> SignOut()
    {
        await _service.SignOut();
        return Ok();
    }
}
