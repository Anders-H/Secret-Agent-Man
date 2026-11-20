using System.Globalization;

namespace sam_online_highscore_toolkit;

public class HighscoreServices
{
    private static readonly HttpClient SharedClient = new() { Timeout = TimeSpan.FromSeconds(10) };
    private readonly ISettings _settings;
    private readonly HttpClient _client;

    public HighscoreServices(ISettings? settings = null, HttpClient? client = null)
    {
        _settings = settings ?? new Settings();
        _client = client ?? SharedClient;
    }

    public Task<GlobalHighscoreList> GetGlobalHighscores() =>
        GetGlobalHighscores(_settings, _client);

    public async Task<GlobalHighscoreList> GetGlobalHighscores(ISettings settings, HttpClient httpClient)
    {
        try
        {
            var data = await httpClient.GetStringAsync($"{settings.BaseUrl}gethighscore.php?format=csv").ConfigureAwait(false);
            return Parse(data);
        }
        catch (Exception exception) when (exception is HttpRequestException or OperationCanceledException or FormatException or ArgumentException or OverflowException)
        {
            return GlobalHighscoreList.CreateSubtitleMessage("failed to download global highscore list");
        }
    }

    // Kept for callers outside the game; the game itself never blocks on HTTP.
    public GlobalHighscoreList GetGlobalHighscoresSync() =>
        GetGlobalHighscores().GetAwaiter().GetResult();

    public GlobalHighscoreList GetGlobalHighscoresSync(ISettings settings, HttpClient httpClient) =>
        GetGlobalHighscores(settings, httpClient).GetAwaiter().GetResult();

    private static GlobalHighscoreList Parse(string data)
    {
        var result = new GlobalHighscoreList();
        foreach (var record in data.Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
        {
            var parts = record.Split('|', StringSplitOptions.TrimEntries);
            if (parts.Length != 3 || string.IsNullOrWhiteSpace(parts[2]))
                throw new FormatException("Invalid highscore record.");
            result.Add(new GlobalHighscore(result.Count + 1,
                int.Parse(parts[0], CultureInfo.InvariantCulture), parts[1], parts[2]));
        }
        return result;
    }

    public async Task<GlobalHighscoreList> SaveGlobalHighscoreEntry(int score, string playerName)
    {
        try
        {
            var url = $"{_settings.BaseUrl}savehighscore.php?password={Uri.EscapeDataString(_settings.Password)}&score={score.ToString(CultureInfo.InvariantCulture)}&user={Uri.EscapeDataString(playerName)}";
            await _client.GetStringAsync(url).ConfigureAwait(false);
            // The refresh starts only after the server has acknowledged the save.
            return await GetGlobalHighscores().ConfigureAwait(false);
        }
        catch (Exception exception) when (exception is HttpRequestException or OperationCanceledException)
        {
            return GlobalHighscoreList.CreateSubtitleMessage("failed to save global highscore entry");
        }
    }
}
