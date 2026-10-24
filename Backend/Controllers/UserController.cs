using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
   private readonly UserDbContext _context;
private readonly IConfiguration _configuration;
   
public UserController(
    UserDbContext context,
    IConfiguration configuration)
{
    _context = context;
    _configuration = configuration;
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
public async Task<IActionResult> Login(UserDto dto)
{
    var user = await _context.Users
        .FirstOrDefaultAsync(u => u.Username == dto.Username);

    if (user == null)
        return Unauthorized();

    if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        return Unauthorized();

    var claims = new[]
    {
        new Claim(ClaimTypes.Name, user.Username)
    };

    var key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
    );

    var credentials = new SigningCredentials(
        key,
        SecurityAlgorithms.HmacSha256
    );

    var token = new JwtSecurityToken(
        issuer: _configuration["Jwt:Issuer"],
        audience: _configuration["Jwt:Audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddHours(2),
        signingCredentials: credentials
    );

    var tokenString = new JwtSecurityTokenHandler()
        .WriteToken(token);

    return Ok(new
    {
        token = tokenString
    });
}

 [Authorize]
[HttpGet("getGames")]
public async Task<IActionResult> GetGames()
{
    var username = User.FindFirstValue(ClaimTypes.Name);

if (username == null)
    return Unauthorized();

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

[Authorize]
[HttpPost("addGames")]
public async Task<IActionResult> AddGame([FromBody] AddGameDto value)
{
    var username = User.FindFirstValue(ClaimTypes.Name);

if (username == null)
    return Unauthorized();

var user = await _context.Users
    .Include(u => u.OwnedGames)
    .FirstOrDefaultAsync(u => u.Username == username);

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