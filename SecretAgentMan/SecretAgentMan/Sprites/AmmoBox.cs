using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using RetroGame.RetroTextures;
using RetroGame.Scene;
using RetroGame.Sprites;

namespace SecretAgentMan.Sprites;

public class AmmoBox : Sprite, IRetroActor
{
    public static RetroTexture? AmmoBoxTexture { get; set; }

    public AmmoBox(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
    {
        AmmoBoxTexture = RetroTexture.LoadContent(graphicsDevice, content, 20, 11, 1, "ammobox16x11");
    }

    public void Act(ulong ticks)
    {
    }

    public bool Collide(Sprite sprite)
    {
        var ammoBoxX = sprite.X + 18;
        var ammoBoxY = sprite.Y + 18;
        var xLimitLeft = IntX + 1;
        var xLimitRight = IntX + 30;

        if (ammoBoxX < xLimitLeft || ammoBoxX > xLimitRight)
            return false;

        if (ammoBoxY < (IntY - 5) || ammoBoxY > IntY + 25)
            return false;

        return true;
    }

    public void Draw(SpriteBatch spriteBatch) =>
        AmmoBoxTexture?.Draw(spriteBatch, 0, IntX, IntY, ColorPalette.White);

}