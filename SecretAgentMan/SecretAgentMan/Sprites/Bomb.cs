using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using RetroGame.RetroTextures;
using RetroGame.Scene;
using RetroGame.Sprites;

namespace SecretAgentMan.Sprites;

public class Bomb : Sprite, IRetroActor
{
    public int State { get; set; }
    public const int StateWaiting = 0;
    public const int StateExploding = 1;
    public const int StateGone = 2;
    public const int FirstDeadlyCell = 52;
    public const int LastIndex = 57;
    public const int CellWidth = 40;
    public const int CellHeight = 31;
    public const int CellCount = 58;
    public int CellIndex { get; set; }
    public GameEventPointer Fiered { get; }
    public static RetroTexture? BombTexture { get; set; }

    public Bomb(int x, int y)
    {
        CellIndex = 0;
        X = x;
        Y = y;
        Fiered = new GameEventPointer();
    }

    public void Act(ulong ticks)
    {
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (CellIndex <= 31)
            BombTexture?.Draw(spriteBatch, CellIndex, IntX, IntY, ColorPalette.White);
    }

    public static void LoadContent(GraphicsDevice graphicsDevice, ContentManager content) =>
        BombTexture = RetroTexture.LoadContent(graphicsDevice, content, CellWidth, CellHeight, CellCount, "bomb40x31");
}