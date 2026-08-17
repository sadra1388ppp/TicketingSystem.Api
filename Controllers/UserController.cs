using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketingSystem.Api.Entities;
using TicketingSystem.Api.Interfaces;

namespace TicketingSystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public IActionResult Create(User user)
    {
        return Ok(_userService.Create(user));
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_userService.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
        var user = _userService.GetById(id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPut("{id}")]
    public IActionResult Update(string id, User user)
    {
        var result = _userService.Update(id, user);

        if (!result)
            return NotFound();

        return Ok("User updated");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        var result = _userService.Delete(id);

        if (!result)
            return NotFound();

        return Ok("User deleted");
    }
}