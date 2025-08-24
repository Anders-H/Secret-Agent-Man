using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using RetroGame.RetroTextures;
using RetroGame.Scene;
using RetroGame.Sprites;

namespace SecretAgentMan.Sprites;

public class Briefcase : Sprite, IRetroActor
{
    private readonly int _color;
    public const int Silver = 0;
    public const int Blue = 1;
    public const int Red = 2;
    public const int Brown = 3;
    public static RetroTexture? BriefcaseTexture { get; set; }
    public static string[] BriefcaseWords { get; } = ["attache case", "briefcase", "document case", "portfolio"];
    public static string[] BriefcaseColors { get; } = ["silver", "blue", "red", "brown"];

    public Briefcase(int color, int x, int y)
    {
        _color = color;
        X = x;
        Y = y;
    }

    public static void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
    {
        BriefcaseTexture = RetroTexture.LoadContent(graphicsDevice, content, 13, 10, 4, "briefcase13x10");
    }

    public void Act(ulong ticks)
    {
    }

    public bool Collide(Sprite sprite)
    {
        var briefcaseX = sprite.X + 18;
        var briefcaseY = sprite.Y + 18;
        var xLimitLeft = IntX + 1;
        var xLimitRight = IntX + 30;

        if (briefcaseX < xLimitLeft || briefcaseX > xLimitRight)
            return false;

        if (briefcaseY < (IntY - 5) || briefcaseY > IntY + 25)
            return false;

        return true;
    }

    public void Draw(SpriteBatch spriteBatch) =>
        BriefcaseTexture?.Draw(spriteBatch, 0, IntX, IntY, ColorPalette.White);
}