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

    public Game1() : base(640, 360, DisplayMode)
    {
    }

    protected override void LoadContent()
    {
        BackColor = Color.Black;
        CurrentScene = new StartScene(this);
        CharactersTexture = new CollisionTexture(GraphicsDevice, 25, 25, 32);
        CharactersTexture.SetData(Content.Load<Texture2D>("player25x25"));
        base.LoadContent();
    }
}