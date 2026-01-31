namespace sam_online_highscore_toolkit;

public class HighscoreServices
{
    public async Task<GlobalHighscoreList> GetGlobalHighscores()
    {
        ISettings settings = new Settings();
        using var httpClient = new HttpClient();
        return await GetGlobalHighscores(settings, httpClient);
    }

    public async Task<GlobalHighscoreList> GetGlobalHighscores(ISettings settings, HttpClient httpClient)
    {
        try
        {
            var data = await httpClient.GetStringAsync($"{settings.BaseUrl}gethighscore.php?format=csv");
            var records = data.Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var result = new GlobalHighscoreList();
            var pos = 0;

            foreach (var record in records)
            {
                pos++;
                var parts = record.Split('|', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                result.Add(new GlobalHighscore(pos, int.Parse(parts[0]), parts[1], parts[2]));
            }
            return result;
        }
        catch
        {
            return GlobalHighscoreList.CreateSubtitleMessage("failed to download global highscore list");
        }
    }

    public GlobalHighscoreList GetGlobalHighscoresSync()
    {
        ISettings settings = new Settings();
        using var httpClient = new HttpClient();
        return GetGlobalHighscoresSync(settings, httpClient);
    }

    public GlobalHighscoreList GetGlobalHighscoresSync(ISettings settings, HttpClient httpClient)
    {
        try
        {
            var data = httpClient.GetStringAsync($"{settings.BaseUrl}gethighscore.php?format=csv").Result;
            var records = data.Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var result = new GlobalHighscoreList();
            var pos = 0;

            foreach (var record in records)
            {
                pos++;
                var parts = record.Split('|', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                result.Add(new GlobalHighscore(pos, int.Parse(parts[0]), parts[1], parts[2]));
            }
            return result;
        }
        catch
        {
            return GlobalHighscoreList.CreateSubtitleMessage("failed to download global highscore list");
        }
    }

    public async Task<GlobalHighscoreList> SaveGlobalHighscoreEntry(int score, string playerName)
    {
        ISettings settings = new Settings();
        using var httpClient = new HttpClient();
        await httpClient.GetStringAsync($"{settings.BaseUrl}savehighscore.php?password={settings.Password}&score={score}&user={playerName}");
        return await GetGlobalHighscores(settings, httpClient);
    }
}