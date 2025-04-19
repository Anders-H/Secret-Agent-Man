using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using RetroGame;
using RetroGame.Input;
using RetroGame.Scene;
using RetroGame.Text;

namespace SecretAgentMan.Scenes.IntroductionScenes;

public class IntroScene : Scene
{
    private const int CellCountX = 40;
    private const int CellCountY = 9;
    private const int TicksBeforeContinue = 1500;
    private int _currentCellX;
    private int _currentCellY;
    private int _inCurrentCellY;
    private bool _done;
    private KeyboardStateChecker Keyboard { get; }
    private const string pressFire = "press fire to continue";
    private readonly int pressFireCenterX;
    private readonly TextBlock _textBlock;

    public IntroScene(RetroGame.RetroGame parent) : base(parent)
    {
        Keyboard = new KeyboardStateChecker();
        pressFireCenterX = 320 - pressFire.Length * 8 / 2;
        _textBlock = new TextBlock(CharacterSet.Uppercase);
        AddToAutoUpdate(Keyboard);
        Game1.LoaderSongIsPlaying = true;
        MediaPlayer.Play(Game1.LoaderSong!);
    }

    public override void Update(GameTime gameTime, ulong ticks)
    {
        _inCurrentCellY += 2;

        if (_inCurrentCellY > 16)
        {
            _inCurrentCellY = 0;
            _currentCellX++;

            if (_currentCellX >= CellCountX)
            {
                _currentCellX = 0;
                _currentCellY++;

                if (_currentCellY >= CellCountY)
                {
                    _done = true;
                }
            }
        }

        if (RetroGame.RetroGame.CheatFileAvailable && Keyboard.IsFirePressed())
            Parent.CurrentScene = new StartScene(Parent, 0, 0);

        if (ticks == TicksBeforeContinue)
            Keyboard.ClearState();

        if (Keyboard.IsKeyPressed(Keys.Escape))
            Exit();
        else if (ticks > TicksBeforeContinue && Keyboard.IsFirePressed())
            Parent.CurrentScene = new StartScene(Parent, 0, 0);

        base.Update(gameTime, ticks);
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        if (_done)
        {
            Game1.IntroGraphics!.Draw(spriteBatch, 0, 0, 0);
        }
        else
        {
            if (_currentCellY > 0)
                Game1.IntroGraphics!.DrawPart(spriteBatch, 0, 0, 640, _currentCellY * 16, 0, 0);

            if (_currentCellX > 0)
                Game1.IntroGraphics!.DrawPart(spriteBatch, 0, _currentCellY * 16, _currentCellX * 16, 16, 0, _currentCellY * 16);

            if (_inCurrentCellY > 0)
                Game1.IntroGraphics!.DrawPart(spriteBatch, _currentCellX * 16, _currentCellY * 16, 16, _inCurrentCellY, _currentCellX * 16, _currentCellY * 16);
        }

        if (ticks > TicksBeforeContinue && ticks % 120 > 60)
            _textBlock.DirectDraw(spriteBatch, pressFireCenterX, 300, pressFire, ColorPalette.White);

        base.Draw(gameTime, ticks, spriteBatch);
    }
}