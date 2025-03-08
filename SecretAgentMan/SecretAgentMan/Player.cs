using System.Collections.Generic;
using RetroGame.Input;
using SecretAgentMan.Scenes;

namespace SecretAgentMan;

public class Player : Character
{
    private readonly int[] _walkRight = [0, 1, 2, 3];
    private readonly int[] _walkLeft = [4, 5, 6, 7];
    private readonly int[] _die = [20, 21, 20, 21];

    public Player(List<Fire> fireList) : base(fireList)
    {
        CurrentAnimation = _walkRight;
        X = 30;
        Y = 250;
    }

    public void PlayerControl(ulong ticks, KeyboardStateChecker keyboard, int currentRoomIndex, out bool nextRoom, out bool previousRoom)
    {
        nextRoom = false;
        previousRoom = false;
        var changeAnimationCells = false;
        var isMoving = false;

        if (keyboard.MoveRight())
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
                if (currentRoomIndex < 4)
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
        else if (keyboard.MoveLeft())
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

        if (keyboard.MoveUp())
        {
            isMoving = true;
            Y -= 2;

            if (Y < IngameScene.SpriteUpperLimit)
                Y = IngameScene.SpriteUpperLimit;
        }
        else if (keyboard.MoveDown())
        {
            isMoving = true;
            Y += 2;

            if (Y > 335)
                Y = 335;
        }

        if (keyboard.IsFirePressed() && ticks > 3)
            Fire(false);

        if (changeAnimationCells)
            CurrentAnimation = FaceRight ? _walkRight : _walkLeft;

        if (isMoving)
            Tick(ticks);
    }

    public void Die(ulong ticks)
    {
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