
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