namespace sam_online_highscore_toolkit;

public class HighscoreServices
{
    public static bool UseOnlineHighscore { get; set; }

    static HighscoreServices()
    {
        UseOnlineHighscore = true;
    }
}