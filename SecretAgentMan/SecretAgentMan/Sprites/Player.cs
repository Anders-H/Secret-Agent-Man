using RetroGame.Input;
using SecretAgentMan.OtherResources;
using SecretAgentMan.Scenes;
using SecretAgentMan.Scenes.Rooms;

namespace SecretAgentMan.Sprites;

public class Player : Character
{
    private readonly ushort[] _walkRight = [0, 1, 2, 3];
    private readonly ushort[] _walkLeft = [4, 5, 6, 7];
    private readonly ushort[] _die = [20, 21, 20, 21];
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
        X = 30;
        Y = 250;
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

    public void PlayerControl(ulong ticks, KeyboardStateChecker keyboard, int currentRoomIndex, out bool nextRoom, out bool previousRoom, RoomList rooms)
    {
        nextRoom = false;
        previousRoom = false;

        if (keyboard.MoveRightWasd())
        {
            MoveRight();

            if (X > 615)
            {
                if (currentRoomIndex < rooms.Count - 1)
                {
                    nextRoom = true;
                    X = 0;
                }
                else
                {
                    X = 615;
                }
            }
        }
        else if (keyboard.MoveLeftWasd())
        {
            MoveLeft();

            if (X < 0)
            {
                if (currentRoomIndex > 0)
                {
                    previousRoom = true;
                    X = 615;
                }
                else
                {
                    X = 0;
                }
            }
        }

        if (keyboard.MoveUpWasd())
        {
            _isMoving = true;
            Y -= _speed;

            if (Y < IngameScene.SpriteUpperLimit)
                Y = IngameScene.SpriteUpperLimit;
        }
        else if (keyboard.MoveDownWasd())
        {
            _isMoving = true;
            Y += _speed;

            if (Y > IngameScene.SpriteLowerLimit)
                Y = IngameScene.SpriteLowerLimit;
        }

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

        if (_changeAnimationCells)
        {
            CurrentAnimation = FaceRight ? _walkRight : _walkLeft;
            _changeAnimationCells = false;
        }

        if (_isMoving)
        {
            Tick(ticks);

            if (AliveStatus == StatusAlive)
                _isMoving = false;
        }
    }

    public void CutSceneFire()
    {
        Fire(false);
    }

    public void PlayerControlBonusRound(ulong ticks, KeyboardStateChecker keyboard)
    {
        if (keyboard.MoveRightWasd())
        {
            MoveRight();

            if (X > 615)
                X = 615;
        }
        else if (keyboard.MoveLeftWasd())
        {
            MoveLeft();

            if (X < 0)
                X = 0;
        }

        if (keyboard.MoveUpWasd())
        {
            _isMoving = true;
            Y -= _speed;

            if (Y < 0)
                Y = 0;
        }
        else if (keyboard.MoveDownWasd())
        {
            _isMoving = true;
            Y += _speed;

            if (Y > 334)
                Y = 334;
        }

        if (keyboard.IsFirePressed() && ticks > 2)
            Fire(true);

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