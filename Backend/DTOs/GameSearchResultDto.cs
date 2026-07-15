namespace Backend.DTOs;

/// <summary>
/// Shape returned to the frontend for IGDB search — matches the React <c>Game</c> type.
/// </summary>
public class GameSearchResultDto
{
    public int Id { get; set; }

    public string GameName { get; set; } = "";

    public string? CoverId { get; set; }

    public bool AlreadyInLibrary { get; set; }
}
