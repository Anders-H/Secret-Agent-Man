using SecretAgentMan.OtherResources;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace SecretAgentMan.Sprites;

public class AmmoBoxList : List<AmmoBox>
{
    public void Act(Player player)
    {
        if (player.AmmoBoxes >= 4)
            return;
        
        foreach (var ammo in this)
        {
            if (!ammo.Collide(player))
                continue;

            SoundEffects.AmmoBox!.PlayNext();
            Remove(ammo);

            if (player.BulletsLeft <= 0 && player.AmmoBoxes <= 0)
            {
                player.BulletsLeft = Player.MaxBullets;
            }
            else
            {
                player.AmmoBoxes++;
            }

            break;
        }
    }

    public void DrawPanel(Player player, SpriteBatch spriteBatch)
    {
        for (var i = 0; i < player.AmmoBoxes; i++)
            AmmoBox.AmmoBoxTexture!.Draw(spriteBatch, 0, 543 + i * 21, 335);
    }
}
