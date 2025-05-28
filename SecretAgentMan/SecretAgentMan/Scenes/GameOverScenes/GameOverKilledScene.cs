using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using RetroGame;
using RetroGame.Scene;
using RetroGame.Text;
using SecretAgentMan.OtherResources;
using SecretAgentMan.Scenes.IntroductionScenes;

namespace SecretAgentMan.Scenes.GameOverScenes;

public class GameOverKilledScene : Scene
{
    private readonly TextBlock _textBlock;
    private readonly string _lastScoreString;
    private readonly string _todaysBestScoreString;
    private int _cellIndex;
    private int _wipe;

    public GameOverKilledScene(RetroGame.RetroGame parent) : base(parent)
    {
        _lastScoreString = $"last score: {Game1.LastScore}";
        _todaysBestScoreString = $"best today: {Game1.TodaysBestScore}";
        _textBlock = new TextBlock(CharacterSet.Uppercase);

        if (MediaPlayer.State == MediaState.Playing)
            MediaPlayer.Stop();

        MediaPlayer.Play(Songs.GameOverSong);
    }

    public override void Update(GameTime gameTime, ulong ticks)
    {
        switch (_cellIndex)
        {
            case 0:
                _wipe += 2;

                if (_wipe >= 360)
                {
                    _wipe = 241;
                    _cellIndex++;
                }
                break;
            case 1:
                _wipe--;

                if (_wipe <= 0)
                {
                    _wipe = 0;
                    _cellIndex++;
                }
                break;
            case 2:
                if (ticks > 600)
                {
                    if (Game1.HighScore.Qualify(Game1.LastScore))
                        Parent.CurrentScene = new HighScoreScene(Parent, Game1.LastScore, GameOverReason.PlayerDied);
                    else
                        Parent.CurrentScene = new StartScene(Parent, Game1.LastScore, Game1.TodaysBestScore);
                }
                break;
        }
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        switch (_cellIndex)
        {
            case 0:
                GameOverFiredScene.GameOverGraphics2!.DrawPart(spriteBatch, 0, 360 - _wipe, 640, 360, 0, 360 - _wipe);
                break;
            case 1:
                GameOverFiredScene.GameOverGraphics3!.Draw(spriteBatch, 0, 0, _wipe);
                GameOverFiredScene.GameOverGraphics2!.Draw(spriteBatch, 0, 0, 0);
                break;
            case 2:
                GameOverFiredScene.GameOverGraphics3!.Draw(spriteBatch, 0, 0, 0);
                GameOverFiredScene.GameOverGraphics2!.Draw(spriteBatch, 0, 0, 0);
                break;
        }

        _textBlock.DirectDraw(spriteBatch, 0, 344, _todaysBestScoreString, ColorPalette.LightGrey);
        _textBlock.DirectDraw(spriteBatch, 0, 336, _lastScoreString, ColorPalette.LightGrey);
    }
}