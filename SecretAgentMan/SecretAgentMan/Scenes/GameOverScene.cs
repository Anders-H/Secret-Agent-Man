using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using RetroGame.Scene;
using RetroGame.Text;

namespace SecretAgentMan.Scenes;

public class GameOverScene : Scene
{
    private readonly bool _gameOverAnimation;
    private readonly bool _gameClearAnimation;
    private const string GameOverText = "game over";
    private const int GameOverX = 284;
    private readonly TextBlock _textBlock;
    private readonly string _lastScoreString;
    private readonly string _todaysBestScoreString;
    public const string GameClearText = "game completed";
    public const int GameClearX = 264;

    public GameOverScene(RetroGame.RetroGame parent, bool gameOverAnimation, bool gameClearAnimation) : base(parent)
    {
        _gameOverAnimation = gameOverAnimation;
        _gameClearAnimation = gameClearAnimation;
        _lastScoreString = $"last score: {Game1.LastScore}";
        _todaysBestScoreString = $"best today: {Game1.TodaysBestScore}";
        _textBlock = new TextBlock(CharacterSet.Uppercase);
    }

    public override void Update(GameTime gameTime, ulong ticks)
    {
        if (ticks > 120)
        {
            if (Game1.HighScore.Qualify(Game1.LastScore))
                Parent.CurrentScene = new HighScoreScene(Parent, Game1.LastScore);
            else
                Parent.CurrentScene = new StartScene(Parent, Game1.LastScore, Game1.TodaysBestScore);
        }
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        if (_gameOverAnimation)
        {
            _textBlock.DirectDraw(spriteBatch, GameOverX, 150, GameOverText, ColorPalette.Red);
        }
        else if (_gameClearAnimation)
        {
            _textBlock.DirectDraw(spriteBatch, GameClearX, 150, GameClearText, ColorPalette.Green);
        }

        _textBlock.DirectDraw(spriteBatch, 0, 344, _todaysBestScoreString, ColorPalette.LightGrey);
        _textBlock.DirectDraw(spriteBatch, 0, 336, _lastScoreString, ColorPalette.LightGrey);
    }
}