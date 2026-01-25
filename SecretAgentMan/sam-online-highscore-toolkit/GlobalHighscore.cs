namespace sam_online_highscore_toolkit;

public class GlobalHighscore
{
    public int Position { get; }
    public int Score { get; }
    public DateOnly Date { get; }
    public string PlayerName { get; }

    public GlobalHighscore(int position, int score, string date, string playerName)
    {
        Position = position;
        Score = score;
        Date = ParseDate(date);
        PlayerName = playerName;
    }

    public GlobalHighscore(int position, int score, DateOnly date, string playerName)
    {
        Position = position;
        Score = score;
        Date = date;
        PlayerName = playerName;
    }

    private DateOnly ParseDate(string date)
    {
        var parts = date.Split('-');

        if (parts.Length != 3)
            throw new FormatException("Invalid date format. Expected format: YYYY-MM-DD");
        
        var year = int.Parse(parts[0]);
        var month = int.Parse(parts[1]);
        var day = int.Parse(parts[2]);
        return new DateOnly(year, month, day);
    }
}