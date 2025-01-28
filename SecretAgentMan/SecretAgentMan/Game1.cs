using Microsoft.Xna.Framework;
using RetroGame;
using RetroGame.RetroTextures;
using SecretAgentMan.Scenes;

namespace SecretAgentMan;

public class Game1 : RetroGame.RetroGame
{
#if DEBUG
    private const RetroDisplayMode DisplayMode = RetroDisplayMode.Windowed;
#else
    private const RetroDisplayMode DisplayMode = RetroDisplayMode.Fullscreen;
#endif
    public static RetroTexture CharactersTexture { get; set; }

    public Game1() : base(640, 360, DisplayMode)
    {
        CharactersTexture = new CollisionTexture(GraphicsDevice, 25, 25, 32);
    }

    protected override void LoadContent()
    {
        BackColor = Color.Black;
        CurrentScene = new StartScene(this);
        base.LoadContent();
    }
}