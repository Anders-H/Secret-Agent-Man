using RetroGame.Sprites;
using System.Collections.Generic;

namespace SecretAgentMan;

public abstract class Character : Sprite
{
    private int[]? _currentAnimation;
    private int _currentAnimationIndex;
    protected readonly List<Fire> FireList;
    protected bool FaceRight;
    protected ulong DieAtTicks;
    public const int StatusAlive = 0;
    public const int StatusDying = 1;
    public const int StatusDead = 2;
    public int CellIndex { get; private set; }
    public int AliveStatus { get; set; }
    
    protected Character(List<Fire> fireList)
    {
        DieAtTicks = 0;
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
        Game1.EnemyFire!.PlayRandom();

        if (FaceRight)
            FireList.Add(new Fire(true, isEnemy, IntX + 11, IntY - 5));
        else
            FireList.Add(new Fire(false, isEnemy, IntX - 10, IntY - 5));
    }

    public bool Hit(Fire fire)
    {
        var fireX = fire.X + 12;
        var fireY = fire.Y + 12;
        var xLimitLeft = IntX + 3;
        var xLimitRight = IntX + 21
            ;
        if (fireX < xLimitLeft || fireX > xLimitRight)
            return false;

        if (fireY < IntY || fireY > IntY + 25)
            return false;

        return true;
    }
}