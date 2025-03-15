using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using SecretAgentMan.Scenes.Rooms;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace SecretAgentMan.Scenes;

public class IngameScene : RetroGame.Scene.IngameScene
{
    private int _innocentKill;
    private ulong _lastInnocentKillAt;
    private int _messageDebug;
    private readonly MessageSystem _messageSystem = new();
    private bool _askQuitMode;
    private int _currentRoomIndex;
    private readonly Player _player;
    private readonly RoomList _roomList;
    private string _currentRoomName = "";
    private readonly IngameFire _fire = new();
    private int _waterFrameIndex;
    private int _killedSpyCount;
    private bool _gameCompleted;
    private ulong _gameCompletedAt;
    public const int SpriteUpperLimit = 100;

    public IngameScene(RetroGame.RetroGame parent) : base(parent)
    {
        _player = new Player(_fire.PlayerFire);
        _roomList = new RoomList(_player, _fire.EnemyFire);
        UpdateRoomNameAndCheckClear();
    }

    private void UpdateRoomNameAndCheckClear()
    {
        _currentRoomName = _roomList.GetDistrictName(_currentRoomIndex);

        if (_roomList.RoomIsClear(_currentRoomIndex))
            _messageSystem.AddMessage("this area is clear.", false);
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
                    _fire.Clear();
                    UpdateRoomNameAndCheckClear();
                }
                else if (previousRoom)
                {
                    _currentRoomIndex--;
                    _fire.Clear();
                    UpdateRoomNameAndCheckClear();
                }

                _roomList.Act(_currentRoomIndex, ticks);

                if (Keyboard.IsKeyDown(Keys.RightShift) && Keyboard.IsKeyPressed(Keys.F10))
                {
                    _messageDebug++;
                    _messageSystem.AddMessage($"you have added text {_messageDebug} to the {(Game1.Random.Next(2) == 0 ? "message " : "")}system!{(Game1.Random.Next(2) == 0 ? " thank you!" : "")}", Game1.Random.Next(0, 2) == 1);
                }

                _messageSystem.Act(ticks);
            }

            _fire.Act(ticks);

            foreach (var npc in _roomList.GetNpcs(_currentRoomIndex))
            {
                foreach (var fire in _fire.PlayerFire)
                {
                    if (npc.Hit(fire))
                    {
                        _fire.PlayerFire.Remove(fire);

                        if (npc.PlayerMayNotKill())
                        {
                            if (npc.AliveStatus == Character.StatusAlive)
                            {
                                npc.Die(ticks);
                                _innocentKill++;

                                switch (_innocentKill)
                                {
                                    case 1:
                                        _messageSystem.AddMessage("you have killed an innocent man!", true);
                                        Score -= 10;
                                        break;
                                    case 2:
                                        _messageSystem.AddMessage("you cannot just go around an shoot people!", true);
                                        Score -= 50;
                                        break;
                                    case 3:
                                        _lastInnocentKillAt = ticks;
                                        break;
                                }
                            }
                        }
                        else if (npc.Status == Npc.StatusSpyDetected && npc.AliveStatus == Character.StatusAlive)
                        {
                            npc.Die(ticks);
                            _killedSpyCount++;
                            var scoreAdded = _killedSpyCount * 5;
                            Score += scoreAdded;

                            if (_killedSpyCount >= _roomList.SpyCount)
                            {
                                _messageSystem.AddMessage($"all spies eliminated! {scoreAdded} points! well done!", false);
                                _fire.Clear();
                                _gameCompleted = true;
                                _gameCompletedAt = ticks;
                            }
                            else
                            {
                                MayorResources.SaySpyKilled(_killedSpyCount, scoreAdded, _messageSystem);
                            }
                        }
                        break;
                    }
                }
            }

            if (_innocentKill >= 3 && ticks > _lastInnocentKillAt + 100)
            {
                Game1.LastScore = Score;

                if (Game1.TodaysBestScore < Game1.LastScore)
                    Game1.TodaysBestScore = Game1.LastScore;

                Parent.CurrentScene = new GameOverInnocentDeathScene(Parent);
                return;
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
                _fire.RemoveOneDeadFire();
                _roomList.RemoveOneDeadNpc(_currentRoomIndex);
                _player.DieIfHit(_fire.EnemyFire, ticks);
            }
        }

        if (_player.AliveStatus == Character.StatusDying)
            _player.Tick(ticks);

        if (_player.AliveStatus == Character.StatusDead)
        {
            Game1.LastScore = Score;

            if (Game1.TodaysBestScore < Game1.LastScore)
                Game1.TodaysBestScore = Game1.LastScore;

            Parent.CurrentScene = new GameOverScene(Parent, true, false);
        }

        _roomList.AnimateDecorations(_currentRoomIndex, ticks);
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
            Text.DirectDraw(spriteBatch, quitX, 150, quitText, ColorPalette.White);
        }
        else
        {
            _roomList.DrawBackground(spriteBatch, _currentRoomIndex, Text, _player);
            _fire.Draw(spriteBatch);
            _roomList.DrawDecorations(spriteBatch, _currentRoomIndex);
        }

        Text.DirectDraw(spriteBatch, 0, 0, _currentRoomName, Color.White);
        DrawScore(spriteBatch, 480, 0, ColorPalette.White);
        _messageSystem.Draw(spriteBatch);

        if (_gameCompleted)
            Text.DirectDraw(spriteBatch, GameOverScene.GameClearX, 150, GameOverScene.GameClearText, ColorPalette.Green);

        base.Draw(gameTime, ticks, spriteBatch);
    }
}