using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Data;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
   private readonly UserDbContext _context;
   
public UserController(UserDbContext context)
    {
        _context = context;
    }
   

    // REGISTER
    [HttpPost("register")]
    public async Task<ActionResult<User>> CreateUser(UserDto dto)
    {
        var existingUser = await _context.Users
        .FirstOrDefaultAsync(u => u.Username == dto.Username);

        if (existingUser != null)
        return BadRequest("User already exists");

        var user = new User
    {
        Username = dto.Username,
        PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
    };

    _context.Users.Add(user);
    await _context.SaveChangesAsync();

    return Ok("User registered");
    }

 // LOGIN
[HttpPost("login")]
public async Task<ActionResult<User>> Login(UserDto dto)
{
    var user = await _context.Users
        .FirstOrDefaultAsync(u => u.Username == dto.Username);

    if (user == null)
        return Unauthorized();

    if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        return Unauthorized();

    return Ok(user);
}

 [HttpGet("getGames/{username}")]
public async Task<IActionResult> GetGames(string username)
{
    var user = await _context.Users
        .Include(u => u.OwnedGames)
        .FirstOrDefaultAsync(u => u.Username == username);

    if (user == null)
        return NotFound("User not found");

    var games = user.OwnedGames.Select(g => new
    {
        g.Id,
        g.GameName,
        g.CoverId
    });

    return Ok(games);
}

[HttpPost("addGames")]
public async Task<IActionResult> AddGame([FromBody] AddGameDto value)
{
    if (value == null || string.IsNullOrWhiteSpace(value.User))
        return BadRequest();

    var user = await _context.Users
        .Include(u => u.OwnedGames)
        .FirstOrDefaultAsync(u => u.Username == value.User);

    if (user == null)
        return NotFound();

    var game = await _context.Games.FindAsync(value.Game.Id);

    if (game == null)
    {
        game = new Game
        {
            Id = value.Game.Id,
            GameName = value.Game.GameName,
            CoverId = value.Game.CoverId
        };
        _context.Games.Add(game);
    }

    if (!user.OwnedGames.Any(g => g.Id == game.Id))
    {
    user.OwnedGames.Add(game);
    }

    await _context.SaveChangesAsync();

    return Ok(user.OwnedGames.Select(g => new GameDto
{
    Id = g.Id,
    GameName = g.GameName,
    CoverId = g.CoverId
}));
}
}