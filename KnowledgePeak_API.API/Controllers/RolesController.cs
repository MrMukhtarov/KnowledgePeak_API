using KnowledgePeak_API.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgePeak_API.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _roleService.GetAllAsync());
    }

    [HttpGet("{id}")]
    //[Authorize(Roles = "Admin")]
    //[Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Get(string id)
    {
        return Ok(await _roleService.GetByIdAsync(id));
    }

    [HttpPost("[action]")]
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Create(string name)
    {
        await _roleService.CreateAsync(name);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("[action]/{id}")]
    //[Authorize(Roles = "Admin")]
    //[Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Update(string id, string name)
    {
        await _roleService.UpdateAsync(id, name);
        return StatusCode(StatusCodes.Status200OK);
    }

    [HttpDelete("[action]/{id}")]
    //[Authorize(Roles = "Admin")]
    //[Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Delete(string id)
    {
        await _roleService.RemoveAsync(id);
        return StatusCode(StatusCodes.Status200OK);
    }
}
