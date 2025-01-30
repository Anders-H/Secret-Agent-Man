using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGame;
using RetroGame.Input;
using RetroGame.Scene;
using RetroGame.Text;

namespace SecretAgentMan.Scenes;

public class IngameScene : Scene
{
    private readonly TextBlock _textBlock;
    private bool _askQuitMode;
    private KeyboardStateChecker Keyboard { get; }
    private int _currentRoomIndex;
    private readonly Player _player;

    public IngameScene(RetroGame.RetroGame parent) : base(parent)
    {
        Keyboard = new KeyboardStateChecker();
        _textBlock = new TextBlock(CharacterSet.Uppercase);
        _askQuitMode = false;
        _currentRoomIndex = 0;
        _player = new Player();
        AddToAutoUpdate(Keyboard);
    }

    public override void Update(GameTime gameTime, ulong ticks)
    {
        if (_askQuitMode)
        {
            if (Keyboard.IsKeyPressed(Keys.F3))
            {
                Parent.CurrentScene = new StartScene(Parent);
                return;
            }

            if (Keyboard.IsKeyPressed(Keys.F7))
            {
                _askQuitMode = false;
            }
        }
        else
        {
            if (Keyboard.IsKeyPressed(Keys.Escape))
                _askQuitMode = true;

            var changeAnimationCells = false;
            var isMoving = false;

            if (Keyboard.IsKeyDown(Keys.Right))
            {
                if (!_player.FaceRight)
                {
                    _player.FaceRight = true;
                    changeAnimationCells = true;
                }

                isMoving = true;
                _player.X += 2;
            }
            else if (Keyboard.IsKeyDown(Keys.Left))
            {
                if (_player.FaceRight)
                {
                    _player.FaceRight = false;
                    changeAnimationCells = true;
                }

                isMoving = true;
                _player.X -= 2;
            }

            if (changeAnimationCells)
                _player.ChangeAnimationCells();

            if (isMoving)
                _player.Tick(ticks);
        }

        base.Update(gameTime, ticks);
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        if (_askQuitMode)
        {
            const string quitText = "press f3 to quit, press f7 to continue.";
            var quitX = 320 - ((quitText.Length * 8) / 2);
            _textBlock.DirectDraw(spriteBatch, quitX, 100, quitText, ColorPalette.White);
        }
        else
        {
            _player.Draw(spriteBatch, Game1.CharactersTexture, _player.CellIndex, Color.White);
        }
        
        base.Draw(gameTime, ticks, spriteBatch);
    }
}