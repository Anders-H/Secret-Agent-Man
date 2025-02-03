using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RetroGame;
using RetroGame.Input;
using RetroGame.Scene;
using RetroGame.Text;
using SecretAgentMan.Scenes.Rooms;

namespace SecretAgentMan.Scenes;

public class IngameScene : Scene
{
    private readonly TextBlock _textBlock;
    private bool _askQuitMode;
    private KeyboardStateChecker Keyboard { get; }
    private int _currentRoomIndex;
    private readonly Player _player;
    // ReSharper disable once CollectionNeverUpdated.Local
    private readonly RoomList _roomList;
    private string _currentRoomName;

    public IngameScene(RetroGame.RetroGame parent) : base(parent)
    {
        Keyboard = new KeyboardStateChecker();
        _textBlock = new TextBlock(CharacterSet.Uppercase);
        _askQuitMode = false;
        _currentRoomIndex = 0;
        _player = new Player();
        _roomList = [];
        UpdateRoomName();
        AddToAutoUpdate(Keyboard);
    }

    private void UpdateRoomName()
    {
        _currentRoomName = $"{_roomList[_currentRoomIndex].DistrictName}, new york";
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

            _player.PlayerControl(ticks, Keyboard, _currentRoomIndex, out var nextRoom, out var previousRoom);

            if (nextRoom)
            {
                _currentRoomIndex++;
                UpdateRoomName();
            }
            else if (previousRoom)
            {
                _currentRoomIndex--;
                UpdateRoomName();
            }

            _roomList[_currentRoomIndex].Act(ticks);
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
            _roomList[_currentRoomIndex].Draw(spriteBatch, _textBlock);
        }

        _textBlock.DirectDraw(spriteBatch, 0, 0, _currentRoomName, Color.White);
        base.Draw(gameTime, ticks, spriteBatch);
    }
}