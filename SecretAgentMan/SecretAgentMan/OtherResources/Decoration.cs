using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using RetroGame.Sprites;

namespace SecretAgentMan.OtherResources;

public class Decoration : Sprite
{
    public void Draw(SpriteBatch spriteBatch, int roomIndex)
    {
        switch (roomIndex)
        {
            case 3:
                Game1.BackgroundLayer01!.Draw(spriteBatch, 1, 0, 0, ColorPalette.White);
                Game1.BackgroundLayer02!.Draw(spriteBatch, 3, 0, 0, ColorPalette.White);
                Game1.BackgroundLayer03!.Draw(spriteBatch, 0, 0, 0, ColorPalette.White);
                Game1.BackgroundLayer04!.Draw(spriteBatch, 2, 0, 0, ColorPalette.White);
                break;
            case 4:
                Game1.BackgroundLayer01!.Draw(spriteBatch, 1, 0, 0, ColorPalette.White);
                Game1.BackgroundLayer02!.Draw(spriteBatch, 2, 0, 0, ColorPalette.White);
                Game1.BackgroundLayer03!.Draw(spriteBatch, 1, 0, 0, ColorPalette.White);
                Game1.BackgroundLayer04!.Draw(spriteBatch, 3, 0, 0, ColorPalette.White);
                break;
            case 5:
                Game1.BackgroundLayer01!.Draw(spriteBatch, 0, 0, 0, ColorPalette.White);
                Game1.BackgroundLayer02!.Draw(spriteBatch, 1, 0, 0, ColorPalette.White);
                Game1.BackgroundLayer03!.Draw(spriteBatch, 2, 0, 0, ColorPalette.White);
                Game1.BackgroundLayer04!.Draw(spriteBatch, 1, 0, 0, ColorPalette.White);
                break;
            case 6:
                Game1.BackgroundLayer01!.Draw(spriteBatch, 2, 0, 0, ColorPalette.White);
                Game1.BackgroundLayer02!.Draw(spriteBatch, 0, 0, 0, ColorPalette.White);
                Game1.BackgroundLayer03!.Draw(spriteBatch, 3, 0, 0, ColorPalette.White);
                Game1.BackgroundLayer04!.Draw(spriteBatch, 0, 0, 0, ColorPalette.White);
                break;
            case 7:
                Game1.BackgroundLayer01!.Draw(spriteBatch, 3, 0, 0, ColorPalette.White);
                Game1.BackgroundLayer02!.Draw(spriteBatch, 1, 0, 0, ColorPalette.White);
                Game1.BackgroundLayer03!.Draw(spriteBatch, 0, 0, 0, ColorPalette.White);
                Game1.BackgroundLayer04!.Draw(spriteBatch, 1, 0, 0, ColorPalette.White);
                break;
            case 8:
                Game1.BackgroundLayer01!.Draw(spriteBatch, 0, 0, 0, ColorPalette.White);
                Game1.BackgroundLayer02!.Draw(spriteBatch, 2, 0, 0, ColorPalette.White);
                Game1.BackgroundLayer03!.Draw(spriteBatch, 1, 0, 0, ColorPalette.White);
                Game1.BackgroundLayer04!.Draw(spriteBatch, 2, 0, 0, ColorPalette.White);
                break;
            case 9:
                Game1.BackgroundLayer01!.Draw(spriteBatch, 2, 0, 0, ColorPalette.White);
                Game1.BackgroundLayer02!.Draw(spriteBatch, 3, 0, 0, ColorPalette.White);
                Game1.BackgroundLayer03!.Draw(spriteBatch, 2, 0, 0, ColorPalette.White);
                Game1.BackgroundLayer04!.Draw(spriteBatch, 3, 0, 0, ColorPalette.White);
                break;
        }
    }
}