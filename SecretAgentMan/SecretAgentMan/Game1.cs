using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using RetroGame.Audio;
using RetroGame.HighScore;
using RetroGame.RetroTextures;
using SecretAgentMan.Scenes;

namespace SecretAgentMan;

public class Game1 : RetroGame.RetroGame
{
    public static RetroTexture? CharactersTexture { get; set; }
    public static RetroTexture? BackgroundTempTexture { get; set; }
    public static RetroTextureVertical? WaterTexture { get; set; }
    public static RetroTexture? AirplaneRightTexture { get; set; }
    public static RetroTexture? AirplaneLeftTexture { get; set; }
    public static RetroTexture? Mayor { get; set; }
    public static RetroTexture? IntroGraphics { get; set; }
    public static RetroTextureVertical? BackgroundLayer01 { get; set; }
    public static RetroTextureVertical? BackgroundLayer02 { get; set; }
    public static Decoration Decoration { get; set; }
    public static SoundEffect? EnemyFire { get; set; }
    public static SoundEffect? PlayerFire { get; set; }
    public static SoundEffect? EnemyDie { get; set; }
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
    }

    protected override void LoadContent()
    {
        BackColor = Color.Black;

        BackgroundLayer01 = new RetroTextureVertical(GraphicsDevice, 640, 91, 4);
        BackgroundLayer01.SetData(Content.Load<Texture2D>("background-640x91"));

        BackgroundLayer02 = new RetroTextureVertical(GraphicsDevice, 640, 91, 4);
        BackgroundLayer02.SetData(Content.Load<Texture2D>("sky-640x91"));

        CharactersTexture = new RetroTexture(GraphicsDevice, 25, 25, 32);
        CharactersTexture.SetData(Content.Load<Texture2D>("player25x25"));

        WaterTexture = new RetroTextureVertical(GraphicsDevice, 640, 30, 18);
        WaterTexture.SetData(Content.Load<Texture2D>("water640x30"));

        AirplaneRightTexture = new RetroTexture(GraphicsDevice, 5, 3, 25);
        AirplaneRightTexture.SetData(Content.Load<Texture2D>("plane5x3"));

        AirplaneLeftTexture = new RetroTexture(GraphicsDevice, 5, 3, 25);
        AirplaneLeftTexture.SetData(Content.Load<Texture2D>("planeflipped5x3"));

        Mayor = new RetroTexture(GraphicsDevice, 50, 50, 2);
        Mayor.SetData(Content.Load<Texture2D>("mayor50x50"));

        IntroGraphics = new RetroTexture(GraphicsDevice, 640, 360, 1);
        IntroGraphics.SetData(Content.Load<Texture2D>("load-screen-360p-nofilter"));

        EnemyFire!.Initialize("sfx_gun1", "sfx_gun2", "sfx_gun3", "sfx_gun4", "sfx_gun5", "sfx_gun6");
        PlayerFire!.Initialize("sfx_gun7", "sfx_gun8", "sfx_gun9", "sfx_gun10");
        EnemyDie!.Initialize("sfx_enemydeath1", "sfx_enemydeath2", "sfx_enemydeath3");

        CurrentScene = new IntroScene(this);
        base.LoadContent();
    }
}