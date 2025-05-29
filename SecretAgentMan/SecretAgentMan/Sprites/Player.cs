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
    public const int MaxBullets = 10;
    public int BulletsLeft { get; set; }
    public int AmmoBoxes { get; set; }

    public Player(FireList fireList) : base(fireList)
    {
        AmmoBoxes = 0;
        BulletsLeft = MaxBullets;
        CurrentAnimation = _walkRight;
        X = 30;
        Y = 250;
    }

    public void ResetBulletsLeft()
    {
        BulletsLeft = MaxBullets;
    }

    public void PlayerControl(ulong ticks, KeyboardStateChecker keyboard, int currentRoomIndex, out bool nextRoom, out bool previousRoom, RoomList rooms)
    {
        nextRoom = false;
        previousRoom = false;
        var changeAnimationCells = false;
        var isMoving = false;

        if (keyboard.MoveRightWasd())
        {
            if (!FaceRight)
            {
                FaceRight = true;
                changeAnimationCells = true;
            }

            isMoving = true;
            X += 2;

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
            if (FaceRight)
            {
                FaceRight = false;
                changeAnimationCells = true;
            }

            isMoving = true;
            X -= 2;

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
            isMoving = true;
            Y -= 2;

            if (Y < IngameScene.SpriteUpperLimit)
                Y = IngameScene.SpriteUpperLimit;
        }
        else if (keyboard.MoveDownWasd())
        {
            isMoving = true;
            Y += 2;

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

        if (changeAnimationCells)
            CurrentAnimation = FaceRight ? _walkRight : _walkLeft;

        if (isMoving)
            Tick(ticks);
    }

    public void PlayerControlBonusRound(ulong ticks, KeyboardStateChecker keyboard)
    {
        var changeAnimationCells = false;
        var isMoving = false;

        if (keyboard.MoveRightWasd())
        {
            if (!FaceRight)
            {
                FaceRight = true;
                changeAnimationCells = true;
            }

            isMoving = true;
            X += 2;

            if (X > 615)
                X = 615;
        }
        else if (keyboard.MoveLeftWasd())
        {
            if (FaceRight)
            {
                FaceRight = false;
                changeAnimationCells = true;
            }

            isMoving = true;
            X -= 2;

            if (X < 0)
                X = 0;
        }

        if (keyboard.MoveUpWasd())
        {
            isMoving = true;
            Y -= 2;

            if (Y < 0)
                Y = 0;
        }
        else if (keyboard.MoveDownWasd())
        {
            isMoving = true;
            Y += 2;

            if (Y > 334)
                Y = 334;
        }

        if (keyboard.IsFirePressed() && ticks > 2)
            Fire(true);

        if (changeAnimationCells)
            CurrentAnimation = FaceRight ? _walkRight : _walkLeft;

        if (isMoving)
            Tick(ticks);
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

    private void Die(ulong ticks)
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