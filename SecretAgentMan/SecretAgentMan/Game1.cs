using RetroGame;
using SecretAgentMan.Scenes;

namespace SecretAgentMan;

public class Game1 : RetroGame.RetroGame
{
#if DEBUG
    private const RetroDisplayMode DisplayMode = RetroDisplayMode.Windowed;
#else
    private const RetroDisplayMode DisplayMode = RetroDisplayMode.Fullscreen;
#endif

    public Game1() : base(640, 360, DisplayMode)
    {
    }

    protected override void LoadContent()
    {
        CurrentScene = new StartScene(this);
        base.LoadContent();
    }
}