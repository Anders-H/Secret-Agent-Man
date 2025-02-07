using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using RetroGame.Scene;
using RetroGame.Sprites;

namespace SecretAgentMan;

public class Fire : Sprite, IRetroActor
{
    private const int Speed = 1;
    private readonly int[] _player = [16, 17];
    private readonly int[] _enemy = [18, 19];
    private readonly int[] _currentAnimation;
    private int _currentAnimationIndex;
    private readonly bool _faceRight;

    public Fire(bool faceRight, bool isEnemy)
    {
        _faceRight = faceRight;
        _currentAnimation = isEnemy ? _enemy : _player;
        _currentAnimationIndex = 0;
    }

    public void Act(ulong ticks)
    {
        if (ticks % 2 == 0)
        {
            _currentAnimationIndex++;

            if (_currentAnimationIndex > 1)
                _currentAnimationIndex = 0;
        }

        if (_faceRight)
            X += Speed;
        else
            X -= Speed;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Game1.CharactersTexture.Draw(spriteBatch, _currentAnimation[_currentAnimationIndex], (int)X, (int)Y, ColorPalette.White);
    }

    public bool IsDead =>
        X < -25 || X > 639;
}