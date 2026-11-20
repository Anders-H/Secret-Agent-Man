
namespace sam_online_highscore_toolkit;

public class GlobalHighscore
{
    public int Position { get; internal set; }
    public int Score { get; }
    public string TimeRaw { get; set; }
    public DateOnly Date { get; }
    public string PlayerName { get; }

    public GlobalHighscore() : this(0, 0, "1980-01-01", "")
    {
    }

    public GlobalHighscore(int position) : this(position, 0, "1980-01-01", "")
    {
    }

    public GlobalHighscore(int position, int score, string date, string playerName)
    {
        Position = position;
        Score = score;
        TimeRaw = "";
        Date = ParseDate(date);
        PlayerName = playerName;
    }

    public GlobalHighscore(int position, int score, DateOnly date, string playerName)
    {
        Position = position;
        Score = score;
        TimeRaw = "";
        Date = date;
        PlayerName = playerName;
    }

    private static DateOnly ParseDate(string date) =>
        DateOnly.ParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
}
