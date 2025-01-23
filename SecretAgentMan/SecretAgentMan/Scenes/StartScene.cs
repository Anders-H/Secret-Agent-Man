using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RetroGame;
using RetroGame.Input;
using RetroGame.Scene;
using RetroGame.Text;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace SecretAgentMan.Scenes;

public class StartScene : Scene
{
    private KeyboardStateChecker Keyboard { get; }
    private TextBlock LogoTextBlock { get; }

    public StartScene(RetroGame.RetroGame parent) : base(parent)
    {
        Keyboard = new KeyboardStateChecker();
        const string logo = "SECRET AGENT MAN";
        LogoTextBlock = new TextBlock(logo.Length, 1, CharacterSet.Lowercase);
        AddToAutoUpdate(Keyboard);
    }

    public override void Update(GameTime gameTime, ulong ticks)
    {
        if (Keyboard.IsKeyPressed(Keys.Escape))
            Exit();
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        LogoTextBlock.Draw(spriteBatch, ColorPalette.White);
        base.Draw(gameTime, ticks, spriteBatch);
    }
}