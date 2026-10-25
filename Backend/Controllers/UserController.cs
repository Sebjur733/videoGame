using Backend.Models;
using Backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    // REGISTER
    [HttpPost("register")]
    public async Task<IActionResult> CreateUser(UserDto dto)
    {
        var registered = await _userService.RegisterAsync(dto);

        if (!registered)
            return BadRequest("User already exists");

        return Ok("User registered");
    }

    // LOGIN
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserDto dto)
    {
        var token = await _userService.LoginAsync(dto);

        if (token == null)
            return Unauthorized();

        return Ok(new { token });
    }

// user search controller
    [Authorize]
    [HttpGet("findUser")]
    public async Task<IActionResult> FindUser([FromQuery] string userName)
    {
        if (User.FindFirstValue(ClaimTypes.Name) == null)
            return Unauthorized();

        var users = await _userService.FindUsers(userName);

        return Ok(users);

    }


    // GET GAMES
    [Authorize]
    [HttpGet("getGames")]
    public async Task<IActionResult> GetGames()
    {
        var username = User.FindFirstValue(ClaimTypes.Name);

        if (username == null)
            return Unauthorized();

        var games = await _userService.GetGamesAsync(username);

        if (games == null)
            return NotFound("User not found");

        return Ok(games);
    }

// GET games based on username search
[Authorize]
[HttpGet("getGamesForUser/{userName}")]
public async Task<IActionResult> GetGamesForUser(
    [FromRoute] string userName)
{
    var currentUsername = User.FindFirstValue(ClaimTypes.Name);

    if (currentUsername == null)
        return Unauthorized();

//Using same function as getgames for own user
    var games = await _userService.GetGamesAsync(userName);

    if (games == null)
        return NotFound("User not found");

    return Ok(games);
}


    // ADD GAME
    [Authorize]
    [HttpPost("addGames")]
    public async Task<IActionResult> AddGame(
        [FromBody] AddGameDto value)
    {
        var username = User.FindFirstValue(ClaimTypes.Name);

        if (username == null)
            return Unauthorized();

        var games = await _userService.AddGameAsync(username, value);

        if (games == null)
            return NotFound("User not found");

        return Ok(games);
    }
}
