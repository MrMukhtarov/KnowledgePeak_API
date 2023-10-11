﻿using KnowledgePeak_API.Business.Dtos.RoleDtos;
using KnowledgePeak_API.Business.Dtos.TutorDtos;
using KnowledgePeak_API.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgePeak_API.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TutorAuthController : ControllerBase
{
    readonly ITutorService _service;

    public TutorAuthController(ITutorService service)
    {
        _service = service;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromForm] TutorCreateDto dto)
    {
        await _service.CreateAsync(dto);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromForm] TutorLoginDto dto)
    {
        return Ok(await _service.LoginAsync(dto));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> LoginWithRefreshToken(string token)
    {
        return Ok(await _service.LoginWithRefreshTokenAsync(token));
    }


    [HttpGet("[action]")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync(true));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddRole([FromForm]AddRoleDto dto)
    {
        await _service.AddRoleAsync(dto);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> RemoveRole([FromForm] RemoveRoleDto dto)
    {
        await _service.RemoveRole(dto);
        return Ok();
    }

}
