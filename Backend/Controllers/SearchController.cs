using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly IgdbService _igdb;

// Receiving DI
    public GameController(IgdbService igdb)
    {
        _igdb = igdb;
    }

// TODO: gotta change to search
// Handles game search
    [HttpPost("send")]
    public async Task<IActionResult> Receive([FromBody] string value)
    {
        Console.WriteLine("in controller before api result: " + value);
        var result = await _igdb.SearchGames(value);
        return Ok(result);
    }
}