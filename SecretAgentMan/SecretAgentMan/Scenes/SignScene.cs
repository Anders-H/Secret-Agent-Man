using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using RetroGame.Scene;

namespace SecretAgentMan.Scenes;

public class SignScene : RetroGame.Scene.IngameScene
{
    private readonly string _message;
    private readonly Scene _nextScene;
    private readonly int _x;
    private const int Y = 130;
    private int _currentChar;

    public SignScene(RetroGame.RetroGame parent, string message, Scene nextScene) : base(parent)
    {
        _message = $"    {message}    ";
        _nextScene = nextScene;
        _x = 320 - (_message.Length * 4); // (... * 8) / 2
    }

    public override void Update(GameTime gameTime, ulong ticks)
    {
        if (ticks > 120)
        {
            Parent.CurrentScene = _nextScene;
            _nextScene.BeginScene();
        }

        if (ticks%2 == 0)
            _currentChar++;

        base.Update(gameTime, ticks);
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        if (_currentChar > (uint)(_message.Length))
        {
            Text.DirectDraw(spriteBatch, _x, Y, _message, ColorPalette.Blue);
        }
        else
        {
            var c = _currentChar;
            Text.DirectDraw(spriteBatch, _x, Y, _message.Substring(0, c), ColorPalette.White);
            c--;

            if (c > 0)
                Text.DirectDraw(spriteBatch, _x, Y, _message.Substring(0, c), ColorPalette.Cyan);

            c--;

            if (c > 0)
                Text.DirectDraw(spriteBatch, _x, Y, _message.Substring(0, c), ColorPalette.LightBlue);

            c--;

            if (c > 0)
                Text.DirectDraw(spriteBatch, _x, Y, _message.Substring(0, c), ColorPalette.Blue);
        }

        base.Draw(gameTime, ticks, spriteBatch);
    }
}