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
    public static RetroTexture CharactersTexture { get; set; }
    public static RetroTexture BackgroundTempTexture { get; set; }
    public static RetroTextureVertical WaterTexture { get; set; }
    public static Random Random;
    public static bool Cheat = false;

    static Game1()
    {
        Random = new Random();
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

        CurrentScene = new StartScene(this);
        base.LoadContent();
    }
}