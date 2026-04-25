using Microsoft.AspNetCore.Mvc;
using Backend.Models;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }


// Handles user reg
    [HttpPost("register")]
public async Task<IActionResult> Receive([FromBody] User user)
{
    Console.WriteLine(user.Username);
    Console.WriteLine(user.Password);

    var result = _userService.RegUser(user.Username, user.Password);

    return Ok(result);
}
}