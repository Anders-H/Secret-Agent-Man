using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using RetroGame.Scene;
using RetroGame.Sprites;
using SecretAgentMan.OtherResources;

namespace SecretAgentMan.Sprites;

public class Fire : Sprite, IRetroActor
{
    private const int Speed = 3;
    private readonly int[] _player = [16, 17];
    private readonly int[] _enemy = [18, 19];
    private readonly int[] _currentAnimation;
    private int _currentAnimationIndex;
    private readonly bool _faceRight;

    public Fire(bool faceRight, bool isEnemy, int x, int y)
    {
        X = x;
        Y = y;
        _faceRight = faceRight;
        _currentAnimation = isEnemy ? _enemy : _player;
        _currentAnimationIndex = 0;
    }

    public void Act(ulong ticks)
    {
        if (ticks % 4 == 0)
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
        IngameBackgroundResources.CharactersTexture?.Draw(spriteBatch, _currentAnimation[_currentAnimationIndex], IntX, IntY, ColorPalette.White);
    }

    public bool IsDead =>
        X < -25 || X > 639;
}