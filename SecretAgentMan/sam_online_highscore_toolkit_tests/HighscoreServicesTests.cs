using sam_online_highscore_toolkit;

namespace sam_online_highscore_toolkit_tests;

[TestClass]
public sealed class HighscoreServicesTests
{
    [TestMethod]
    public void GetGlobalHighscores()
    {
        var response = new HighscoreServices().GetGlobalHighscores().Result;

        foreach (var x in response)
        {
            Console.WriteLine($"{x.Position} - {x.Score} - {x.Date} - {x.PlayerName}");
        }
    }

    [TestMethod]
    public void CreateSubtitleMessage()
    {
        var response = GlobalHighscoreList.CreateSubtitleMessage("this is a test subtitle message");

        foreach (var x in response)
        {
            Console.WriteLine($"{x.Position} - {x.Score} - {x.Date} - {x.PlayerName}");
        }
    }
}