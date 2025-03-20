using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using RetroGame.Scene;
using RetroGame.Sprites;

namespace SecretAgentMan.Sprites;

public class GraveStone : Sprite, IRetroActor, IGameFieldThings
{
    private int _cellIndex;

    public GraveStone(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void Act(ulong ticks)
    {
        if (ticks % 15 == 0 && _cellIndex < 12)
        {
            _cellIndex++;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Game1.GraveStoneTexture?.Draw(spriteBatch, _cellIndex, IntX, base.IntY, ColorPalette.White);
    }

    public new int IntY =>
        base.IntY - 10;
}