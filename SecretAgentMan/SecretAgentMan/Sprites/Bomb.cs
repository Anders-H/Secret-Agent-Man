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
    public const int FirstDeadlyCell = 26;
    public int CellIndex { get; set; }
    public static RetroTexture? BombTexture { get; set; }

    public Bomb()
    {
        CellIndex = 0;
    }

    public void Act(ulong ticks)
    {
    }

    public void Draw(SpriteBatch spriteBatch) =>
        BombTexture?.Draw(spriteBatch, CellIndex, IntX, IntY, ColorPalette.White);

    public static void LoadContent(GraphicsDevice graphicsDevice, ContentManager content) =>
        BombTexture = RetroTexture.LoadContent(graphicsDevice, content, 13, 10, 4, "bomb40x31");
}