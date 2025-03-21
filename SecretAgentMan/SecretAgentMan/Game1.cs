using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using RetroGame.Audio;
using RetroGame.HighScore;
using RetroGame.RetroTextures;
using SecretAgentMan.OtherResources;
using SecretAgentMan.Scenes;

namespace SecretAgentMan;

public class Game1 : RetroGame.RetroGame
{
    public static RetroTexture? CharactersTexture { get; set; }
    public static RetroTexture? BackgroundTempTexture { get; set; }
    public static RetroTextureVertical? WaterTexture { get; set; }
    public static RetroTexture? AirplaneRightTexture { get; set; }
    public static RetroTexture? AirplaneLeftTexture { get; set; }
    public static RetroTexture? GraveStoneTexture { get; set; }
    public static RetroTexture? IntroGraphics1 { get; set; }
    public static RetroTexture? IntroGraphics2 { get; set; }
    public static RetroTexture? IntroGraphics3 { get; set; }
    public static RetroTexture? GameOverGraphics1 { get; set; }
    public static RetroTexture? GameOverGraphics2 { get; set; }
    public static RetroTexture? GameOverGraphics3 { get; set; }
    public static RetroTextureVertical? BackgroundLayer01 { get; set; }
    public static RetroTextureVertical? BackgroundLayer02 { get; set; }
    public static RetroTextureVertical? BackgroundLayer03 { get; set; }
    public static RetroTextureVertical? BackgroundLayer04 { get; set; }
    public static RetroTexture? CoinTexture { get; set; }
    public static Decoration Decoration { get; set; }
    public static SoundEffect? EnemyFire { get; set; }
    public static SoundEffect? PlayerFire { get; set; }
    public static SoundEffect? EnemyDie { get; set; }
    public static SoundEffect? PlayerDie { get; set; }
    public static SoundEffect? EnemyCoin { get; set; }
    public static SoundEffect? PlayerCoin { get; set; }
    public static Random Random;
    public static bool Cheat = false;
    public static int LastScore;
    public static int TodaysBestScore;
    public static HighScoreList HighScore { get; }

    static Game1()
    {
        Random = new Random();
        LastScore = 0;
        TodaysBestScore = 0;
        HighScore = new HighScoreList(640, 380);
        Decoration = new Decoration();
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
        BackgroundLayer01 = RetroTextureVertical.LoadContent(GraphicsDevice, Content, 640, 91, 4, "background-640x91");
        BackgroundLayer02 = RetroTextureVertical.LoadContent(GraphicsDevice, Content, 640, 91, 4, "sky-640x91");
        BackgroundLayer03 = RetroTextureVertical.LoadContent(GraphicsDevice, Content, 640, 91, 4, "skyline_bg-640x91");
        BackgroundLayer04 = RetroTextureVertical.LoadContent(GraphicsDevice, Content, 640, 91, 4, "skyline_fg-640x91");
        CoinTexture = RetroTexture.LoadContent(GraphicsDevice, Content, 14, 14, 4, "coin14x14");
        CharactersTexture = RetroTexture.LoadContent(GraphicsDevice, Content, 25, 25, 32, "player25x25");

        GraveStoneTexture = new RetroTexture(GraphicsDevice, 25, 25, 13);
        GraveStoneTexture.SetData(Content.Load<Texture2D>("rip25x25"));

        WaterTexture = new RetroTextureVertical(GraphicsDevice, 640, 30, 18);
        WaterTexture.SetData(Content.Load<Texture2D>("water640x30"));

        AirplaneRightTexture = new RetroTexture(GraphicsDevice, 5, 3, 25);
        AirplaneRightTexture.SetData(Content.Load<Texture2D>("plane5x3"));

        AirplaneLeftTexture = new RetroTexture(GraphicsDevice, 5, 3, 25);
        AirplaneLeftTexture.SetData(Content.Load<Texture2D>("planeflipped5x3"));

        GameOverGraphics1 = new RetroTexture(GraphicsDevice, 640, 360, 1);
        GameOverGraphics1.SetData(Content.Load<Texture2D>("gameover1"));

        GameOverGraphics2 = new RetroTexture(GraphicsDevice, 640, 360, 1);
        GameOverGraphics2.SetData(Content.Load<Texture2D>("gameover2"));

        GameOverGraphics3 = new RetroTexture(GraphicsDevice, 640, 360, 1);
        GameOverGraphics3.SetData(Content.Load<Texture2D>("gameover3"));

        MayorResources.LoadContent(GraphicsDevice, Content);

        IntroGraphics1 = new RetroTexture(GraphicsDevice, 640, 360, 1);
        IntroGraphics1.SetData(Content.Load<Texture2D>("preloadscreen0"));

        IntroGraphics2 = new RetroTexture(GraphicsDevice, 640, 360, 1);
        IntroGraphics2.SetData(Content.Load<Texture2D>("preloadscreen1"));

        IntroGraphics3 = new RetroTexture(GraphicsDevice, 640, 360, 1);
        IntroGraphics3.SetData(Content.Load<Texture2D>("load-screen-360p-nofilter"));

        EnemyFire!.Initialize("sfx_gun1", "sfx_gun2", "sfx_gun3", "sfx_gun4", "sfx_gun5", "sfx_gun6");
        PlayerFire!.Initialize("sfx_gun7", "sfx_gun8", "sfx_gun9", "sfx_gun10");
        EnemyDie!.Initialize("sfx_enemydeath1", "sfx_enemydeath2", "sfx_enemydeath3");
        PlayerDie!.Initialize("sfx_playerdeath");
        EnemyCoin!.Initialize("enemy_sfx_coin_1", "enemy_sfx_coin_2", "enemy_sfx_coin_3");
        PlayerCoin!.Initialize("player_sfx_coin_1", "player_sfx_coin_2");

        CurrentScene = new IntroScene(this);
        base.LoadContent();
    }
}