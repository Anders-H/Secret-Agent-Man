using Microsoft.Xna.Framework.Graphics;
using RetroGame.RetroTextures;
using RetroGame.Sprites;

namespace SecretAgentMan.OtherResources;

public class Decoration : Sprite
{
    public void Draw(SpriteBatch spriteBatch, int roomIndex)
    {
        switch (roomIndex)
        {
            case 0:
                Game1.BackgroundLayer01!.Draw(spriteBatch, 0, 0, 0, Flip.FlipLeftRight);
                Game1.BackgroundLayer02!.Draw(spriteBatch, 3, 0, 0, Flip.FlipLeftRight);
                Game1.BackgroundLayer03!.Draw(spriteBatch, 0, 0, 0, Flip.FlipLeftRight);
                Game1.BackgroundLayer04!.Draw(spriteBatch, 3, 0, 0, Flip.FlipLeftRight);
                break;
            case 1:
                Game1.BackgroundLayer01!.Draw(spriteBatch, 2, 0, 0, Flip.FlipLeftRight);
                Game1.BackgroundLayer02!.Draw(spriteBatch, 2, 0, 0, Flip.FlipLeftRight);
                Game1.BackgroundLayer03!.Draw(spriteBatch, 1, 0, 0, Flip.FlipLeftRight);
                Game1.BackgroundLayer04!.Draw(spriteBatch, 2, 0, 0, Flip.FlipLeftRight);
                break;
            case 2:
                Game1.BackgroundLayer01!.Draw(spriteBatch, 3, 0, 0, Flip.FlipLeftRight);
                Game1.BackgroundLayer02!.Draw(spriteBatch, 1, 0, 0, Flip.FlipLeftRight);
                Game1.BackgroundLayer03!.Draw(spriteBatch, 2, 0, 0, Flip.FlipLeftRight);
                Game1.BackgroundLayer04!.Draw(spriteBatch, 1, 0, 0, Flip.FlipLeftRight);
                break;
            case 3:
                Game1.BackgroundLayer01!.Draw(spriteBatch, 1, 0, 0);
                Game1.BackgroundLayer02!.Draw(spriteBatch, 3, 0, 0);
                Game1.BackgroundLayer03!.Draw(spriteBatch, 0, 0, 0);
                Game1.BackgroundLayer04!.Draw(spriteBatch, 2, 0, 0);
                break;
            case 4:
                Game1.BackgroundLayer01!.Draw(spriteBatch, 1, 0, 0);
                Game1.BackgroundLayer02!.Draw(spriteBatch, 2, 0, 0);
                Game1.BackgroundLayer03!.Draw(spriteBatch, 1, 0, 0);
                Game1.BackgroundLayer04!.Draw(spriteBatch, 3, 0, 0);
                break;
            case 5:
                Game1.BackgroundLayer01!.Draw(spriteBatch, 0, 0, 0);
                Game1.BackgroundLayer02!.Draw(spriteBatch, 1, 0, 0);
                Game1.BackgroundLayer03!.Draw(spriteBatch, 2, 0, 0);
                Game1.BackgroundLayer04!.Draw(spriteBatch, 1, 0, 0);
                break;
            case 6:
                Game1.BackgroundLayer01!.Draw(spriteBatch, 2, 0, 0);
                Game1.BackgroundLayer02!.Draw(spriteBatch, 0, 0, 0);
                Game1.BackgroundLayer03!.Draw(spriteBatch, 3, 0, 0);
                Game1.BackgroundLayer04!.Draw(spriteBatch, 0, 0, 0);
                break;
            case 7:
                Game1.BackgroundLayer01!.Draw(spriteBatch, 3, 0, 0);
                Game1.BackgroundLayer02!.Draw(spriteBatch, 1, 0, 0);
                Game1.BackgroundLayer03!.Draw(spriteBatch, 0, 0, 0);
                Game1.BackgroundLayer04!.Draw(spriteBatch, 1, 0, 0);
                break;
            case 8:
                Game1.BackgroundLayer01!.Draw(spriteBatch, 0, 0, 0);
                Game1.BackgroundLayer02!.Draw(spriteBatch, 2, 0, 0);
                Game1.BackgroundLayer03!.Draw(spriteBatch, 1, 0, 0);
                Game1.BackgroundLayer04!.Draw(spriteBatch, 2, 0, 0);
                break;
            case 9:
                Game1.BackgroundLayer01!.Draw(spriteBatch, 2, 0, 0);
                Game1.BackgroundLayer02!.Draw(spriteBatch, 3, 0, 0);
                Game1.BackgroundLayer03!.Draw(spriteBatch, 2, 0, 0);
                Game1.BackgroundLayer04!.Draw(spriteBatch, 3, 0, 0);
                break;
        }
    }
}