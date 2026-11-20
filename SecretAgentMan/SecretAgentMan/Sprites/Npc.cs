using System;
using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using RetroGame.Scene;
using SecretAgentMan.OtherResources;
using IngameScene = SecretAgentMan.Scenes.IngameScene;

namespace SecretAgentMan.Sprites;

public class Npc : Character, IRetroActor
{
    private readonly Player? _player;
    private int _ticksSinceDirectionChange;
    private int _ticksSinceGunToggle;
    private int _ticksSinceLastFire;
    private static readonly ushort[] _walkRight = [24, 25, 26, 27];
    private static readonly ushort[] _walkLeft = [28, 29, 30, 31];
    private static readonly ushort[] _die = [22, 23, 22, 23];
    private static readonly ushort[] _walkRightWithGun = [8, 9, 10, 11];
    private static readonly ushort[] _walkLeftWithGun = [12, 13, 14, 15];
    private static readonly ushort[] Speeds = [1, 2, 1, 1, 2, 2, 3, 3, 3, 4, 5, 4, 5, 7, 7, 1, 7, 2, 6, 3, 5, 4, 8, 4, 8];
    private bool _gunUp;
    private bool _isMovingUp;
    private bool _isMovingDown;
    private readonly ulong _speed;
    private int _graveStoneCellIndex;
    public int Status { get; private set; }
    public const ushort StatusCutScene = 20;
    public const ushort StatusBonus = 10;
    public const ushort StatusInnocent = 0;
    public const ushort StatusSpyUndetected = 1;
    public const ushort StatusSpyDetected = 2;
    public bool IsGraveStone { get; private set; }

    public Npc(int status, Player? player, FireList enemyFireList, ulong speed) : base(enemyFireList)
    {
        _player = player;
        Status = status;
        _ticksSinceDirectionChange = 0;
        _ticksSinceGunToggle = 0;
        _ticksSinceLastFire = 0;
        _gunUp = false;
        _speed = speed;

        if (Status == StatusBonus)
        {
            _gunUp = true;
            FaceRight = false;
            CurrentAnimation = _walkLeftWithGun;
            X = 639 + Game1.Random.Next(500);
            Y = Game1.Random.Next(0, IngameScene.SpriteLowerLimit);
        }
        else if (Status == StatusCutScene)
        {
            _gunUp = true;
            FaceRight = false;
            CurrentAnimation = _walkLeftWithGun;
        }
        else
        {
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

            Y = Game1.Random.Next(IngameScene.SpriteUpperLimit, IngameScene.SpriteLowerLimit);
        }
    }

    public void Act(ulong ticks) => ActCore(ticks, false);

    public void ActBonus(ulong ticks) => ActCore(ticks, true);

    private void ActCore(ulong ticks, bool bonus)
    {
        if (IsGraveStone)
        {
            if (ticks % 6 == 0 && _graveStoneCellIndex < 12)
                _graveStoneCellIndex++;
            return;
        }
        if (InFullView)
            _ticksSinceDirectionChange++;
        if (AliveStatus == StatusAlive)
        {
            if (!bonus && ticks % 3 == 0)
                PerhapsToggleGunDrawn();
            if (ticks % (_speed + 1) == 0)
            {
                MoveWithinBounds(bonus ? 0 : IngameScene.SpriteUpperLimit);
                if (!bonus && _gunUp)
                {
                    if (_ticksSinceLastFire > 7 && Game1.Random.Next(6) == 4 && InPositionToShootPlayer())
                    {
                        Fire(true);
                        _ticksSinceLastFire = 0;
                    }
                    else
                        _ticksSinceLastFire++;
                }
            }
            if (ticks % (_speed + 9) == 0)
                CurrentAnimationIndex++;
            if (_ticksSinceDirectionChange > 60)
                PerhapsChangeDirection();
        }
        else if (AliveStatus == StatusDying && ticks % 10 == 0)
        {
            CurrentAnimationIndex++;
            if (ticks - DieAtTicks > 40)
                AliveStatus = StatusDead;
        }
    }

    private void MoveWithinBounds(int minimumY)
    {
        if (_isMovingUp)
            Y = Math.Max(minimumY, Y - 1);
        else if (_isMovingDown)
            Y = Math.Min(IngameScene.SpriteLowerLimit, Y + 1);
        X += FaceRight ? 1 : -1;
        if ((FaceRight && X > 615) || (!FaceRight && X < 0))
        {
            _ticksSinceDirectionChange = 0;
            FaceRight = !FaceRight;
            CurrentAnimation = FaceRight
                ? (_gunUp ? _walkRightWithGun : _walkRight)
                : (_gunUp ? _walkLeftWithGun : _walkLeft);
        }
    }

    public void ActCutScene(ulong ticks)
    {
        if (ticks % (_speed + 1) == 0)
            X -= 1;

        if (ticks % (_speed + 9) == 0)
            CurrentAnimationIndex++;
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

        if (Status != StatusInnocent && _ticksSinceGunToggle > 150)
        {
            if (Game1.Random.Next(_gunUp ? 550 : 110) == 100)
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

    public void Die(ulong ticks, bool playSound)
    {
        if (playSound)
            SoundEffects.EnemyDie!.PlayRandom();

        CurrentAnimation = _die;
        AliveStatus = StatusDying;
        DieAtTicks = ticks;
    }

    private bool InFullView =>
        X >= 0 && X <= 615;

    private static ulong GetSpeed(int index) =>
        index < Speeds.Length ? Speeds[index] : (ulong)Game1.Random.Next(1, 9);

    public static Npc CreateInnocent(FireList enemyFireList, int index) =>
        new(StatusInnocent, null, enemyFireList, GetSpeed(index));

    public static Npc CreateSpy(Player player, FireList enemyFireList, int index) =>
        new(StatusSpyUndetected, player, enemyFireList, GetSpeed(index));

    public static Npc CreateBonus(int index) =>
        new(StatusBonus, null, [], GetSpeed(index));

    public bool PlayerMayNotKill() =>
        Status == StatusInnocent || Status == StatusSpyUndetected;

    public void Draw(SpriteBatch spriteBatch)
    {
        if (IsGraveStone)
        {
            Game1.GraveStoneTexture?.Draw(spriteBatch, _graveStoneCellIndex, IntX, IntY, ColorPalette.White);
            return;
        }

        Draw(spriteBatch, IngameBackgroundResources.CharactersTexture, CellIndex, ColorPalette.White);
    }

    public void TurnToGraveStone()
    {
        _graveStoneCellIndex = 0;
        IsGraveStone = true;
        Y += 6;
    }

    public override int IntYForYSort =>
        IsGraveStone ? IntY - 8 : IntY;

    public void PutTheGunAway()
    {
        _gunUp = false;
        CurrentAnimation = FaceRight ? _walkRight : _walkLeft;
    }
}