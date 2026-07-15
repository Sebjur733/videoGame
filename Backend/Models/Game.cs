namespace Backend.Models
{
public class Game
{
    public int Id { get; set; } // IGDB id

    public string GameName { get; set; }

    public string? CoverId { get; set; }

    public List<User> Users { get; set; } = new();
}
}