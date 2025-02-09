using System.Collections.Generic;
using RetroGame.Input;
using RetroGame.Sprites;
using Microsoft.Xna.Framework.Input;
using SecretAgentMan.Scenes;

namespace SecretAgentMan;

public class Player : Sprite
{
    private readonly List<Fire> _fireList;
    private readonly int[] _walkRight = [0, 1, 2, 3];
    private readonly int[] _walkLeft = [4, 5, 6, 7];
    private readonly int[] _die = [20, 21, 20, 21];
    private int[] _currentAnimation;
    private int _currentAnimationIndex;
    private bool _faceRight;
    public int CellIndex { get; set; }
    public int AliveStatus { get; set; }
    public const int StatusAlive = 0;
    public const int StatusDying = 1;
    public const int StatusDead = 2;

    public Player(List<Fire> fireList)
    {
        AliveStatus = StatusAlive;
        _fireList = fireList;
        _faceRight = true;
        _currentAnimation = _walkRight;
        _currentAnimationIndex = 0;
        CellIndex = 1;
        X = 30;
        Y = 250;
    }

    public void PlayerControl(ulong ticks, KeyboardStateChecker keyboard, int currentRoomIndex, out bool nextRoom, out bool previousRoom)
    {
        nextRoom = false;
        previousRoom = false;
        var changeAnimationCells = false;
        var isMoving = false;

        if (keyboard.IsKeyDown(Keys.Right))
        {
            if (!_faceRight)
            {
                _faceRight = true;
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
        else if (keyboard.IsKeyDown(Keys.Left))
        {
            if (_faceRight)
            {
                _faceRight = false;
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

        if (keyboard.IsKeyDown(Keys.Up))
        {
            isMoving = true;
            Y -= 2;

            if (Y < IngameScene.SpriteUpperLimit)
                Y = IngameScene.SpriteUpperLimit;
        }
        else if (keyboard.IsKeyDown(Keys.Down))
        {
            isMoving = true;
            Y += 2;

            if (Y > 335)
                Y = 335;
        }

        if (keyboard.IsFirePressed() && ticks > 3)
        {
            if (_faceRight)
                _fireList.Add(new Fire(true, false, IntX + 11, IntY - 5));
            else
                _fireList.Add(new Fire(false, false, IntX - 10, IntY - 5));
        }

        if (changeAnimationCells)
            ChangeAnimationCells();

        if (isMoving)
            Tick(ticks);
    }

    private void ChangeAnimationCells()
    {
        _currentAnimation = _faceRight ? _walkRight : _walkLeft;
        _currentAnimationIndex = 0;
        CellIndex = _currentAnimation[_currentAnimationIndex];
    }

    public void Tick(ulong ticks)
    {
        if (ticks % 7 == 0)
        {
            _currentAnimationIndex++;

            if (_currentAnimationIndex >= _currentAnimation.Length)
                _currentAnimationIndex = 0;
        }

        CellIndex = _currentAnimation[_currentAnimationIndex];
    }
}