using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using RetroGame;
using RetroGame.Input;
using RetroGame.Scene;
using RetroGame.Text;
using SecretAgentMan.OtherResources;

namespace SecretAgentMan.Scenes.IntroductionScenes;

public class StartScene : Scene
{
    private int _frameVisiblePart;
    private uint _partTick;
    private const string CreditsText = "programming: anders hesselbom    sound and graphics: mats j. larsson    copyright 1989 havet software company";
    private const string TodaysBestPlayersHeader = "the best secret agents today are";
    private int _creditsX;
    private readonly TextBlock _textBlock;
    private KeyboardStateChecker Keyboard { get; }
    private readonly string _lastScoreString;
    private readonly string _todaysBestScoreString;
    private StartSceneState _state;
    private int _gunX;
    private int _logoY;
    private readonly List<int> _logoImageList;
    private int _logoImageListIndex;
    private TextBlockStaticVerticalCenter? _typeWriter;

    public StartScene(RetroGame.RetroGame parent, int lastScore, int todaysBest) : base(parent)
    {
        _gunX = 610;
        _logoY = -60;

        _logoImageList =
        [
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0,
            14, 14, 13, 13, 12, 12, 11, 11, 10, 10, 9, 9, 8, 8, 7, 7, 6, 6, 5, 5, 4, 4, 3, 3, 2, 2, 1, 1, 0,
            14, 0, 13, 0, 12, 0, 11, 0, 10, 0, 9, 0, 8, 0, 7, 0, 6, 0, 5, 0, 4, 0, 3, 0, 2, 0, 1, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        ];

        _lastScoreString = $"last score: {lastScore}";
        _todaysBestScoreString = $"best today: {todaysBest}";
        _creditsX = 700;
        Keyboard = new KeyboardStateChecker();
        _textBlock = new TextBlock(CharacterSet.Uppercase);
        _state = StartSceneState.Logo;
        AddToAutoUpdate(Keyboard);
        Game1.HighScore.ResetVisuals(Game1.HighScoreViewY);

        if (!Game1.LoaderSongIsPlaying)
        {
            Game1.LoaderSongIsPlaying = true;
            MediaPlayer.Play(Songs.LoaderSong!);
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


        _frameVisiblePart += 4;
        _partTick++;

        switch (_state)
        {
            case StartSceneState.Logo when _partTick % 750 == 0:
                _state = StartSceneState.HighScore;
                break;
            case StartSceneState.HighScore when _partTick % 750 == 0:
                _state = StartSceneState.Instructions;
                CreateInstructions();
                break;
            case StartSceneState.Instructions when _partTick % 3000 == 0:
                _partTick = 0;
                _state = StartSceneState.Logo;
                break;
        }

        switch (_state)
        {
            case StartSceneState.Logo:
            case StartSceneState.HighScore:
                _gunX -= 2;

                if (_gunX < 0)
                    _gunX = 0;

                if (ticks % 2 == 0)
                {
                    _logoY += 2;

                    if (_logoY > 30)
                        _logoY = 30;
                }

                break;
            case StartSceneState.Instructions:
                
                if (ticks % 3 == 0)
                {
                    _logoImageListIndex++;

                    if (_logoImageListIndex >= _logoImageList.Count)
                        _logoImageListIndex = 0;
                }

                _typeWriter!.Act(_partTick);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        base.Update(gameTime, ticks);
    }

    private void CreateInstructions()
    {
        _typeWriter = new TextBlockStaticVerticalCenter(640, 110,
            "AS A SECRET AGENT, YOU ARE CALLED UPON",
            "BY NEW YORK CITY MAYOR KOCH TO CURB THE",
            "ESCALATING CRIME RATE.",
            "",
            "CRIMINAL ELEMENTS ARE OPENLY ROAMING",
            "AMONG THE OTHER CITIZENS, AND IT IS OF",
            "UTMOST IMPORTANCE THAT YOU ELIMINATE",
            "ALL THE CRIMINALS IN THE AREA, WITHOUT",
            "HARMING ANY OF THE PEACEFUL CIVILIAN",
            "POPULATION.",
            "",
            "YOU MAY NOT KILL ANY CIVILIANS, YOU",
            "NEED TO KILL ALL CRIMINAL ELEMENTS.",
            "",
            "TO REACH THE NEXT LEVEL, ALL CRIMINALS",
            "MUST BE ELIMINATED.",
            "",
            "THERE ARE BONUS ITEMS PLACED OUT FOR",
            "YOU, AND ADDITIONAL TASKS MAY BE GIVEN",
            "TO YOU DURING GAMEPLAY - PAY CAREFUL",
            "ATTENTION TO WHAT MAYOR KOCH HAS TO SAY.",
            "GOOD LUCK, AND BE CAREFUL OUT THERE!");
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        switch (_state)
        {
            case StartSceneState.Logo:
                Game1.StartScreenGun!.Draw(spriteBatch, 0, _gunX, 0);
                break;
            case StartSceneState.HighScore:
                var x = TodaysBestPlayersHeader.Length * 8;
                x = 320 - x / 2;
                _textBlock.DirectDraw(spriteBatch, x, 134, TodaysBestPlayersHeader, ColorPalette.Yellow);
                Game1.HighScore.Draw(spriteBatch, ticks);
                break;
            case StartSceneState.Instructions:
                _typeWriter!.Draw(spriteBatch, ticks);
                break;
            default:
                throw new ArgumentOutOfRangeException($"StartScene.Draw: {_state}");
        }

        Game1.StartScreenLogo!.Draw(spriteBatch, _logoImageList[_logoImageListIndex], 99, _logoY);

        if (_frameVisiblePart < 360)
            Game1.StartScreenFrame!.DrawPart(spriteBatch, 0, 0, 640, _frameVisiblePart, 0, 0);
        else
            Game1.StartScreenFrame!.Draw(spriteBatch, 0, 0, 0);

        _textBlock.DirectDraw(spriteBatch, 11, 342, _todaysBestScoreString, ColorPalette.LightGrey);
        _textBlock.DirectDraw(spriteBatch, 11, 334, _lastScoreString, ColorPalette.LightGrey);
        _textBlock.DirectDraw(spriteBatch, _creditsX, 352, CreditsText, ColorPalette.Green);
        base.Draw(gameTime, ticks, spriteBatch);
    }
}