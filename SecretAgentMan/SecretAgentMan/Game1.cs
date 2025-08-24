using System;
using System.IO;
using BroncoSettingsParser;
using BroncoSettingsParser.ResponseModel;
using Microsoft.Xna.Framework;
using RetroGame;
using RetroGame.HighScore;
using RetroGame.RetroTextures;
using RetroGame.Text;
using SecretAgentMan.OtherResources;
using SecretAgentMan.Scenes;
using SecretAgentMan.Scenes.GameOverScenes;
using SecretAgentMan.Scenes.IntroductionScenes;
using SecretAgentMan.Sprites;

namespace SecretAgentMan;

public class Game1 : RetroGame.RetroGame
{
    public static SettingCollection? Settings { get; set; }
    public static RetroTexture? GraveStoneTexture { get; set; }
    public static RetroTexture? Hud { get; set; }
    public static RetroTexture? Frame { get; set; }
    public static RetroTexture? BonusLevelFrame { get; set; }
    public static RetroTexture? BonusMeter { get; set; }
    public static RetroTexture? LivesSymbolTexture { get; set; }
    public static RetroTexture? AmmoTexture { get; set; }
    public static RetroTexture? CutSceneFrame { get; set; }
    public static Decoration Decoration { get; set; }
    public static Random Random;
    public static bool Cheat = false;
    public static int LastScore;
    public static int TodaysBestScore;
    public static HighScoreList HighScore { get; }
    public const int HighScoreEditY = 220;
    public const int HighScoreViewY = 164;
    public static TypeWriter TypeWriter { get; }
    public static IngameScene? CurrentIngameScene { get; set; }
    public static int BonusRoundSeconds { get; set; }

    static Game1()
    {
        Random = new Random();
        LastScore = 0;
        TodaysBestScore = 0;
        HighScore = new HighScoreList(640, 380, true, true, HighScoreViewY);
        Decoration = new Decoration();
        TypeWriter = new TypeWriter(70, 298, 6, ColorPalette.White);
        CurrentIngameScene = null;
    }

    public Game1() : base(640, 360, RetroDisplayMode.Fullscreen, false)
    {
        SoundEffects.CreateSoundEffects(this);
    }

    protected override void LoadContent()
    {
        BackColor = Color.Black;

        var parser = new Parser(new FileInfo(Path.Combine(Tools.ExeFolder.FullName, "config.bronco")));
        var response = parser.Parse();

        if (response.Status != Status.Success)
            throw new SystemException("Settings cannot be parsed.");

        Settings = response.Settings;
        BonusRoundSeconds = int.Parse(Settings.GetValue("BonusRoundSeconds"));

        Hud = RetroTexture.LoadContent(GraphicsDevice, Content, 620, 59, 1, "hud");
        Frame = RetroTexture.LoadContent(GraphicsDevice, Content, 640, 360, 1, "frame");
        BonusLevelFrame = RetroTexture.LoadContent(GraphicsDevice, Content, 640, 360, 2, "bonus-stars-640x360");
        GraveStoneTexture = RetroTexture.LoadContent(GraphicsDevice, Content, 25, 25, 13, "rip25x25");
        BonusMeter = RetroTexture.LoadContent(GraphicsDevice, Content, 11, 51, 26, "bonus-meter");
        LivesSymbolTexture = RetroTexture.LoadContent(GraphicsDevice, Content, 11, 7, 1, "lives11x7");
        AmmoTexture = RetroTexture.LoadContent(GraphicsDevice, Content, 4, 7, 1, "ammo4x7");
        CutSceneFrame = RetroTexture.LoadContent(GraphicsDevice, Content, 640, 360, 1, "cutsceneframe");
        StartSceneResources.LoadContent(GraphicsDevice, Content);
        Coin.LoadContent(GraphicsDevice, Content);
        AmmoBox.LoadContent(GraphicsDevice, Content);
        Briefcase.LoadContent(GraphicsDevice, Content);
        GameOverFiredScene.LoadResources(GraphicsDevice, Content);
        IngameBackgroundResources.LoadContent(GraphicsDevice, Content);
        MayorResources.LoadContent(GraphicsDevice, Content);
        SoundEffects.LoadSoundEffects();
        Songs.LoadSongs(Content);
        CurrentScene = new IntroScene(this);
        base.LoadContent();
    }
}