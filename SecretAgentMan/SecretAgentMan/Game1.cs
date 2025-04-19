using System;
using System.IO;
using BroncoSettingsParser;
using BroncoSettingsParser.ResponseModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using RetroGame;
using RetroGame.Audio;
using RetroGame.HighScore;
using RetroGame.RetroTextures;
using RetroGame.Text;
using SecretAgentMan.OtherResources;
using SecretAgentMan.Scenes;
using SecretAgentMan.Scenes.IntroductionScenes;

namespace SecretAgentMan;

public class Game1 : RetroGame.RetroGame
{
    public static SettingCollection? Settings { get; set; }
    public static RetroTexture? CharactersTexture { get; set; }
    public static RetroTexture? BackgroundTempTexture { get; set; }
    public static RetroTextureVertical? WaterTexture { get; set; }
    public static RetroTexture? AirplaneRightTexture { get; set; }
    public static RetroTexture? AirplaneLeftTexture { get; set; }
    public static RetroTexture? GraveStoneTexture { get; set; }
    public static RetroTexture? IntroGraphics { get; set; }
    public static RetroTexture? GameOverGraphics1 { get; set; }
    public static RetroTexture? GameOverGraphics2 { get; set; }
    public static RetroTexture? GameOverGraphics3 { get; set; }
    public static RetroTexture? GameOverGraphics4 { get; set; }
    public static RetroTextureVertical? BackgroundLayer01 { get; set; }
    public static RetroTextureVertical? BackgroundLayer02 { get; set; }
    public static RetroTextureVertical? BackgroundLayer03 { get; set; }
    public static RetroTextureVertical? BackgroundLayer04 { get; set; }
    public static RetroTexture? CoinTexture { get; set; }
    public static RetroTexture? Hud { get; set; }
    public static RetroTexture? Frame { get; set; }
    public static RetroTexture? BonusLevelFrame { get; set; }
    public static Decoration Decoration { get; set; }
    public static SoundEffect? EnemyFire { get; set; }
    public static SoundEffect? PlayerFire { get; set; }
    public static SoundEffect? EnemyDie { get; set; }
    public static SoundEffect? PlayerDie { get; set; }
    public static SoundEffect? EnemyCoin { get; set; }
    public static SoundEffect? PlayerCoin { get; set; }
    public static Song? GameOverSong { get; set; }
    public static Song? LoaderSong { get; set; }
    public static Song? HiScoreSong { get; set; }
    public static Random Random;
    public static bool Cheat = false;
    public static int LastScore;
    public static int TodaysBestScore;
    public static HighScoreList HighScore { get; }
    public static TypeWriter TypeWriter { get; }
    public static IngameScene? CurrentIngameScene { get; set; }
    public static int BonusRoundSeconds { get; set; }
    public static bool LoaderSongIsPlaying { get; set; }

    static Game1()
    {
        Random = new Random();
        LastScore = 0;
        TodaysBestScore = 0;
        HighScore = new HighScoreList(640, 380, true, true, 220);
        Decoration = new Decoration();
        TypeWriter = new TypeWriter(70, 298, 6, ColorPalette.White);
        CurrentIngameScene = null;
    }

    public Game1() : base(640, 360, RetroDisplayMode.Fullscreen, false)
    {
        EnemyFire = new SoundEffect(this);
        PlayerFire = new SoundEffect(this);
        EnemyDie = new SoundEffect(this);
        PlayerDie = new SoundEffect(this);
        EnemyCoin = new SoundEffect(this);
        PlayerCoin = new SoundEffect(this);
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
        BackgroundLayer01 = RetroTextureVertical.LoadContent(GraphicsDevice, Content, 640, 91, 4, "background-640x91");
        BackgroundLayer02 = RetroTextureVertical.LoadContent(GraphicsDevice, Content, 640, 91, 4, "sky-640x91");
        BackgroundLayer03 = RetroTextureVertical.LoadContent(GraphicsDevice, Content, 640, 91, 4, "skyline_bg-640x91");
        BackgroundLayer04 = RetroTextureVertical.LoadContent(GraphicsDevice, Content, 640, 91, 4, "skyline_fg-640x91");
        CoinTexture = RetroTexture.LoadContent(GraphicsDevice, Content, 10, 10, 6, "coin10x10");
        CharactersTexture = RetroTexture.LoadContent(GraphicsDevice, Content, 25, 25, 32, "player25x25");
        Frame = RetroTexture.LoadContent(GraphicsDevice, Content, 640, 360, 1, "frame");
        BonusLevelFrame = RetroTexture.LoadContent(GraphicsDevice, Content, 640, 360, 2, "bonus-stars-640x360");
        GraveStoneTexture = RetroTexture.LoadContent(GraphicsDevice, Content, 25, 25, 13, "rip25x25");
        WaterTexture = RetroTextureVertical.LoadContent(GraphicsDevice, Content, 640, 30, 18, "water640x30");
        
        AirplaneRightTexture = new RetroTexture(GraphicsDevice, 5, 3, 25);
        AirplaneRightTexture.SetData(Content.Load<Texture2D>("plane5x3"));

        AirplaneLeftTexture = new RetroTexture(GraphicsDevice, 5, 3, 25);
        AirplaneLeftTexture.SetData(Content.Load<Texture2D>("planeflipped5x3"));

        GameOverGraphics1 = RetroTexture.LoadContent(GraphicsDevice, Content, 640, 360, 1, "gameover1");
        GameOverGraphics2 = RetroTexture.LoadContent(GraphicsDevice, Content, 640, 360, 1, "gameover2");
        GameOverGraphics3 = RetroTexture.LoadContent(GraphicsDevice, Content, 640, 360, 1, "gameover3");
        GameOverGraphics4 = RetroTexture.LoadContent(GraphicsDevice, Content, 640, 360, 1, "gameover4");
        MayorResources.LoadContent(GraphicsDevice, Content);
        IntroGraphics = RetroTexture.LoadContent(GraphicsDevice, Content, 640, 360, 1, "load-screen-360p-nofilter");
        EnemyFire!.Initialize("sfx_gun1", "sfx_gun2", "sfx_gun3", "sfx_gun4", "sfx_gun5", "sfx_gun6");
        PlayerFire!.Initialize("sfx_gun7", "sfx_gun8", "sfx_gun9", "sfx_gun10");
        EnemyDie!.Initialize("sfx_enemydeath1", "sfx_enemydeath2", "sfx_enemydeath3");
        PlayerDie!.Initialize("sfx_playerdeath");
        EnemyCoin!.Initialize("enemy_sfx_coin_1", "enemy_sfx_coin_2", "enemy_sfx_coin_3");
        PlayerCoin!.Initialize("player_sfx_coin_1", "player_sfx_coin_2");
        GameOverSong = Content.Load<Song>("game-over");
        LoaderSong = Content.Load<Song>("loader");
        HiScoreSong = Content.Load<Song>("hiscore");
        CurrentScene = new IntroScene(this);
        base.LoadContent();
    }
}