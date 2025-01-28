using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RetroGame;
using RetroGame.Input;
using RetroGame.RetroTextures;
using RetroGame.Scene;
using RetroGame.Text;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace SecretAgentMan.Scenes;

public class StartScene : Scene
{
    private int _logoX;
    private int _logoY;
    private double _counter;
    private const string LogoText = "secret agent man";
    private readonly TextBlock _textBlock;
    private KeyboardStateChecker Keyboard { get; }

    public StartScene(RetroGame.RetroGame parent) : base(parent)
    {
        Keyboard = new KeyboardStateChecker();
        _textBlock = new TextBlock(CharacterSet.Uppercase);
        AddToAutoUpdate(Keyboard);
    }

    public override void Update(GameTime gameTime, ulong ticks)
    {
        if (Keyboard.IsKeyPressed(Keys.Escape))
        {
            Exit();
            return;
        }
        if (Keyboard.IsFirePressed())
        {
            Parent.CurrentScene = new IngameScene(Parent);
            return;
        }

        _counter++;
        const int centerX = 255;
        const int centerY = 100;
        _logoX = (int)(centerX + Math.Sin(_counter / 50) * 150.0);
        _logoY = (int)(centerY + Math.Cos(_counter / 30) * 30.0);

        base.Update(gameTime, ticks);
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        _textBlock.DirectDraw(spriteBatch, _logoX, _logoY, LogoText, ColorPalette.White);
        base.Draw(gameTime, ticks, spriteBatch);
    }
}