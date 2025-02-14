using System.Collections.Generic;
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
    private int _messageDebug;
    private string _scoreString;
    private int _score;
    private readonly MessageSystem _messageSystem;
    private readonly TextBlock _textBlock;
    private bool _askQuitMode;
    private KeyboardStateChecker Keyboard { get; }
    private int _currentRoomIndex;
    private readonly Player _player;
    // ReSharper disable once CollectionNeverUpdated.Local
    private readonly RoomList _roomList;
    private string _currentRoomName;
    private readonly List<Fire> _fireList;
    private readonly List<Fire> _enemyFireList;
    private int _waterFrameIndex;
    public const int SpriteUpperLimit = 100;

    public IngameScene(RetroGame.RetroGame parent) : base(parent)
    {
        _scoreString = "score: 0";
        _waterFrameIndex = 0;
        _messageSystem = new MessageSystem();
        _messageDebug = 0;
        Keyboard = new KeyboardStateChecker();
        _textBlock = new TextBlock(CharacterSet.Uppercase);
        _askQuitMode = false;
        _currentRoomIndex = 0;
        _fireList = [];
        _enemyFireList = [];
        _player = new Player(_fireList);
        _roomList = new RoomList(_player, _enemyFireList);
        _currentRoomName = "";
        UpdateRoomName();
        AddToAutoUpdate(Keyboard);
    }

    public int Score
    {
        get => _score;
        set
        {
            if (value != _score)
            {
                _score = value;
                _scoreString = $"score: {_score}";
            }
        }
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

            if (Keyboard.IsKeyDown(Keys.RightShift) && Keyboard.IsKeyPressed(Keys.F9))
                Game1.Cheat = !Game1.Cheat;

            if (ticks % 7 == 0)
            {
                _waterFrameIndex++;

                if (_waterFrameIndex > 17)
                    _waterFrameIndex = 0;
            }

            _player.PlayerControl(ticks, Keyboard, _currentRoomIndex, out var nextRoom, out var previousRoom);

            if (nextRoom)
            {
                _currentRoomIndex++;
                _fireList.Clear();
                UpdateRoomName();
            }
            else if (previousRoom)
            {
                _currentRoomIndex--;
                _fireList.Clear();
                UpdateRoomName();
            }

            _roomList[_currentRoomIndex].Act(ticks);

            if (Keyboard.IsKeyDown(Keys.RightShift) && Keyboard.IsKeyPressed(Keys.F10))
            {
                _messageDebug++;
                _messageSystem.AddMessage($"you have added text {_messageDebug} to the {(Game1.Random.Next(2) == 0 ? "message " : "")}system!{(Game1.Random.Next(2) == 0 ? " thank you!" : "")}");
            }

            _messageSystem.Act(ticks);
        }

        foreach (var fire in _fireList)
            fire.Act(ticks);

        foreach (var npc in _roomList[_currentRoomIndex].Npcs)
        {
            foreach (var fire in _fireList)
            {
                if (npc.Hit(fire))
                {
                    npc.Die(ticks);
                    _fireList.Remove(fire);
                    break;
                }
            }
        }

        foreach (var fire in _fireList)
        {
            if (!fire.IsDead)
                continue;

            _fireList.Remove(fire);
            return;
        }

        foreach (var fire in _enemyFireList)
            fire.Act(ticks);

        foreach (var fire in _enemyFireList)
        {
            if (!fire.IsDead)
                continue;

            _enemyFireList.Remove(fire);
            return;
        }

        foreach (var npc in _roomList[_currentRoomIndex].Npcs)
        {
            if (npc.AliveStatus == Character.StatusDead)
            {
                _roomList[_currentRoomIndex].Npcs.Remove(npc);
                break;
            }
        }

        base.Update(gameTime, ticks);
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        Game1.BackgroundTempTexture.Draw(spriteBatch, 0, 0, 0, ColorPalette.White);
        Game1.WaterTexture.Draw(spriteBatch, _waterFrameIndex, 0, 91, ColorPalette.White);

        if (_askQuitMode)
        {
            const string quitText = "press f3 to quit, press f7 to continue.";
            var quitX = 320 - ((quitText.Length * 8) / 2);
            _textBlock.DirectDraw(spriteBatch, quitX, 150, quitText, ColorPalette.White);
        }
        else
        {
            _roomList[_currentRoomIndex].Draw(spriteBatch, _textBlock, _player);

            foreach (var fire in _fireList)
                fire.Draw(spriteBatch);

            foreach (var fire in _enemyFireList)
                fire.Draw(spriteBatch);
        }

        _textBlock.DirectDraw(spriteBatch, 0, 0, _currentRoomName, Color.White);
        _textBlock.DirectDraw(spriteBatch, 480, 0, _scoreString, Color.White);
        _messageSystem.Draw(spriteBatch);
        base.Draw(gameTime, ticks, spriteBatch);
    }
}