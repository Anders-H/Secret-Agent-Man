using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using RetroGame.Scene;
using RetroGame.Text;

namespace SecretAgentMan.Scenes;

public class GameOverInnocentDeathScene : Scene
{
    private const string MayorTalk = "another innocent civilian killed. this ends now!";
    private readonly int _mayorTalkX;
    private int _mayorTalkCharacterCount = 0;
    private const string GameOverText = "game over";
    private const int GameOverX = 284;
    private readonly TextBlock _textBlock;
    private readonly string _lastScoreString;
    private readonly string _todaysBestScoreString;
    private int _currentMayorCell;
    private bool _isAngry;

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

        if (ticks == 150)
            _isAngry = true;

        if (ticks > 300)
        {
            if (Game1.HighScore.Qualify(Game1.LastScore))
                //Parent.CurrentScene = new HighScoreScene(Parent, Game1.LastScore);
                Parent.CurrentScene = new StartScene(Parent, Game1.LastScore, Game1.TodaysBestScore);
            else
                Parent.CurrentScene = new StartScene(Parent, Game1.LastScore, Game1.TodaysBestScore);
        }
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        MayorResources.MayorTexture?.Draw(spriteBatch, _currentMayorCell, 295, 90, ColorPalette.White);
        _textBlock.DirectDraw(spriteBatch, _mayorTalkX, 150, MayorTalk[.._mayorTalkCharacterCount], ColorPalette.White);
        _textBlock.DirectDraw(spriteBatch, GameOverX, 200, GameOverText, ColorPalette.Red);
        _textBlock.DirectDraw(spriteBatch, 0, 344, _todaysBestScoreString, ColorPalette.LightGrey);
        _textBlock.DirectDraw(spriteBatch, 0, 336, _lastScoreString, ColorPalette.LightGrey);
    }
}