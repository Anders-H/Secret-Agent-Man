using RetroGame.Scene;
using RetroGame.Sprites;
using SecretAgentMan.Scenes;

namespace SecretAgentMan;

public class Npc : Sprite, IRetroActor
{
    private int _ticksSinceDirectionChange;
    private int _ticksSinceGunToggle;
    private readonly int[] _walkRight = [24, 25, 26, 27];
    private readonly int[] _walkLeft = [28, 29, 30, 31];
    private readonly int[] _die = [22, 23, 22, 23];
    private readonly int[] _walkRightWithGun = [8, 9, 10, 11];
    private readonly int[] _walkLeftWithGun = [12, 13, 14, 15];
    private int[] _currentAnimation;
    private int _currentAnimationIndex;
    private bool _gunUp;
    private bool _faceRight;
    private bool _isMovingUp;
    private bool _isMovingDown;
    private readonly ulong _speed;
    public int Status { get; private set; }
    public int CellIndex { get; set; }
    public const int StatusInnocent = 0;
    public const int StatusSpyUndetected = 1;
    public const int StatusSpyDetected = 2;
    public int AliveStatus { get; set; }
    public const int StatusAlive = 0;
    public const int StatusDying = 1;
    public const int StatusDead = 2;

    public Npc(int status)
    {
        Status = status;
        AliveStatus = StatusAlive;
        _ticksSinceDirectionChange = 0;
        _ticksSinceGunToggle = 0;
        _currentAnimationIndex = 0;
        _gunUp = false;
        _speed = (ulong)Game1.Random.Next(1, 8);

        if (Game1.Random.Next(0, 2) == 0)
        {
            _faceRight = true;
            _currentAnimation = _walkRight;
            X = 0 - Game1.Random.Next(100);
        }
        else
        {
            _faceRight = false;
            _currentAnimation = _walkLeft;
            X = 639 + Game1.Random.Next(100);
        }

        Y = Game1.Random.Next(IngameScene.SpriteUpperLimit, 336);
        CellIndex = _currentAnimation[_currentAnimationIndex];
    }

    public void Act(ulong ticks)
    {
        if (InFullView)
            _ticksSinceDirectionChange++;

        if (AliveStatus == StatusAlive)
        {
            if (ticks % _speed == 0)
            {
                _ticksSinceGunToggle++;

                if (Status != StatusInnocent & _ticksSinceGunToggle > 50)
                {
                    if (Game1.Random.Next(500) == 100)
                    {
                        _gunUp = !_gunUp;

                        if (_gunUp)
                        {
                            Status = StatusSpyDetected;
                            _currentAnimation = _faceRight ? _walkRightWithGun : _walkLeftWithGun;
                        }
                        else
                        {
                            _currentAnimation = _faceRight ? _walkRight : _walkLeft;
                        }
                    }
                }

                if (_faceRight)
                {
                    X += 1;

                    if (X > 615)
                    {
                        _ticksSinceDirectionChange = 0;
                        _faceRight = false;
                        _currentAnimation = _gunUp ? _walkLeftWithGun : _walkLeft;
                    }
                }
                else
                {
                    X -= 1;

                    if (X < 0)
                    {
                        _ticksSinceDirectionChange = 0;
                        _faceRight = true;
                        _currentAnimation = _gunUp ? _walkRightWithGun : _walkRight;
                    }
                }

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

                if (ticks % 7 == 0)
                {
                    _currentAnimationIndex++;

                    if (_currentAnimationIndex >= _currentAnimation.Length)
                        _currentAnimationIndex = 0;
                }

                CellIndex = _currentAnimation[_currentAnimationIndex];
            }

            if (_ticksSinceDirectionChange > 40)
            {
                var newDirection = Game1.Random.Next(200);

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
                        if (_faceRight)
                        {
                            _faceRight = false;
                            _currentAnimation = _gunUp ? _walkLeftWithGun : _walkLeft;
                            _ticksSinceDirectionChange = 0;
                        }
                        break;
                    case 13:
                        if (!_faceRight)
                        {
                            _faceRight = true;
                            _currentAnimation = _gunUp ? _walkRightWithGun : _walkRight;
                            _ticksSinceDirectionChange = 0;
                        }
                        break;
                }
            }
        }
        else if (AliveStatus == StatusDying)
        {
            if (ticks % 10 == 0)
            {
                _currentAnimationIndex++;

                if (_currentAnimationIndex >= _currentAnimation.Length)
                    AliveStatus = StatusDead;
                else
                    CellIndex = _currentAnimation[_currentAnimationIndex];
            }
        }
    }

    public bool Hit(Fire fire)
    {
        var fireX = fire.X + 12;
        var fireY = fire.Y + 12;

        if (fireX < IntX || fireX > IntX + 25)
            return false;

        if (fireY < IntY || fireY > IntY + 25)
            return false;

        return true;
    }

    public void Die()
    {
        _currentAnimation = _die;
        _currentAnimationIndex = 0;
        CellIndex = _currentAnimation[_currentAnimationIndex];
        AliveStatus = StatusDying;
    }

    private bool InFullView =>
        X >= 0 && X <= 615;

    public static Npc CreateInnocent()
    {
        var n = new Npc(StatusInnocent);
        return n;
    }

    public static Npc CreateSpy()
    {
        var n = new Npc(StatusSpyUndetected);
        return n;
    }
}