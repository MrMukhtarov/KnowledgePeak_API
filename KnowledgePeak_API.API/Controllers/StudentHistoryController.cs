using KnowledgePeak_API.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgePeak_API.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentHistoryController : ControllerBase
{
    readonly IStudentHistoryService _service;

    public StudentHistoryController(IStudentHistoryService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("[action]/{id}")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }
}
