using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using SecretAgentMan.OtherResources;
using SecretAgentMan.Scenes.GameOverScenes;
using SecretAgentMan.Scenes.IntroductionScenes;
using SecretAgentMan.Scenes.Rooms;
using SecretAgentMan.Sprites;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace SecretAgentMan.Scenes;

public class IngameScene : RetroGame.Scene.IngameScene
{
    private int _innocentKill;
    private ulong _lastInnocentKillAt;
    private int _messageDebug;
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
    public const int SpriteUpperLimit = 98;
    public const int SpriteLowerLimit = 276;

    public IngameScene(RetroGame.RetroGame parent) : base(parent)
    {
        _player = new Player(_fire.PlayerFire);
        _roomList = new RoomList(_player, _fire.EnemyFire);
        Score = 0;
        AddToAutoUpdate(Game1.TypeWriter);
        AddToAutoDraw(Game1.TypeWriter);
        UpdateRoomNameAndCheckClear(0);
    }

    private void UpdateRoomNameAndCheckClear(ulong ticks)
    {
        _currentRoomName = _roomList.GetDistrictName(_currentRoomIndex);

        if (_roomList.RoomIsClear(_currentRoomIndex) && ticks > 200)
            Game1.TypeWriter.SetText("this area is clear");
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

                if (Keyboard.IsKeyDown(Keys.RightShift) && Keyboard.IsKeyPressed(Keys.F9) && RetroGame.RetroGame.CheatFileAvailable)
                    Game1.Cheat = !Game1.Cheat;

                if (Keyboard.IsKeyDown(Keys.RightShift) && Keyboard.IsKeyPressed(Keys.F8) && RetroGame.RetroGame.CheatFileAvailable)
                    Score += 100;

                if (ticks % 7 == 0)
                {
                    _waterFrameIndex++;

                    if (_waterFrameIndex > 17)
                        _waterFrameIndex = 0;
                }

                _player.PlayerControl(ticks, Keyboard, _currentRoomIndex, out var nextRoom, out var previousRoom, _roomList);

                if (nextRoom)
                {
                    _currentRoomIndex++;
                    _fire.Clear();
                    UpdateRoomNameAndCheckClear(ticks);
                }
                else if (previousRoom)
                {
                    _currentRoomIndex--;
                    _fire.Clear();
                    UpdateRoomNameAndCheckClear(ticks);
                }

                _roomList.Act(_currentRoomIndex, ticks);

                if (Keyboard.IsKeyDown(Keys.RightShift) && Keyboard.IsKeyPressed(Keys.F10))
                {
                    _messageDebug++;
                    Game1.TypeWriter.SetText($"you have added text {_messageDebug} to the {(Game1.Random.Next(2) == 0 ? "message " : "")}system!{(Game1.Random.Next(2) == 0 ? " thank you!" : "")}");
                    // TODO Arg eller inte?
                }
            }

            _fire.Act(ticks);
            var coins = _roomList.GetCoins(_currentRoomIndex);

            foreach (var coin in coins)
            {
                coin.Act(ticks);

                if (coin.Collide(_player))
                {
                    Game1.PlayerCoin!.PlayRandom();
                    coins.Remove(coin);
                    Game1.TypeWriter.SetText("coin collected, 50 points awarded");
                    Score += 50;
                    break;
                }

                foreach (var npc in _roomList.GetNpcs(_currentRoomIndex))
                {
                    if (coin.Collide(npc))
                    {
                        Game1.EnemyCoin!.PlayRandom();
                        coins.Remove(coin);
                        Game1.TypeWriter.SetText("coin stolen");
                        goto OuterBail;
                    }
                }
            }

            OuterBail:;

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
                                        Game1.TypeWriter.SetText("you have killed an innocent man!");
                                        Score -= 10;
                                        break;
                                    case 2:
                                        Game1.TypeWriter.SetText("you cannot just go around and shoot people!");
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
                                Game1.TypeWriter.SetText($"all spies eliminated! {scoreAdded} points! well done!");
                                _fire.Clear();
                                _gameCompleted = true;
                                _gameCompletedAt = ticks;
                            }
                            else
                            {
                                MayorResources.SaySpyKilled(_killedSpyCount, scoreAdded);
                            }
                        }
                        break;
                    }
                }
            }

            if (_innocentKill >= 3 && ticks > _lastInnocentKillAt + 100)
            {
                ScoreManagement.StoreLastScore(Score);
                Parent.CurrentScene = new GameOverFiredScene(Parent);
                return;
            }

            if (_gameCompleted && ticks > _gameCompletedAt + 500)
            {
                ScoreManagement.StoreLastScore(Score);
                Parent.CurrentScene = new GameOverKilledScene(Parent); // TODO: Detta ska vara game completed.
                return;
            }

            if (!_gameCompleted)
            {
                _fire.RemoveOneDeadFire();
                _roomList.TurnOneDeadNpcToGraveStone(_currentRoomIndex);
                _player.DieIfHit(_fire.EnemyFire, ticks);
            }
        }

        if (_player.AliveStatus == Character.StatusDying)
            _player.Tick(ticks);

        if (_player.AliveStatus == Character.StatusDead)
        {
            ScoreManagement.StoreLastScore(Score);
            Parent.CurrentScene = new GameOverKilledScene(Parent);
        }
        System.Diagnostics.Debug.WriteLine(_player.Y);
        base.Update(gameTime, ticks);
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        Game1.Decoration.Draw(spriteBatch, _currentRoomIndex);
        Game1.BackgroundTempTexture?.Draw(spriteBatch, 0, 0, 0);
        Game1.WaterTexture?.Draw(spriteBatch, _waterFrameIndex, 0, 91);

        if (_askQuitMode)
        {
            const string quitText = "press f3 to quit, press f7 to continue.";
            var quitX = 320 - quitText.Length * 8 / 2;
            Text.DirectDraw(spriteBatch, quitX, 150, quitText, ColorPalette.White);
        }
        else
        {
            _roomList.DrawBackground(spriteBatch, _currentRoomIndex, Text, _player);
            
            foreach (var coin in _roomList.GetCoins(_currentRoomIndex))
                coin.Draw(spriteBatch);

            _fire.Draw(spriteBatch);
            _roomList.DrawDecorations(spriteBatch, _currentRoomIndex);
        }

        Text.DirectDraw(spriteBatch, 11, 11, _currentRoomName, Color.White);
        Text.DirectDraw(spriteBatch, 508, 11, ScoreString, ColorPalette.White);
        Game1.Hud?.Draw(spriteBatch, 0, 10, 292);

        if (_gameCompleted)
        {
            //TODO: Övergång till game completed.
            //Text.DirectDraw(spriteBatch, GameOverKilledScene.GameClearX, 150, GameOverKilledScene.GameClearText, ColorPalette.Green);
        }

        Game1.Frame!.Draw(spriteBatch, 0, 0, 0);
        base.Draw(gameTime, ticks, spriteBatch);
    }
}