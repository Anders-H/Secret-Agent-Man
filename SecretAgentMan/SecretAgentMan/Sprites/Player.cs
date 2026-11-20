using RetroGame.Input;
using SecretAgentMan.OtherResources;
using SecretAgentMan.Scenes;
using SecretAgentMan.Scenes.Rooms;

namespace SecretAgentMan.Sprites;

public class Player : Character
{
    public const int PlayerStartX = 30;
    public const int PlayerStartY = 250;
    private static readonly ushort[] _walkRight = [0, 1, 2, 3];
    private static readonly ushort[] _walkLeft = [4, 5, 6, 7];
    private static readonly ushort[] _die = [20, 21, 20, 21];
    private bool _changeAnimationCells;
    private bool _isMoving;
    private int _speed;
    public const int MaxBullets = 10;
    public int BulletsLeft { get; set; }
    public int AmmoBoxes { get; set; }

    public Player(FireList fireList) : base(fireList)
    {
        _speed = 2;
        _changeAnimationCells = false;
        _isMoving = false;
        AmmoBoxes = 0;
        BulletsLeft = MaxBullets;
        CurrentAnimation = _walkRight;
        X = PlayerStartX;
        Y = PlayerStartY;
    }

    public void Reset()
    {
        ForceRestoreToLookRight();
        AliveStatus = StatusAlive;
        _isMoving = false;
        _changeAnimationCells = false;
        X = PlayerStartX;
        Y = PlayerStartY;
    }

    public void TweakPlayerSpeed(int secretAgentManManipulatedSpeed) =>
        _speed = secretAgentManManipulatedSpeed;

    public void ResetBulletsLeft()
    {
        BulletsLeft = MaxBullets;
    }

    public bool RestoreToLookRight()
    {
        if (CurrentAnimation != _die)
            return false;

        CurrentAnimation = _walkRight;
        FaceRight = true;
        CurrentAnimationIndex = 0;
        return true;
    }

    public void ForceRestoreToLookRight()
    {
        CurrentAnimation = _walkRight;
        FaceRight = true;
        CurrentAnimationIndex = 0;
        ForceCellIndexToZero();
    }

    public void WalkTo(int x, int y, ulong ticks)
    {
        var oldX = IntX;
        var oldY = IntY;

        if (X > x && X - _speed >= x)
        {
            MoveLeft();

            if (CurrentAnimation != _walkLeft)
                CurrentAnimation = _walkLeft;
        }
        else if (X < x && X + _speed <= x)
        {
            MoveRight();

            if (CurrentAnimation != _walkRight)
                CurrentAnimation = _walkRight;
        }

        if (Y > y && Y - _speed >= y)
        {
            _isMoving = true;
            Y -= _speed;
        }
        else if (Y < y && Y + _speed <= y)
        {
            _isMoving = true;
            Y += _speed;
        }

        if ((oldX != IntX || oldY != IntY) && (ticks % 4 == 0))
            CurrentAnimationIndex++;
    }

    public void PlayerControl(ulong ticks, KeyboardStateChecker keyboard, int currentRoomIndex, out bool nextRoom, out bool previousRoom, RoomList rooms)
    {
        MoveFromInput(keyboard, IngameScene.SpriteUpperLimit, currentRoomIndex, rooms.Count, out nextRoom, out previousRoom);

        if (keyboard.IsFirePressed() && ticks > 2)
        {
            if (BulletsLeft > 0)
            {
                Fire(true);
                BulletsLeft--;

                if (BulletsLeft <= 0 && AmmoBoxes > 0)
                {
                    BulletsLeft = MaxBullets;
                    AmmoBoxes--;
                }
            }
            else
            {
                SoundEffects.FireNoAmmo!.PlayNext();
            }
        }

        AnimateMovement(ticks);
    }

    public void CutSceneFire()
    {
        Fire(false);
    }

    public void PlayerControlBonusRound(ulong ticks, KeyboardStateChecker keyboard)
    {
        MoveFromInput(keyboard, 0, 0, 1, out _, out _);
        if (keyboard.IsFirePressed() && ticks > 2)
            Fire(true);
        AnimateMovement(ticks);
    }

    private void MoveFromInput(KeyboardStateChecker keyboard, int minimumY, int roomIndex, int roomCount,
        out bool nextRoom, out bool previousRoom)
    {
        nextRoom = false;
        previousRoom = false;
        if (keyboard.MoveRightWasd())
        {
            MoveRight();
            if (X > 615)
            {
                nextRoom = roomIndex < roomCount - 1;
                X = nextRoom ? 0 : 615;
            }
        }
        else if (keyboard.MoveLeftWasd())
        {
            MoveLeft();
            if (X < 0)
            {
                previousRoom = roomIndex > 0;
                X = previousRoom ? 615 : 0;
            }
        }
        if (keyboard.MoveUpWasd())
        {
            _isMoving = true;
            Y = System.Math.Max(minimumY, Y - _speed);
        }
        else if (keyboard.MoveDownWasd())
        {
            _isMoving = true;
            Y = System.Math.Min(IngameScene.PlayerSpriteLowerLimit, Y + _speed);
        }
    }

    private void AnimateMovement(ulong ticks)
    {
        if (_changeAnimationCells)
        {
            CurrentAnimation = FaceRight ? _walkRight : _walkLeft;
            _changeAnimationCells = false;
        }
        if (_isMoving)
            Tick(ticks);
        _isMoving = false;
    }

    public void MoveLeft()
    {
        if (FaceRight)
        {
            FaceRight = false;
            _changeAnimationCells = true;
        }

        _isMoving = true;
        X -= _speed;
    }

    public void MoveLeftForce()
    {
        FaceRight = false;

        if (CurrentAnimation != _walkLeft)
            CurrentAnimation = _walkLeft;
        
        _isMoving = true;
        X -= _speed;
    }

    public void MoveRight()
    {
        if (!FaceRight)
        {
            FaceRight = true;
            _changeAnimationCells = true;
        }

        _isMoving = true;
        X += _speed;
    }

    public void MoveRightForce()
    {
        FaceRight = true;

        if (CurrentAnimation != _walkRight)
            CurrentAnimation = _walkRight;

        _isMoving = true;
        X += _speed;
    }

    public void DieIfHit(FireList enemyFire, ulong ticks)
    {
        foreach (var fire in enemyFire)
        {
            if (!Hit(fire))
                continue;

            Die(ticks);
            break;
        }
    }

    /// <summary>
    /// Exposed for testing - do not call directly.
    /// </summary>
    /// <param name="ticks"></param>
    public void Die(ulong ticks)
    {
        SoundEffects.PlayerDie!.PlayNext();
        CurrentAnimation = _die;
        AliveStatus = StatusDying;
        DieAtTicks = ticks;
    }

    public void Tick(ulong ticks)
    {
        if (ticks % 7 == 0)
            CurrentAnimationIndex++;

        if (AliveStatus == StatusDying && ticks > DieAtTicks + 60)
            AliveStatus = StatusDead;
    }
}