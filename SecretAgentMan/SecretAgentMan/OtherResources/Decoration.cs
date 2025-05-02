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
                IngameBackgroundResources.BackgroundLayer01!.Draw(spriteBatch, 0, 0, 0, Flip.FlipLeftRight);
                IngameBackgroundResources.BackgroundLayer02!.Draw(spriteBatch, 3, 0, 0, Flip.FlipLeftRight);
                IngameBackgroundResources.BackgroundLayer03!.Draw(spriteBatch, 0, 0, 0, Flip.FlipLeftRight);
                IngameBackgroundResources.BackgroundLayer04!.Draw(spriteBatch, 3, 0, 0, Flip.FlipLeftRight);
                break;
            case 1:
                IngameBackgroundResources.BackgroundLayer01!.Draw(spriteBatch, 2, 0, 0, Flip.FlipLeftRight);
                IngameBackgroundResources.BackgroundLayer02!.Draw(spriteBatch, 2, 0, 0, Flip.FlipLeftRight);
                IngameBackgroundResources.BackgroundLayer03!.Draw(spriteBatch, 1, 0, 0, Flip.FlipLeftRight);
                IngameBackgroundResources.BackgroundLayer04!.Draw(spriteBatch, 2, 0, 0, Flip.FlipLeftRight);
                break;
            case 2:
                IngameBackgroundResources.BackgroundLayer01!.Draw(spriteBatch, 3, 0, 0, Flip.FlipLeftRight);
                IngameBackgroundResources.BackgroundLayer02!.Draw(spriteBatch, 1, 0, 0, Flip.FlipLeftRight);
                IngameBackgroundResources.BackgroundLayer03!.Draw(spriteBatch, 2, 0, 0, Flip.FlipLeftRight);
                IngameBackgroundResources.BackgroundLayer04!.Draw(spriteBatch, 1, 0, 0, Flip.FlipLeftRight);
                break;
            case 3:
                IngameBackgroundResources.BackgroundLayer01!.Draw(spriteBatch, 1, 0, 0);
                IngameBackgroundResources.BackgroundLayer02!.Draw(spriteBatch, 3, 0, 0);
                IngameBackgroundResources.BackgroundLayer03!.Draw(spriteBatch, 0, 0, 0);
                IngameBackgroundResources.BackgroundLayer04!.Draw(spriteBatch, 2, 0, 0);
                break;
            case 4:
                IngameBackgroundResources.BackgroundLayer01!.Draw(spriteBatch, 1, 0, 0);
                IngameBackgroundResources.BackgroundLayer02!.Draw(spriteBatch, 2, 0, 0);
                IngameBackgroundResources.BackgroundLayer03!.Draw(spriteBatch, 1, 0, 0);
                IngameBackgroundResources.BackgroundLayer04!.Draw(spriteBatch, 3, 0, 0);
                break;
            case 5:
                IngameBackgroundResources.BackgroundLayer01!.Draw(spriteBatch, 0, 0, 0);
                IngameBackgroundResources.BackgroundLayer02!.Draw(spriteBatch, 1, 0, 0);
                IngameBackgroundResources.BackgroundLayer03!.Draw(spriteBatch, 2, 0, 0);
                IngameBackgroundResources.BackgroundLayer04!.Draw(spriteBatch, 1, 0, 0);
                break;
            case 6:
                IngameBackgroundResources.BackgroundLayer01!.Draw(spriteBatch, 2, 0, 0);
                IngameBackgroundResources.BackgroundLayer02!.Draw(spriteBatch, 0, 0, 0);
                IngameBackgroundResources.BackgroundLayer03!.Draw(spriteBatch, 3, 0, 0);
                IngameBackgroundResources.BackgroundLayer04!.Draw(spriteBatch, 0, 0, 0);
                break;
            case 7:
                IngameBackgroundResources.BackgroundLayer01!.Draw(spriteBatch, 3, 0, 0);
                IngameBackgroundResources.BackgroundLayer02!.Draw(spriteBatch, 1, 0, 0);
                IngameBackgroundResources.BackgroundLayer03!.Draw(spriteBatch, 0, 0, 0);
                IngameBackgroundResources.BackgroundLayer04!.Draw(spriteBatch, 1, 0, 0);
                break;
            case 8:
                IngameBackgroundResources.BackgroundLayer01!.Draw(spriteBatch, 0, 0, 0);
                IngameBackgroundResources.BackgroundLayer02!.Draw(spriteBatch, 2, 0, 0);
                IngameBackgroundResources.BackgroundLayer03!.Draw(spriteBatch, 1, 0, 0);
                IngameBackgroundResources.BackgroundLayer04!.Draw(spriteBatch, 2, 0, 0);
                break;
            case 9:
                IngameBackgroundResources.BackgroundLayer01!.Draw(spriteBatch, 2, 0, 0);
                IngameBackgroundResources.BackgroundLayer02!.Draw(spriteBatch, 3, 0, 0);
                IngameBackgroundResources.BackgroundLayer03!.Draw(spriteBatch, 2, 0, 0);
                IngameBackgroundResources.BackgroundLayer04!.Draw(spriteBatch, 3, 0, 0);
                break;
        }
    }
}