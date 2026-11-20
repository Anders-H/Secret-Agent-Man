namespace sam_online_highscore_toolkit;

public class GlobalHighscoreList : List<GlobalHighscore>
{
    public static GlobalHighscoreList CreateSubtitleMessage(string message)
    {
        var result = new GlobalHighscoreList();
        var parts = new List<string>();

        for (var i = 0; i < message.Length; i += 3)
            parts.Add(message.Substring(i, Math.Min(3, message.Length - i)).PadRight(3));

        var count = parts.Count;
        var position = 0;
        var date = new DateOnly(1980, 1, 1);

        foreach (var part in parts)
        {
            position++;
            result.Add(new GlobalHighscore(position, count, date, part));
            count--;
            date = date.AddDays(1);
        }

        return result;
    }
}
