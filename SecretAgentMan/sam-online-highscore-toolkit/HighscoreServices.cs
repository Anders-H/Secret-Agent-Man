namespace sam_online_highscore_toolkit;

public class HighscoreServices
{
    public async Task<GlobalHighscoreList> GetGlobalHighscores()
    {
        try
        {
            ISettings settings = new Settings();
            var httpClient = new HttpClient();
            var data = await httpClient.GetStringAsync($"{settings.BaseUrl}gethighscore.php?format=json");
            var list = System.Text.Json.JsonSerializer.Deserialize<dynamic>(data);

            if (list == null)
                return GlobalHighscoreList.CreateSubtitleMessage("no highscores found");

            foreach (var item in list)
        }
        catch (Exception e)
        {
            return GlobalHighscoreList.CreateSubtitleMessage("failed to download global highscore list");
        }
    }
}