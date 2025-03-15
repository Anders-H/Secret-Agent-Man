using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGame;
using RetroGame.Input;
using RetroGame.Scene;

namespace SecretAgentMan.Scenes;

public class IntroScene : Scene
{
    private int _cellIndex;
    private int _wipe;
    private KeyboardStateChecker Keyboard { get; }

    public IntroScene(RetroGame.RetroGame parent) : base(parent)
    {
        Keyboard = new KeyboardStateChecker();
        AddToAutoUpdate(Keyboard);
    }

    public override void Update(GameTime gameTime, ulong ticks)
    {
        switch (_cellIndex)
        {
            case 0:
                _wipe += 4;

                if (_wipe >= 650)
                {
                    _wipe = 290;
                    _cellIndex++;
                }
                
                break;
            case 1:
                _wipe += 4;

                if (_wipe >= 650)
                {
                    _wipe = 0;
                    _cellIndex++;
                }

                break;
            case 2:
                _wipe++;

                if (_wipe >= 380)
                    _cellIndex++;

                break;
        }

        if (Keyboard.IsKeyPressed(Keys.Escape))
            Exit();
        else if (Keyboard.IsFirePressed())
            Parent.CurrentScene = new StartScene(Parent, 0, 0);

        base.Update(gameTime, ticks);
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        switch (_cellIndex)
        {
            case 0:
                Game1.IntroGraphics1!.DrawPart(spriteBatch, 640 - _wipe, 0, _wipe, 380, 640 - _wipe, 0);
                break;
            case 1:
                Game1.IntroGraphics1!.Draw(spriteBatch, 0, 0, 0, ColorPalette.White);
                Game1.IntroGraphics2!.DrawPart(spriteBatch, 0, 0, _wipe, 380, 0, 0);
                break;
            case 2:
                Game1.IntroGraphics1!.Draw(spriteBatch, 0, 0, 0, ColorPalette.White);
                Game1.IntroGraphics2!.Draw(spriteBatch, 0, 0, 0, ColorPalette.White);
                Game1.IntroGraphics3!.DrawPart(spriteBatch, 0, 0, 480, _wipe, 0, 0);
                break;
            default:
                Game1.IntroGraphics3!.Draw(spriteBatch, 0, 0, 0, ColorPalette.White);
                break;
        }
    }
}