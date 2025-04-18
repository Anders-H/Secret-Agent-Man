using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SecretAgentMan.Scenes;

public class BonusLevelScene : RetroGame.Scene.IngameScene
{
    private readonly DateTime _bonusLevelStartTime;

    public BonusLevelScene(RetroGame.RetroGame parent) : base(parent)
    {
        _bonusLevelStartTime = DateTime.Now;
    }

    public override void Update(GameTime gameTime, ulong ticks)
    {
        if (DateTime.Now.Subtract(_bonusLevelStartTime).TotalSeconds >= Game1.BonusRoundSeconds)
        {
            Parent.CurrentScene = Game1.CurrentIngameScene;
            return;
        }
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {

        Game1.Frame!.Draw(spriteBatch, 0, 0, 0);
        Game1.BonusLevelFrame!.Draw(spriteBatch, ticks % 20 < 10 ? 0 : 1, 0, 0);
        base.Draw(gameTime, ticks, spriteBatch);
    }
}