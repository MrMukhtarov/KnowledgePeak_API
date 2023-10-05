﻿using KnowledgePeak_API.Business.Dtos.GroupDtos;
using KnowledgePeak_API.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgePeak_API.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupsController : ControllerBase
{
    readonly IGroupService _service;

    public GroupsController(IGroupService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAllAsync(true));
    }

    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _service.GetByIdAsync(id, true));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromForm] GroupCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("[action]/{id}")]
    public async Task<IActionResult> Update([FromForm] GroupUpdateDto dto, int id)
    {
        await _service.UpdateAsync(id, dto);
        return StatusCode(StatusCodes.Status200OK);
    }

    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return StatusCode(StatusCodes.Status200OK);
    }

    [HttpPatch("[action]/{id}")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        await _service.SoftDeleteAsync(id);
        return StatusCode(StatusCodes.Status200OK);
    }

    [HttpPatch("[action]/{id}")]
    public async Task<IActionResult> RevertSoftDelete(int id)
    {
        await _service.RevertSoftDeleteAsync(id);
        return StatusCode(StatusCodes.Status200OK);
    }
}