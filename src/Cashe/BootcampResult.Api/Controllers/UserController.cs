using AutoMapper;
using BootcampResult.Api.Models.Dtos;
using BootcampResult.Application.Common.Identity.Services;
using BootcampResult.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BootcampResult.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> GetAll() =>
        Ok(await userService.GetAllAsync(true));
    
    [HttpGet("{id:guid}")]
    public async ValueTask<IActionResult> GetById([FromRoute] Guid id) =>
        Ok(await userService.GetByIdAsync(id, true, HttpContext.RequestAborted));

    [HttpPost]
    public async ValueTask<IActionResult> Create([FromBody] UserDto user) =>
        Ok(await userService.CreateAsync(mapper.Map<User>(user), true, HttpContext.RequestAborted));

    [HttpPut]
    public async ValueTask<IActionResult> Update([FromBody] UserDto user)
    {
        var foundUser = await userService.GetByIdAsync(user.Id, true, HttpContext.RequestAborted);

        var mapped = mapper.Map(user, foundUser);
        
        return Ok(await userService.UpdateAsync(mapped!, true, HttpContext.RequestAborted));
    }

    [HttpDelete("{id:guid}")]
    public async ValueTask<IActionResult> DeleteById([FromRoute] Guid id) =>
        Ok(await userService.DeleteByIdAsync(id, true, HttpContext.RequestAborted));
}