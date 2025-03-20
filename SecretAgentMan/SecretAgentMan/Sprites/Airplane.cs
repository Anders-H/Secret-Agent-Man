using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using RetroGame.Scene;
using RetroGame.Sprites;

namespace SecretAgentMan.Sprites;

public class Airplane : Sprite, IRetroActor
{
    private readonly ulong _speed;
    private bool _faceRight;
    private int _cellIndex;

    public Airplane()
    {
        _cellIndex = -1;
        _speed = (ulong)Game1.Random.Next(4, 14);
        _faceRight = Game1.Random.Next(0, 2) == 0;
        X = Game1.Random.Next(-300, 940);
        Y = Game1.Random.Next(0, 80);
    }

    public void Act(ulong ticks)
    {
        if (ticks % _speed == 0)
        {
            _cellIndex++;

            if (_cellIndex > 24)
                _cellIndex = 0;

            if (_faceRight)
            {
                X++;

                if (X > 940)
                    _faceRight = false;
            }
            else
            {
                X--;

                if (X < -300)
                    _faceRight = true;
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (_faceRight)
            Game1.AirplaneRightTexture?.Draw(spriteBatch, _cellIndex, IntX, IntY, ColorPalette.White);
        else
            Game1.AirplaneLeftTexture?.Draw(spriteBatch, _cellIndex, IntX, IntY, ColorPalette.White);
    }
}