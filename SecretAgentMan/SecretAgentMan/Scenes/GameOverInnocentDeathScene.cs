using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using RetroGame.Scene;
using RetroGame.Text;
using SecretAgentMan.OtherResources;

namespace SecretAgentMan.Scenes;

public class GameOverInnocentDeathScene : Scene
{
    private const string MayorTalk = "another innocent civilian killed. this ends now!";
    private readonly int _mayorTalkX;
    private int _mayorTalkCharacterCount;
    private readonly TextBlock _textBlock;
    private readonly string _lastScoreString;
    private readonly string _todaysBestScoreString;
    private int _currentMayorCell;
    private bool _isAngry;
    private int _cellIndex;
    private int _wipe;

    public GameOverInnocentDeathScene(RetroGame.RetroGame parent) : base(parent)
    {
        _isAngry = false;
        _currentMayorCell = 0;
        _mayorTalkX = 320 - ((MayorTalk.Length * 8) / 2);
        _lastScoreString = $"last score: {Game1.LastScore}";
        _todaysBestScoreString = $"best today: {Game1.TodaysBestScore}";
        _textBlock = new TextBlock(CharacterSet.Uppercase);
    }

    public override void Update(GameTime gameTime, ulong ticks)
    {
        switch (_cellIndex)
        {
            case 0:
                _wipe += 2;

                if (_wipe >= 650)
                {
                    _wipe = 0;
                    _cellIndex++;
                }

                break;
            case 1:
                _wipe += 2;

                if (_wipe >= 380)
                {
                    _wipe = 380;

                    if (ticks > 550)
                        _cellIndex++;
                }

                break;
            case 2:
                if (_wipe > 220)
                    _wipe -= 2;
                else
                    _wipe--;

                if (_wipe < 200)
                    _wipe = 200;

                break;
        }

        if (_isAngry)
        {
            if (ticks % 5 == 0)
                _currentMayorCell = _currentMayorCell != 2 ? 2 : 3;
        }
        else
        {
            if (ticks % 20 == 0)
                _currentMayorCell = _currentMayorCell != 0 ? 0 : 1;
        }

        if (ticks % 3 == 0 && _mayorTalkCharacterCount < MayorTalk.Length)
            _mayorTalkCharacterCount++;

        if (ticks == 200)
            _isAngry = true;

        if (ticks > 780)
        {
            if (Game1.HighScore.Qualify(Game1.LastScore))
                Parent.CurrentScene = new HighScoreScene(Parent, Game1.LastScore, GameOverReason.PlayerFired);
            else
                Parent.CurrentScene = new StartScene(Parent, Game1.LastScore, Game1.TodaysBestScore);
        }
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        if (_cellIndex == 0)
            Game1.GameOverGraphics1!.DrawPart(spriteBatch, 640 - _wipe, 0, _wipe, 380, 640 - _wipe, 0);

        if (ticks < 280)
            MayorResources.MayorTexture?.Draw(spriteBatch, _currentMayorCell, 295, 145, ColorPalette.White);

        if (ticks < 320)
            _textBlock.DirectDraw(spriteBatch, _mayorTalkX, 200, MayorTalk[.._mayorTalkCharacterCount], ColorPalette.White);
        
        if (_cellIndex == 1)
        {
            Game1.GameOverGraphics4!.DrawPart(spriteBatch, 0, 0, 640, _wipe, 0, 0);
            Game1.GameOverGraphics1!.DrawPart(spriteBatch, 0, 0, 640, 380 - _wipe, 0, 0);
        }
        else if (_cellIndex > 1)
        {
            Game1.GameOverGraphics4!.Draw(spriteBatch, 0, 0, 0);
            Game1.GameOverGraphics3!.Draw(spriteBatch, 0, 0, _wipe);
        }

        _textBlock.DirectDraw(spriteBatch, 0, 344, _todaysBestScoreString, ColorPalette.LightGrey);
        _textBlock.DirectDraw(spriteBatch, 0, 336, _lastScoreString, ColorPalette.LightGrey);
    }
}