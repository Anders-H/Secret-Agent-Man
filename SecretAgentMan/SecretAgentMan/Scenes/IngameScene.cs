using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
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
    private GameEventPointer _gameCompleted = new();
    private short _currentBonusLevel;
    private ulong _bonusReached52At;
    private int _lives;
    private int _zeroBasedLevel;
    private string _levelString = "";
    public const int SpriteUpperLimit = 98;
    public const int SpriteLowerLimit = 276;

    public IngameScene(RetroGame.RetroGame parent) : base(parent)
    {
        _player = new Player(_fire.PlayerFire);
        _roomList = new RoomList(_player, _fire.EnemyFire, 0);
        Score = 0;
        AddToAutoUpdate(Game1.TypeWriter);
        AddToAutoDraw(Game1.TypeWriter);
        UpdateRoomNameAndCheckClear(0);
        _lives = 2;
        MediaPlayer.Stop();
        ZeroBasedLevel = 0;
    }

    private int ZeroBasedLevel
    {
        //get => _zeroBasedLevel;
        set
        {
            _zeroBasedLevel = value;
            _levelString = $"level    {_zeroBasedLevel + 1}";
        }
    }

    private void UpdateRoomNameAndCheckClear(ulong ticks)
    {
        _currentRoomName = _roomList.GetDistrictName(_currentRoomIndex);

        if (_roomList.RoomIsClear(_currentRoomIndex) && ticks > 200)
        {
            MayorResources.DoShortTalk();
            Game1.TypeWriter.SetText("this area is clear");
        }
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

                if (RetroGame.RetroGame.CheatFileAvailable)
                {
                    if (Keyboard.IsKeyDown(Keys.RightShift) && Keyboard.IsKeyPressed(Keys.F9))
                        Game1.Cheat = !Game1.Cheat;

                    if (Keyboard.IsKeyDown(Keys.RightShift) && Keyboard.IsKeyPressed(Keys.F8))
                        Score += 100;

                    if (Keyboard.IsKeyDown(Keys.RightShift) && Keyboard.IsKeyPressed(Keys.B))
                        Parent.CurrentScene = new SignScene(Parent, "bonus", new BonusLevelScene(Parent, Score, AddScore));

                    if (Keyboard.IsKeyDown(Keys.RightShift) && Keyboard.IsKeyPressed(Keys.W))
                        Parent.CurrentScene = new SignScene(Parent, "act i", new CutScene1(Parent));

                    if (Keyboard.IsKeyDown(Keys.RightShift) && Keyboard.IsKeyPressed(Keys.F))
                        _player.ResetBulletsLeft();
                }

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
                    SoundEffects.PlayerCoin!.PlayRandom();
                    coins.Remove(coin);
                    Score += 50;
                    _currentBonusLevel++;
                    break;
                }

                foreach (var npc in _roomList.GetNpcs(_currentRoomIndex))
                {
                    if (coin.Collide(npc))
                    {
                        SoundEffects.EnemyCoin!.PlayRandom();
                        coins.Remove(coin);
                        goto CoinOuterBail;
                    }
                }
            }

            CoinOuterBail:;

            var ammos = _roomList.GetAmmos(_currentRoomIndex);

            if (_player.AmmoBoxes < 4)
            {
                foreach (var ammo in ammos)
                {
                    if (ammo.Collide(_player))
                    {
                        SoundEffects.AmmoBox!.PlayNext();
                        ammos.Remove(ammo);

                        if (_player.BulletsLeft <= 0 && _player.AmmoBoxes <= 0)
                        {
                            _player.BulletsLeft = Player.MaxBullets;
                        }
                        else
                        {
                            _player.AmmoBoxes++;
                        }

                        break;
                    }
                }
            }

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
                                _currentBonusLevel -= 2;

                                if (_currentBonusLevel < 0)
                                    _currentBonusLevel = 0;

                                switch (_innocentKill)
                                {
                                    case 1:
                                        MayorResources.DoShortTalk();
                                        Game1.TypeWriter.SetText("you have killed an innocent man!");
                                        Score -= 10;
                                        break;
                                    case 2:
                                        MayorResources.DoShortTalkAngry();
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
                                _gameCompleted.Occure(ticks);
                                _currentBonusLevel++;
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

            if (_gameCompleted.OccuredTicksAgo(ticks, 500))
            {
                // TODO: Next level or game completed.
                ScoreManagement.StoreLastScore(Score);
                Parent.CurrentScene = new GameOverKilledScene(Parent); // TODO: Detta ska vara game completed.
                return;
            }

            if (!_gameCompleted.Occured)
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

        if (!_gameCompleted.Occured && _player.AliveStatus == Character.StatusAlive)
        {
            if (_currentBonusLevel >= 52 && _bonusReached52At <= 0)
            {
                _bonusReached52At = ticks;
            }
            else if (_currentBonusLevel >= 52 && _bonusReached52At > 10)
            {
                if (ticks - _bonusReached52At > 200)
                {
                    _bonusReached52At = 0;
                    _currentBonusLevel = 0;
                    Parent.CurrentScene = new SignScene(Parent, "bonus", new BonusLevelScene(Parent, Score, AddScore));
                }
            }
        }

        MayorResources.Act(ticks);
        base.Update(gameTime, ticks);
    }

    public int AddScore(int points) =>
        DirectAddScore(points);

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        Game1.Decoration.Draw(spriteBatch, _currentRoomIndex);
        IngameBackgroundResources.WaterTexture?.Draw(spriteBatch, _waterFrameIndex, 0, 91);

        if (_askQuitMode)
        {
            const string quitText = "press f3 to quit, press f7 to continue.";
            var quitX = 320 - quitText.Length * 8 / 2;
            Text.DirectDraw(spriteBatch, quitX, 150, quitText, ColorPalette.White);
        }
        else
        {
            _roomList.DrawBackground(spriteBatch, _currentRoomIndex, Text, _player);
            _roomList.GetCoins(_currentRoomIndex).Draw(spriteBatch);
            _roomList.GetAmmos(_currentRoomIndex).ForEach(x => x.Draw(spriteBatch));
            _fire.Draw(spriteBatch);
            _roomList.DrawDecorations(spriteBatch, _currentRoomIndex);
        }

        Text.DirectDraw(spriteBatch, 12, 12, _currentRoomName, Color.White);
        Text.DirectDraw(spriteBatch, 508, 12, ScoreString, ColorPalette.White);
        Game1.Hud?.Draw(spriteBatch, 0, 10, 292);
        Text.DirectDraw(spriteBatch, 544, 299, "lives", ColorPalette.White);
        Text.DirectDraw(spriteBatch, 544, 307, "faults", ColorPalette.White);
        
        if (_player.AmmoBoxes <= 0)
        {
            switch (_player.BulletsLeft)
            {
                case 0:
                    Text.DirectDraw(spriteBatch, 544, 315, "ammo", ticks % 16 < 8 ? ColorPalette.White : ColorPalette.Red);
                    break;
                case 1:
                    Text.DirectDraw(spriteBatch, 544, 315, "ammo", ticks % 18 < 9 ? ColorPalette.White : ColorPalette.Orange);
                    break;
                case 2:
                    Text.DirectDraw(spriteBatch, 544, 315, "ammo", ticks % 20 < 10 ? ColorPalette.White : ColorPalette.Yellow);
                    break;
                default:
                    Text.DirectDraw(spriteBatch, 544, 315, "ammo", ColorPalette.White);
                    break;
            }
        }
        else
        {
            Text.DirectDraw(spriteBatch, 544, 315, "ammo", ColorPalette.White);
        }

        if (_player.BulletsLeft > 0)
        {
            var ammoX = 621;

            for (var x = 0; x < _player.BulletsLeft; x++)
            {
                Game1.AmmoTexture!.Draw(spriteBatch, 0, ammoX, 315);
                ammoX -= 5;
            }
        }

        Text.DirectDraw(spriteBatch, 544, 323, _levelString, ColorPalette.White);

        switch (_lives)
        {
            case 2:
                Game1.LivesSymbolTexture!.Draw(spriteBatch, 0, 590, 298);
                Game1.LivesSymbolTexture.Draw(spriteBatch, 0, 602, 298);

                if (ticks % 40 > 20)
                    Game1.LivesSymbolTexture.Draw(spriteBatch, 0, 614, 298);

                break;
            case 1:
                Game1.LivesSymbolTexture!.Draw(spriteBatch, 0, 590, 298);
                
                if (ticks % 40 > 20)
                    Game1.LivesSymbolTexture.Draw(spriteBatch, 0, 602, 298);

                break;
            case 0:
                if (ticks % 40 > 20)
                    Game1.LivesSymbolTexture!.Draw(spriteBatch, 0, 590, 298);

                break;
        }

        switch (_player.AmmoBoxes)
        {
            case 4:
                AmmoBox.AmmoBoxTexture!.Draw(spriteBatch, 0, 543, 335);
                AmmoBox.AmmoBoxTexture.Draw(spriteBatch, 0, 564, 335);
                AmmoBox.AmmoBoxTexture.Draw(spriteBatch, 0, 585, 335);
                AmmoBox.AmmoBoxTexture.Draw(spriteBatch, 0, 606, 335);
                break;
            case 3:
                AmmoBox.AmmoBoxTexture!.Draw(spriteBatch, 0, 543, 335);
                AmmoBox.AmmoBoxTexture.Draw(spriteBatch, 0, 564, 335);
                AmmoBox.AmmoBoxTexture.Draw(spriteBatch, 0, 585, 335);
                break;
            case 2:
                AmmoBox.AmmoBoxTexture!.Draw(spriteBatch, 0, 543, 335);
                AmmoBox.AmmoBoxTexture.Draw(spriteBatch, 0, 564, 335);
                break;
            case 1:
                AmmoBox.AmmoBoxTexture!.Draw(spriteBatch, 0, 543, 335);
                break;
        }

        if (_gameCompleted.Occured)
        {
            //TODO: Övergång till game completed.
            //Text.DirectDraw(spriteBatch, GameOverKilledScene.GameClearX, 150, GameOverKilledScene.GameClearText, ColorPalette.Green);
        }

        Game1.Frame!.Draw(spriteBatch, 0, 0, 0);

        if (_currentBonusLevel > 0)
        {
            var frame = _currentBonusLevel / 2;

            frame = frame switch
            {
                < 0 => 0,
                > 25 => 25,
                _ => frame
            };

            if (_bonusReached52At > 10)
            {
                if (ticks % 15 < 7)
                    Game1.BonusMeter!.Draw(spriteBatch, frame, 521, 297);
            }
            else
            {
                Game1.BonusMeter!.Draw(spriteBatch, frame, 521, 297);
            }
        }
        
        MayorResources.Draw(spriteBatch);
        base.Draw(gameTime, ticks, spriteBatch);
    }
}