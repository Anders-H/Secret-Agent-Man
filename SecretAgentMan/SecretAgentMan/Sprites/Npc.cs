using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using RetroGame.Scene;
using IngameScene = SecretAgentMan.Scenes.IngameScene;

namespace SecretAgentMan.Sprites;

public class Npc : Character, IRetroActor, IGameFieldThings
{
    private readonly Player? _player;
    private int _ticksSinceDirectionChange;
    private int _ticksSinceGunToggle;
    private int _ticksSinceLastFire;
    private readonly int[] _walkRight = [24, 25, 26, 27];
    private readonly int[] _walkLeft = [28, 29, 30, 31];
    private readonly int[] _die = [22, 23, 22, 23];
    private readonly int[] _walkRightWithGun = [8, 9, 10, 11];
    private readonly int[] _walkLeftWithGun = [12, 13, 14, 15];
    private static readonly ulong[] Speeds = [1, 2, 1, 1, 2, 2, 3, 3, 3, 4, 5, 4, 5, 7, 7];
    private bool _gunUp;
    private bool _isMovingUp;
    private bool _isMovingDown;
    private readonly ulong _speed;
    private int _graveStoneCellIndex;
    public int Status { get; private set; }
    public const int StatusInnocent = 0;
    public const int StatusSpyUndetected = 1;
    public const int StatusSpyDetected = 2;
    public bool IsGraveStone { get; private set; }

    public Npc(int status, Player? player, List<Fire> enemyFireList, ulong speed) : base(enemyFireList)
    {
        _player = player;
        Status = status;
        _ticksSinceDirectionChange = 0;
        _ticksSinceGunToggle = 0;
        _ticksSinceLastFire = 0;
        _gunUp = false;
        _speed = speed;

        if (Game1.Random.Next(0, 2) == 0)
        {
            FaceRight = true;
            CurrentAnimation = _walkRight;
            X = 0 - Game1.Random.Next(100);
        }
        else
        {
            FaceRight = false;
            CurrentAnimation = _walkLeft;
            X = 639 + Game1.Random.Next(100);
        }

        Y = Game1.Random.Next(IngameScene.SpriteUpperLimit, 336);
    }

    public void Act(ulong ticks)
    {
        if (IsGraveStone)
        {
            if (ticks % 15 == 0 && _graveStoneCellIndex < 12)
                _graveStoneCellIndex++;

            return;
        }

        if (InFullView)
            _ticksSinceDirectionChange++;

        if (AliveStatus == StatusAlive)
        {
            if (ticks % 3 == 0)
                PerhapsToggleGunDrawn();

            if (ticks % (_speed + 1) == 0)
            {
                if (_isMovingUp)
                {
                    Y -= 1;

                    if (Y < IngameScene.SpriteUpperLimit)
                        Y = IngameScene.SpriteUpperLimit;
                }
                else if (_isMovingDown)
                {
                    Y += 1;

                    if (Y > 335)
                        Y = 335;
                }

                if (FaceRight)
                {
                    X += 1;

                    if (X > 615)
                    {
                        _ticksSinceDirectionChange = 0;
                        FaceRight = false;
                        CurrentAnimation = _gunUp ? _walkLeftWithGun : _walkLeft;
                    }
                }
                else
                {
                    X -= 1;

                    if (X < 0)
                    {
                        _ticksSinceDirectionChange = 0;
                        FaceRight = true;
                        CurrentAnimation = _gunUp ? _walkRightWithGun : _walkRight;
                    }
                }

                if (_gunUp)
                {
                    if (_ticksSinceLastFire > 7 && Game1.Random.Next(6) == 4 && InPositionToShootPlayer())
                    {
                        Fire(true);
                        _ticksSinceLastFire = 0;
                    }
                    else
                    {
                        _ticksSinceLastFire++;
                    }
                }
            }

            if (ticks % (_speed + 9) == 0)
                CurrentAnimationIndex++;

            if (_ticksSinceDirectionChange > 60)
                PerhapsChangeDirection();
        }
        else if (AliveStatus == StatusDying)
        {
            if (ticks % 10 == 0)
            {
                CurrentAnimationIndex++;

                if (ticks - DieAtTicks > 40)
                    AliveStatus = StatusDead;
            }
        }
    }

    private void PerhapsChangeDirection()
    {
        var newDirection = Game1.Random.Next(300);

        switch (newDirection)
        {
            case 6:
            case 7:
                if (!_isMovingUp)
                {
                    _isMovingUp = true;
                    _isMovingDown = false;
                    _ticksSinceDirectionChange = 0;
                }
                break;
            case 8:
            case 9:
                if (_isMovingUp || _isMovingDown)
                {
                    _isMovingUp = false;
                    _isMovingDown = false;
                    _ticksSinceDirectionChange = 0;
                }
                break;
            case 10:
            case 11:
                if (!_isMovingDown)
                {
                    _isMovingUp = false;
                    _isMovingDown = true;
                    _ticksSinceDirectionChange = 0;
                }
                break;
            case 12:
                if (FaceRight)
                {
                    FaceRight = false;
                    CurrentAnimation = _gunUp ? _walkLeftWithGun : _walkLeft;
                    _ticksSinceDirectionChange = 0;
                }
                break;
            case 13:
                if (!FaceRight)
                {
                    FaceRight = true;
                    CurrentAnimation = _gunUp ? _walkRightWithGun : _walkRight;
                    _ticksSinceDirectionChange = 0;
                }
                break;
        }
    }

    private void PerhapsToggleGunDrawn()
    {
        _ticksSinceGunToggle++;

        if (Status != StatusInnocent & _ticksSinceGunToggle > 150)
        {
            if (Game1.Random.Next(550) == 100)
            {
                _gunUp = !_gunUp;

                if (_gunUp)
                {
                    Status = StatusSpyDetected;
                    CurrentAnimation = FaceRight ? _walkRightWithGun : _walkLeftWithGun;
                    _ticksSinceLastFire = 0;
                }
                else
                {
                    CurrentAnimation = FaceRight ? _walkRight : _walkLeft;
                }
            }
        }
    }

    private bool InPositionToShootPlayer()
    {
        if (_player == null)
            return false;

        if (FaceRight)
        {
            if (_player.X - X < 45)
                return false;
        }
        else
        {
            if (X - _player.X < 25)
                return false;
        }

        if (Math.Abs(_player.X - X) > 200)
            return false;

        return true;
    }

    public void Die(ulong ticks)
    {
        Game1.EnemyDie!.PlayRandom();
        CurrentAnimation = _die;
        AliveStatus = StatusDying;
        DieAtTicks = ticks;
    }

    private bool InFullView =>
        X >= 0 && X <= 615;

    public static Npc CreateInnocent(List<Fire> enemyFireList, int index)
    {
        var n = new Npc(StatusInnocent, null, enemyFireList, Speeds[index]);
        return n;
    }

    public static Npc CreateSpy(Player player, List<Fire> enemyFireList, int index)
    {
        var n = new Npc(StatusSpyUndetected, player, enemyFireList, Speeds[index]);
        return n;
    }

    public new int IntY =>
        IsGraveStone ? base.IntY - 10 : base.IntY;

    public bool PlayerMayNotKill() =>
        Status == StatusInnocent || Status == StatusSpyUndetected;

    public void Draw(SpriteBatch spriteBatch)
    {
        if (IsGraveStone)
        {
            Game1.GraveStoneTexture?.Draw(spriteBatch, _graveStoneCellIndex, IntX, base.IntY, ColorPalette.White);
            return;
        }

        Draw(spriteBatch, Game1.CharactersTexture, CellIndex, ColorPalette.White);
    }

    public void TurnToGraveStone()
    {
        _graveStoneCellIndex = 0;
        IsGraveStone = true;
        Y += 3;
    }
}