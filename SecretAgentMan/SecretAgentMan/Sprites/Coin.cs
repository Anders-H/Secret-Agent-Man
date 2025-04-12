using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using RetroGame.Scene;
using RetroGame.Sprites;

namespace SecretAgentMan.Sprites;

public class Coin : Sprite, IRetroActor
{
    private int _cellIndex;

    public Coin(int x, int y, int cellIndexStart)
    {
        _cellIndex = cellIndexStart;
        X = x;
        Y = y;
    }

    public void Act(ulong ticks)
    {
        if (ticks % 6 == 0)
        {
            _cellIndex++;

            if (_cellIndex > 5)
                _cellIndex = 0;
        }
    }

    public bool Collide(Sprite sprite)
    {
        var coinX = sprite.X + 18;
        var coinY = sprite.Y + 18;
        var xLimitLeft = IntX + 3;
        var xLimitRight = IntX + 21;

        if (coinX < xLimitLeft || coinX > xLimitRight)
            return false;

        if (coinY < (IntY - 5) || coinY > IntY + 25)
            return false;

        return true;
    }

    public void Draw(SpriteBatch spriteBatch) =>
        Game1.CoinTexture?.Draw(spriteBatch, _cellIndex, IntX, IntY, ColorPalette.White);
}