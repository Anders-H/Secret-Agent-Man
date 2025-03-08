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
    private int _killedSpyCount;
    private bool _gameCompleted;
    private ulong _gameCompletedAt;
    public const int SpriteUpperLimit = 100;

    public IngameScene(RetroGame.RetroGame parent) : base(parent)
    {
        _gameCompleted = false;
        _gameCompletedAt = 0;
        _killedSpyCount = 0;
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
        UpdateRoomNameAndCheckClear();
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

    private void UpdateRoomNameAndCheckClear()
    {
        _currentRoomName = _roomList[_currentRoomIndex].DistrictName;

        if (_roomList[_currentRoomIndex].IsClear())
            _messageSystem.AddMessage("this area is clear.");
    }

    public override void Update(GameTime gameTime, ulong ticks)
    {
        if (_player.AliveStatus == Character.StatusAlive)
        {
            if (_askQuitMode)
            {
                if (Keyboard.IsKeyPressed(Keys.F3))
                {
                    Parent.CurrentScene = new StartScene(Parent, Game1.LastScore, Game1.TodaysBestScore);
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
                    UpdateRoomNameAndCheckClear();
                }
                else if (previousRoom)
                {
                    _currentRoomIndex--;
                    _fireList.Clear();
                    UpdateRoomNameAndCheckClear();
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
                        _fireList.Remove(fire);

                        if (npc.Status == Npc.StatusInnocent || npc.Status == Npc.StatusSpyUndetected)
                        {
                            if (npc.AliveStatus == Character.StatusAlive)
                                npc.Die(ticks);
                        }
                        else if (npc.Status == Npc.StatusSpyDetected && npc.AliveStatus == Character.StatusAlive)
                        {
                            npc.Die(ticks);
                            _killedSpyCount++;
                            var scoreAdded = _killedSpyCount * 5;
                            Score += scoreAdded;

                            if (_killedSpyCount >= _roomList.SpyCount)
                            {
                                _messageSystem.AddMessage($"all spies eliminated! {scoreAdded} points! well done!");
                                _fireList.Clear();
                                _enemyFireList.Clear();
                                _gameCompleted = true;
                                _gameCompletedAt = ticks;
                            }
                            else
                            {
                                switch (_killedSpyCount)
                                {
                                    case 1:
                                        _messageSystem.AddMessage($"first spy eliminated! {scoreAdded} points!");
                                        break;
                                    case 2:
                                        _messageSystem.AddMessage($"second spy eliminated! {scoreAdded} points!");
                                        break;
                                    case 3:
                                        _messageSystem.AddMessage($"third spy eliminated! {scoreAdded} points!");
                                        break;
                                    case 4:
                                        _messageSystem.AddMessage($"fourth spy eliminated! {scoreAdded} points!");
                                        break;
                                    case 5:
                                        _messageSystem.AddMessage($"fifth spy eliminated! {scoreAdded} points!");
                                        break;
                                    default:
                                        _messageSystem.AddMessage($"spy number {_killedSpyCount} eliminated! {scoreAdded} points!");
                                        break;

                                }
                            }
                        }
                        break;
                    }
                }
            }

            if (_gameCompleted && ticks > _gameCompletedAt + 500)
            {
                Game1.LastScore = Score;

                if (Game1.TodaysBestScore < Game1.LastScore)
                    Game1.TodaysBestScore = Game1.LastScore;

                Parent.CurrentScene = new GameOverScene(Parent, false, true);
                return;
            }

            if (!_gameCompleted)
            {
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

                foreach (var fire in _enemyFireList)
                {
                    if (_player.Hit(fire))
                    {
                        _player.Die(ticks);
                        break;
                    }
                }
            }
        }

        if (_player.AliveStatus == Character.StatusDying)
        {
            _player.Tick(ticks);
        }

        if (_player.AliveStatus == Character.StatusDead)
        {
            Game1.LastScore = Score;

            if (Game1.TodaysBestScore < Game1.LastScore)
                Game1.TodaysBestScore = Game1.LastScore;

            Parent.CurrentScene = new GameOverScene(Parent, true, false);
        }

        _roomList[_currentRoomIndex].ActDecorations(ticks);
        base.Update(gameTime, ticks);
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        Game1.Decoration.Draw(spriteBatch, _currentRoomIndex);
        Game1.BackgroundTempTexture?.Draw(spriteBatch, 0, 0, 0, ColorPalette.White);
        Game1.WaterTexture?.Draw(spriteBatch, _waterFrameIndex, 0, 91, ColorPalette.White);

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

            _roomList[_currentRoomIndex].DrawAirPlanes(spriteBatch);
        }

        _textBlock.DirectDraw(spriteBatch, 0, 0, _currentRoomName, Color.White);
        _textBlock.DirectDraw(spriteBatch, 480, 0, _scoreString, Color.White);
        _messageSystem.Draw(spriteBatch);

        if (_gameCompleted)
            _textBlock.DirectDraw(spriteBatch, GameOverScene.GameClearX, 150, GameOverScene.GameClearText, ColorPalette.Green);

        base.Draw(gameTime, ticks, spriteBatch);
    }
}