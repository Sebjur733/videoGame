using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Backend.DTOs;

public class IgdbService
{
    //authentication
    private readonly IConfiguration _config;

    public IgdbService(IConfiguration config)
    {
        _config = config;
    }

private static readonly JsonSerializerOptions IgdbJsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

//Task for searching games
    public async Task<List<GameSearchResultDto>> SearchGames(string query)
    {
        var client = new HttpClient();

        var clientId = _config["Igdb:ClientId"];
        var clientSecret = _config["Igdb:ClientSecret"];

        // 1. hent token fra Twitch
        var tokenRes = await client.PostAsync(
            "https://id.twitch.tv/oauth2/token",
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "grant_type", "client_credentials" }
            })
        );

        var tokenJson = await tokenRes.Content.ReadAsStringAsync();
        var token = JsonDocument.Parse(tokenJson)
            .RootElement.GetProperty("access_token").GetString();

        // 2. kall IGDB
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Add("Client-ID", clientId);
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        var body = $"fields name, cover.image_id, rating; where name ~ *\"{query}\"*; sort rating desc; limit 10;";

        var response = await client.PostAsync(
            "https://api.igdb.com/v4/games",
            new StringContent(body, Encoding.UTF8, "text/plain")
        );

        Console.WriteLine(response);

        var json = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException($"IGDB error: {(int)response.StatusCode} — {json}");

        var igdbItems = JsonSerializer.Deserialize<List<IgdbGameJson>>(json, IgdbJsonOptions)
                        ?? new List<IgdbGameJson>();

        return igdbItems.Select(g => new GameSearchResultDto
        {
            Id = g.Id,
            GameName = g.Name ?? "",
            CoverId = g.Cover?.ImageId,
            AlreadyInLibrary = false
        }).ToList();
    }

    private sealed class IgdbGameJson
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public IgdbCoverJson? Cover { get; set; }
    }

    private sealed class IgdbCoverJson
    {
        [JsonPropertyName("image_id")]
        public string? ImageId { get; set; }
    }
}