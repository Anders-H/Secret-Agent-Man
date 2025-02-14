using RetroGame.Sprites;
using System.Collections.Generic;

namespace SecretAgentMan;

public abstract class Character : Sprite
{
    private int[]? _currentAnimation;
    private int _currentAnimationIndex;
    protected readonly List<Fire> FireList;
    protected bool FaceRight;
    public const int StatusAlive = 0;
    public const int StatusDying = 1;
    public const int StatusDead = 2;
    public int CellIndex { get; private set; }
    public int AliveStatus { get; set; }

    protected Character(List<Fire> fireList)
    {
        FireList = fireList;
        AliveStatus = StatusAlive;
        FaceRight = true;
    }

    protected int[]? CurrentAnimation
    {
        get => _currentAnimation;
        set
        {
            _currentAnimation = value;
            CurrentAnimationIndex = 0;
        }
    }

    protected int CurrentAnimationIndex
    {
        get => _currentAnimationIndex;
        set
        {
            _currentAnimationIndex = value;

            if (_currentAnimation != null)
            {
                if (_currentAnimationIndex >= _currentAnimation.Length)
                    _currentAnimationIndex = 0;

                CellIndex = _currentAnimation[_currentAnimationIndex];
            }
        }
    }

    protected void Fire(bool isEnemy)
    {
        if (FaceRight)
            FireList.Add(new Fire(true, isEnemy, IntX + 11, IntY - 5));
        else
            FireList.Add(new Fire(false, isEnemy, IntX - 10, IntY - 5));
    }
}