using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using RetroGame;
using RetroGame.Input;
using RetroGame.Scene;
using RetroGame.Text;

namespace SecretAgentMan.Scenes.IntroductionScenes;

public class StartScene : Scene
{
    private const string CreditsText = "programming: anders hesselbom    sound and graphics: mats j. larsson    copyright 1989 havet software company";
    private const string TodaysBestPlayersHeader = "the best secret agents today are";
    private int _creditsX;
    private readonly TextBlock _textBlock;
    private KeyboardStateChecker Keyboard { get; }
    private readonly string _lastScoreString;
    private readonly string _todaysBestScoreString;
    private StartSceneState _state;
    private int _frameX;
    private int _gunX;
    private int _logoY;

    public StartScene(RetroGame.RetroGame parent, int lastScore, int todaysBest) : base(parent)
    {
        _frameX = -641;
        _gunX = 610;
        _logoY = -60;
        _lastScoreString = $"last score: {lastScore}";
        _todaysBestScoreString = $"best today: {todaysBest}";
        _creditsX = 700;
        Keyboard = new KeyboardStateChecker();
        _textBlock = new TextBlock(CharacterSet.Uppercase);
        _state = StartSceneState.Logo;
        AddToAutoUpdate(Keyboard);

        if (!Game1.LoaderSongIsPlaying)
        {
            Game1.LoaderSongIsPlaying = true;
            MediaPlayer.Play(Game1.LoaderSong!);
        }
    }

    public override void Update(GameTime gameTime, ulong ticks)
    {
        if (ticks <= 10)
        {
            Keyboard.ClearState();
            return;
        }

        if (Keyboard.IsKeyPressed(Keys.Escape))
        {
            Exit();
            return;
        }

        if ((Keyboard.IsFirePressed() || Keyboard.IsPadButtonPressed(Buttons.Start) ) && ticks > 11)
        {
            Game1.CurrentIngameScene = new IngameScene(Parent);
            Parent.CurrentScene = Game1.CurrentIngameScene;
            return;
        }

        if (ticks % 2 == 0)
        {
            _creditsX--;

            if (_creditsX < -940)
                _creditsX = 640;
        }

        _state = _state switch
        {
            StartSceneState.Logo when ticks % 700 == 0 => StartSceneState.HighScore,
            StartSceneState.HighScore when ticks % 700 == 0 => StartSceneState.Instructions,
            StartSceneState.Instructions when ticks % 1700 == 0 => StartSceneState.Logo,
            _ => _state
        };

        switch (_state)
        {
            case StartSceneState.Logo:
                _frameX += 2;

                if (_frameX > 0)
                    _frameX = 0;

                _gunX--;

                if (_gunX < 0)
                    _gunX = 0;

                if (ticks % 2 == 0)
                {
                    _logoY++;

                    if (_logoY > 50)
                        _logoY = 50;
                }

                break;
            case StartSceneState.HighScore:
                break;
            case StartSceneState.Instructions:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        base.Update(gameTime, ticks);
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        switch (_state)
        {
            case StartSceneState.Logo:
                Game1.StartScreenGun!.Draw(spriteBatch, 0, _gunX, 0);
                Game1.StartScreenFrame!.Draw(spriteBatch, 0, _frameX, 0);
                break;
            case StartSceneState.HighScore:
                Game1.StartScreenFrame!.Draw(spriteBatch, 0, _frameX, 0);
                var x = TodaysBestPlayersHeader.Length * 8;
                x = 320 - x / 2;
                _textBlock.DirectDraw(spriteBatch, x, 32, TodaysBestPlayersHeader, ColorPalette.Yellow);
                Game1.HighScore.Draw(spriteBatch, ticks);
                break;
            case StartSceneState.Instructions:
                _textBlock.DirectDraw(spriteBatch, 0, 32, "här ska vi ha manualen - eller som ingenjörerna säger: manulen", ColorPalette.White);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        _textBlock.DirectDraw(spriteBatch, 0, 344, _todaysBestScoreString, ColorPalette.LightGrey);
        _textBlock.DirectDraw(spriteBatch, 0, 336, _lastScoreString, ColorPalette.LightGrey);
        _textBlock.DirectDraw(spriteBatch, _creditsX, 352, CreditsText, ColorPalette.Green);
        base.Draw(gameTime, ticks, spriteBatch);
    }
}