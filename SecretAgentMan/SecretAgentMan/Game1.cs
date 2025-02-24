using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using RetroGame.RetroTextures;
using SecretAgentMan.Scenes;

namespace SecretAgentMan;

public class Game1 : RetroGame.RetroGame
{
#if DEBUG
    private const RetroDisplayMode DisplayMode = RetroDisplayMode.Fullscreen;
#else
    private const RetroDisplayMode DisplayMode = RetroDisplayMode.Fullscreen;
#endif
    public static RetroTexture? CharactersTexture { get; set; }
    public static RetroTexture? BackgroundTempTexture { get; set; }
    public static RetroTextureVertical? WaterTexture { get; set; }
    public static RetroTexture? AirplaneRightTexture { get; set; }
    public static RetroTexture? AirplaneLeftTexture { get; set; }
    public static RetroTexture? Mayor { get; set; }
    public static RetroTexture IntroGraphics { get; set; }
    public static Random Random;
    public static bool Cheat = false;
    public static int LastScore;
    public static int TodaysBestScore;

    static Game1()
    {
        Random = new Random();
        LastScore = 0;
        TodaysBestScore = 0;
    }

    public Game1() : base(640, 360, DisplayMode)
    {
    }

    protected override void LoadContent()
    {
        BackColor = Color.Black;

        CharactersTexture = new RetroTexture(GraphicsDevice, 25, 25, 32);
        CharactersTexture.SetData(Content.Load<Texture2D>("player25x25"));

        BackgroundTempTexture = new RetroTexture(GraphicsDevice, 640, 360, 1);
        BackgroundTempTexture.SetData(Content.Load<Texture2D>("skylinetest"));

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

        CurrentScene = new IntroScene(this);
        base.LoadContent();
    }
}