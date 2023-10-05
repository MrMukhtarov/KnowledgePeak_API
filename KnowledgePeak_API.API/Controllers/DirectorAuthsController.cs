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
    readonly IRoleService _roleService;

    public DirectorAuthsController(IDirectorService service, IRoleService roleService)
    {
        _service = service;
        _roleService = roleService;
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

    [HttpPatch("[action]/{id}")]
    public async Task<IActionResult> SoftDelete(string id)
    {
        await _service.SoftDeleteAsync(id);
        return Ok();
    }

    [HttpPatch("[action]/{id}")]
    public async Task<IActionResult> RevertSoftDelete(string id)
    {
        await _service.RevertSoftDeleteAsync(id);
        return Ok();
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }
    [Authorize(Roles = "Admin")]
    [HttpPost("[action]")]
    public async Task<IActionResult> AddRole([FromForm] AddRoleDto dto)
    {
        await _service.AddRole(dto);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}
