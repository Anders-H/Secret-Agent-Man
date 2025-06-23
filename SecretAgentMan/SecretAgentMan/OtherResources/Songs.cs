using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace SecretAgentMan.OtherResources;

public static class Songs
{
    public static Song? GameOverSong { get; set; }
    public static Song? LoaderSong { get; set; }
    public static Song? HiScoreSong { get; set; }
    public static Song? BonusLevelSong { get; set; }
    public static Song? CutSceneSong { get; set; }
    public static Song? IngameSong { get; set; }

    public static void LoadSongs(ContentManager content)
    {
        GameOverSong = content.Load<Song>("game-over");
        LoaderSong = content.Load<Song>("loader");
        HiScoreSong = content.Load<Song>("hiscore");
        BonusLevelSong = content.Load<Song>("bonuslevel");
        CutSceneSong = content.Load<Song>("cutscene");
        IngameSong = content.Load<Song>("ingame");
    }
}