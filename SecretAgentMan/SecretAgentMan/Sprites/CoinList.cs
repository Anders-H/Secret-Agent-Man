using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace SecretAgentMan.Sprites;

public class CoinList : List<Coin>
{
    public void CreateBonusRoundSquare()
    {
        var coinY = 46;

        for (var y = 0; y < 15; y++)
        {
            var coinX = 206;

            for (var x = 0; x < 15; x++)
            {
                Add(new Coin(coinX, coinY, Count % 6));
                coinX += 15;
            }

            coinY += 15;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var coin in this)
            coin.Draw(spriteBatch);
    }
}