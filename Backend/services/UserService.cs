
using Backend.Data;
using Backend.Models;
using Backend.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class UserService
{
    private readonly UserDbContext _context;
    private readonly IConfiguration _configuration;

    public UserService(
        UserDbContext context,
        IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    // REGISTER
    public async Task<bool> RegisterAsync(UserDto dto)
    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == dto.Username);

        if (existingUser != null)
            return false;

        var user = new User
        {
            Username = dto.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return true;
    }

    // LOGIN
    public async Task<string?> LoginAsync(UserDto dto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == dto.Username);

        if (user == null)
            return null;

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return null;

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

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<List<UserSearchResultDto>> FindUsers(string name)
    {
        return await _context.Users
        .Where(u => u.Username.Contains(name))
        .Select(u => new UserSearchResultDto
        {
            Username = u.Username
        })
        .ToListAsync();
    } 
   
    // GET GAMES
    public async Task<List<GameDto>?> GetGamesAsync(string username)
    {
        var user = await _context.Users
            .Include(u => u.OwnedGames)
            .FirstOrDefaultAsync(u => u.Username == username);

        if (user == null)
            return null;

        return user.OwnedGames.Select(g => new GameDto
        {
            Id = g.Id,
            GameName = g.GameName,
            CoverId = g.CoverId
        }).ToList();
    }

    // ADD GAME
    public async Task<List<GameDto>?> AddGameAsync(
        string username,
        AddGameDto value)
    {
        var user = await _context.Users
            .Include(u => u.OwnedGames)
            .FirstOrDefaultAsync(u => u.Username == username);

        if (user == null)
            return null;

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

        return user.OwnedGames.Select(g => new GameDto
        {
            Id = g.Id,
            GameName = g.GameName,
            CoverId = g.CoverId
        }).ToList();
    }
}
