using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGame.Input;
using RetroGame.Scene;

namespace SecretAgentMan.Scenes.IntroductionScenes;

public class IntroScene : Scene
{
    private const int CellCountX = 40;
    private const int CellCountY = 9;
    private int _currentCellX = 0;
    private int _currentCellY = 0;
    private int _inCurrentCellY = 0;
    private bool _done = false;
    private KeyboardStateChecker Keyboard { get; }

    public IntroScene(RetroGame.RetroGame parent) : base(parent)
    {
        Keyboard = new KeyboardStateChecker();
        AddToAutoUpdate(Keyboard);
    }

    public override void Update(GameTime gameTime, ulong ticks)
    {
        _inCurrentCellY += 2;

        if (_inCurrentCellY > 16)
        {
            _inCurrentCellY = 0;
            _currentCellX++;

            if (_currentCellX >= CellCountX)
            {
                _currentCellX = 0;
                _currentCellY++;

                if (_currentCellY >= CellCountY)
                {
                    _done = true;
                }
            }
        }

        if (Keyboard.IsKeyPressed(Keys.Escape))
            Exit();
        else if (Keyboard.IsFirePressed())
            Parent.CurrentScene = new StartScene(Parent, 0, 0);

        base.Update(gameTime, ticks);
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        if (_done)
        {
            Game1.IntroGraphics!.Draw(spriteBatch, 0, 0, 0);
        }
        else
        {
            if (_currentCellY > 0)
                Game1.IntroGraphics!.DrawPart(spriteBatch, 0, 0, 640, _currentCellY * 16, 0, 0);

            if (_currentCellX > 0)
                Game1.IntroGraphics!.DrawPart(spriteBatch, 0, _currentCellY * 16, _currentCellX * 16, 16, 0, _currentCellY * 16);

            if (_inCurrentCellY > 0)
                Game1.IntroGraphics!.DrawPart(spriteBatch, _currentCellX * 16, _currentCellY * 16, 16, _inCurrentCellY, _currentCellX * 16, _currentCellY * 16);
        }

        base.Draw(gameTime, ticks, spriteBatch);
    }
}