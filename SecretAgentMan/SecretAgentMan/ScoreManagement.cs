namespace SecretAgentMan;

public static class ScoreManagement
{
    public static void StoreLastScore(int score)
    {
        Game1.LastScore = score;

        if (Game1.TodaysBestScore < Game1.LastScore)
            Game1.TodaysBestScore = Game1.LastScore;
    }
}