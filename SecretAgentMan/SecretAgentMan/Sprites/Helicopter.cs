using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using RetroGame.RetroTextures;
using RetroGame.Scene;
using RetroGame.Sprites;

namespace SecretAgentMan.Sprites;

public class Helicopter : Sprite, IRetroActor
{
    public enum HelicopterStatus
    {
        None,
        MovingIn,
        Waiting,
        MovingOut,
    }

    private const int TargetX = 200;
    private const int TargetY = 10;
    private const int SpeedX = 2;
    private const int SpeedY = 1;
    private int _cellIndex = 0;
    private static RetroTexture? HeliLadderTexture { get; set; }
    public ulong ReachedTargetAt { get; private set; }
    public HelicopterStatus Status { get; set; }

    public Helicopter()
    {
        X = TargetX - 400;
        Y = TargetY - 100;
        Status = HelicopterStatus.None;
        ReachedTargetAt = 0;
    }

    public static void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
    {
        HeliLadderTexture = RetroTexture.LoadContent(graphicsDevice, content, 248, 212, 8, "heliladder248x212");
    }

    public void Act(ulong ticks)
    {
        if (ticks % 4== 0)
        {
            _cellIndex++;

            if (_cellIndex >= HeliLadderTexture!.CellCount)
                _cellIndex = 0;
        }

        switch (Status)
        {
            case HelicopterStatus.None:
                break;
            case HelicopterStatus.MovingIn:
                X += SpeedX;

                if (ticks % 2 == 0)
                    Y += SpeedY;

                if (X >= TargetX && Y >= TargetY)
                {
                    ReachedTargetAt = ticks;
                    Status = HelicopterStatus.Waiting;
                }

                break;
            case HelicopterStatus.Waiting:
                break;
            case HelicopterStatus.MovingOut:
                X += SpeedX;

                if (ticks % 2 == 0)
                    Y -= SpeedY;

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (Status == HelicopterStatus.None)
            return;

        HeliLadderTexture?.Draw(spriteBatch, _cellIndex, IntX, IntY, ColorPalette.White);
    }
}