namespace sam_online_highscore_toolkit;

public class GlobalHighscoreList : List<GlobalHighscore>
{
    public static GlobalHighscoreList CreateSubtitleMessage(string message)
    {
        var result = new GlobalHighscoreList();
        var parts = new List<string>();

        while (message.Length > 0)
        {
            if (message.Length <= 0)
                break;

            if (message.Length == 1)
            {
                parts.Add(message[..1] + "  ");
                break;
            }

            if (message.Length == 2)
            {
                parts.Add(message[..2] + " ");
                break;
            }

            if (message.Length == 3)
            {
                parts.Add(message[..3]);
                break;
            }

            parts.Add(message[..3]);
            message = message[3..];
        }

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