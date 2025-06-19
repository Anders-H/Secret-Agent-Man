using System.Collections.Generic;
using SecretAgentMan.OtherResources;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

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

    public bool Act(ulong ticks, Player player, List<Npc> npcs)
    {
        var hit = false;

        foreach (var coin in this)
        {
            coin.Act(ticks);

            if (coin.Collide(player))
            {
                SoundEffects.PlayerCoin!.PlayRandom();
                Remove(coin);
                hit = true;
                break;
            }

            foreach (var npc in npcs)
            {
                if (coin.Collide(npc))
                {
                    SoundEffects.EnemyCoin!.PlayRandom();
                    Remove(coin);
                    return hit;
                }
            }
        }

        return hit;
    }
}