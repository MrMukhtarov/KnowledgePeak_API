using KnowledgePeak_API.Business.Services.Interfaces;
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
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }
}
