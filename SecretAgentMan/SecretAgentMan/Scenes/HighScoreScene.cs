using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using RetroGame;
using RetroGame.Audio;
using RetroGame.Scene;
using SecretAgentMan.OtherResources;
using SecretAgentMan.Scenes.GameOverScenes;
using SecretAgentMan.Scenes.IntroductionScenes;

namespace SecretAgentMan.Scenes;

public class HighScoreScene : TextAndKeyboardScene
{
    private readonly int _score;
    private bool _qualify;
    private GameEventPointer _editEnded = new();
    private const string BestPlayer = "you are one of the best players today. enter your name in the highscore list! well done, sir!";
    private int _bestPlayerX;
    private readonly GameOverReason _gameOverReason;
    private int _gameOverY;

    public HighScoreScene(RetroGame.RetroGame parent, int score, GameOverReason gameOverReason) : base(parent)
    {
        _gameOverReason = gameOverReason;
        _bestPlayerX = 650;
        _score = score;
        Game1.HighScore.ResetVisuals(Game1.HighScoreEditY);
        Jukebox.PlayWithLoop(Songs.HiScoreSong);

        if (_gameOverReason == GameOverReason.PlayerFired)
            _gameOverY = 200;
        else if (_gameOverReason == GameOverReason.PlayerDied)
            _gameOverY = 0;
    }

    public override void Update(GameTime gameTime, ulong ticks)
    {
        if (ticks % 2 == 0)
        {
            _bestPlayerX--;

            if (_bestPlayerX < -940)
                _bestPlayerX = 640;
        }

        if (ticks < 2)
        {
            Keyboard.ClearState();
            return;
        }

        if (ticks == 3)
        {
            _qualify = Game1.HighScore.Qualify(_score);

            if (_qualify)
            {
                Game1.HighScore.BeginEdit(_score);
                return;
            }
        }

        if (ticks > 3 && _qualify)
        {
            if (Game1.HighScore.StillEditing)
            {
                Game1.HighScore.Edit(Keyboard);
            }
            else
            {
                _editEnded.Occure(ticks);
                _qualify = false;
            }
        }
        else if (_editEnded.OccuredTicksAgo(ticks, 100) && (Keyboard.IsKeyPressed(Keys.Escape) || Keyboard.IsFirePressed()))
        {
            MediaPlayer.Stop();
            Parent.CurrentScene = new StartScene(Parent, _score, Game1.TodaysBestScore);
        }

        switch (_gameOverReason)
        {
            case GameOverReason.PlayerFired:
            case GameOverReason.PlayerDied:
                _gameOverY--;

                if (_gameOverY < -360)
                    _gameOverY = -360;

                break;
        }

        base.Update(gameTime, ticks);
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        switch (_gameOverReason)
        {
            case GameOverReason.PlayerFired:
                GameOverFiredScene.GameOverGraphics.Texture4!.Draw(spriteBatch, 0, 0, 0);
                GameOverFiredScene.GameOverGraphics.Texture3!.Draw(spriteBatch, 0, 0, _gameOverY);
                break;
            case GameOverReason.PlayerDied:
                GameOverFiredScene.GameOverGraphics.Texture2!.Draw(spriteBatch, 0, 0, 0);
                GameOverFiredScene.GameOverGraphics.Texture3!.Draw(spriteBatch, 0, 0, _gameOverY);
                break;
        }

        TextBlock.DirectDraw(spriteBatch, _bestPlayerX, 70, BestPlayer, ColorPalette.Green);
        Game1.HighScore.Draw(spriteBatch, ticks);
        base.Draw(gameTime, ticks, spriteBatch);
    }
}