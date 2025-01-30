using RetroGame.Sprites;

namespace SecretAgentMan;

public class Player : Sprite
{
    private readonly int[] _walkRight = [0, 1, 2, 3];
    private readonly int[] _walkLeft = [4, 5, 6, 7];
    private int[] _currentAnimation;
    private int _currentAnimationIndex;
    public int CellIndex { get; set; }
    public bool FaceRight { get; set; }

    public Player()
    {
        FaceRight = true;
        _currentAnimation = _walkRight;
        _currentAnimationIndex = 0;
        CellIndex = 1;
        X = 30;
        Y = 250;
    }

    public void ChangeAnimationCells()
    {
        _currentAnimation = FaceRight ? _walkRight : _walkLeft;
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