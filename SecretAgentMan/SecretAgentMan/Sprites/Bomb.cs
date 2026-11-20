using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using RetroGame.RetroTextures;
using RetroGame.Scene;
using RetroGame.Sprites;

namespace SecretAgentMan.Sprites;

public class Bomb : Sprite, IRetroActor
{
    private readonly ulong _createdAt;
    private int _flashCounter;
    public const int FirstDeadlyCell = 52;
    public const int LastIndex = 57;
    public const int CellWidth = 40;
    public const int CellHeight = 31;
    public const int CellCount = 58;
    public int CellIndex { get; set; }
    public static RetroTexture? BombTexture { get; set; }
    public bool FlashState { get; set; }
    public bool IsFinished => CellIndex >= CellCount || (FlashState && _flashCounter >= 3);

    public Bomb(int x, int y, ulong ticks)
    {
        CellIndex = 0;
        X = x;
        Y = y;
        _createdAt = ticks;
        _flashCounter = 0;
        FlashState = false;
    }

    public void Act(ulong ticks)
    {
        if (FlashState)
        {
            _flashCounter++;
            return;
        }
        if (_createdAt + 20 >= ticks)
            return;

        if (ticks % 6 == 0)
            CellIndex++;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (FlashState)
        {
            BombTexture?.Draw(spriteBatch, FirstDeadlyCell, IntX, IntY, ColorPalette.White);
            return;
        }

        if (CellIndex <= LastIndex)
            BombTexture?.Draw(spriteBatch, CellIndex, IntX, IntY, ColorPalette.White);
    }

    public static void LoadContent(GraphicsDevice graphicsDevice, ContentManager content) =>
        BombTexture = RetroTexture.LoadContent(graphicsDevice, content, CellWidth, CellHeight, CellCount, "bomb40x31");

    public bool Collide(Fire? fire) =>
        !FlashState
            && ((fire != null)
            && (fire.X > X + 1 && fire.X < X + 15 && fire.Y > Y + 5 && fire.Y < Y + 20));
}