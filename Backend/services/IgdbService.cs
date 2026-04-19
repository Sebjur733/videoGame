using System.Text;
using System.Text.Json;

public class IgdbService
{
    //authentication
    private readonly IConfiguration _config;

    public IgdbService(IConfiguration config)
    {
        _config = config;
    }

//Task for searching games
    public async Task<string> SearchGames(string query)
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

        var body = $"search \"{query}\"; fields name; limit 10;";

        var response = await client.PostAsync(
            "https://api.igdb.com/v4/games",
            new StringContent(body, Encoding.UTF8, "text/plain")
        );

        Console.WriteLine(response);
        return await response.Content.ReadAsStringAsync();
        
    }
}