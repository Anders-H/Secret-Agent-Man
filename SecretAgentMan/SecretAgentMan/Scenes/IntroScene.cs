using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGame.Input;
using RetroGame.Scene;

namespace SecretAgentMan.Scenes;

public class IntroScene : Scene
{
    private int _y = 360;
    private KeyboardStateChecker Keyboard { get; }

    public IntroScene(RetroGame.RetroGame parent) : base(parent)
    {
        Keyboard = new KeyboardStateChecker();
        AddToAutoUpdate(Keyboard);
    }

    public override void Update(GameTime gameTime, ulong ticks)
    {
        if (_y > 200)
            _y -= 4;
        else if (_y > 100)
            _y -= 3;
        else if (_y > 50)
            _y -= 2;
        else if (_y > 0)
            _y -= 1;

        if (Keyboard.IsKeyPressed(Keys.Escape))
            Exit();
        else if (Keyboard.IsFirePressed())
            Parent.CurrentScene = new StartScene(Parent, 0, 0);

        base.Update(gameTime, ticks);
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        Game1.IntroGraphics!.Draw(spriteBatch, 0, 0, _y);
    }
}