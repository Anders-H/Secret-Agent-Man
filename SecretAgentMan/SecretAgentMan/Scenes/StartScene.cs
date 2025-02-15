using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RetroGame;
using RetroGame.Input;
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
    private const string CreditsText = "programming: anders hesselbom   sound and graphics: mats larsson   copyright 1989 havet software company";
    private int _creditsX;
    private readonly TextBlock _textBlock;
    private KeyboardStateChecker Keyboard { get; }
    private string _lastScoreString;
    private string _todaysBestScoreString;

    public StartScene(RetroGame.RetroGame parent, int lastScore, int todaysBest) : base(parent)
    {
        _lastScoreString = $"last score: {lastScore}";
        _todaysBestScoreString = $"best today: {todaysBest}";
        _creditsX = 700;
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

        if (ticks%2 == 0)
        {
            _creditsX--;

            if (_creditsX < -940)
                _creditsX = 640;
        }

        base.Update(gameTime, ticks);
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        _textBlock.DirectDraw(spriteBatch, _logoX, _logoY, LogoText, ColorPalette.White);
        _textBlock.DirectDraw(spriteBatch, _creditsX, 352, CreditsText, ColorPalette.Green);
        _textBlock.DirectDraw(spriteBatch, 0, 344, _todaysBestScoreString, ColorPalette.LightGrey);
        _textBlock.DirectDraw(spriteBatch, 0, 336, _lastScoreString, ColorPalette.LightGrey);
        base.Draw(gameTime, ticks, spriteBatch);
    }
}