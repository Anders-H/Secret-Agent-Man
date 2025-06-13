using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using RetroGame;
using RetroGame.Input;
using RetroGame.Scene;
using RetroGame.Text;
using SecretAgentMan.OtherResources;

namespace SecretAgentMan.Scenes.IntroductionScenes;

public class IntroScene : Scene
{
    private const int CellCountX = 40;
    private const int CellCountY = 23;
    private const int TicksBeforeContinue = 3500;
    private int _currentCellX;
    private int _currentCellY;
    private int _inCurrentCellY;
    private bool _done;
    private KeyboardStateChecker Keyboard { get; }
    private const string LoadingPleaseWait = "loading, please wait...";
    private readonly int _loadingPleaseWaitCenterX;
    private const string PressFire = "press fire to continue";
    private readonly int _pressFireCenterX;
    private readonly TextBlock _textBlock;
    private bool _canContinue;

    public IntroScene(RetroGame.RetroGame parent) : base(parent)
    {
        Keyboard = new KeyboardStateChecker();
        _pressFireCenterX = 320 - PressFire.Length * 8 / 2;
        _loadingPleaseWaitCenterX = 320 - LoadingPleaseWait.Length * 8 / 2;
        _textBlock = new TextBlock(CharacterSet.Uppercase);
        AddToAutoUpdate(Keyboard);
        MediaPlayer.Play(Songs.LoaderSong!);
    }

    public override void Update(GameTime gameTime, ulong ticks)
    {
        _inCurrentCellY += 6;

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

        _canContinue = ticks > TicksBeforeContinue;

        if (Keyboard.IsKeyPressed(Keys.Escape))
            Exit();
        else if (_done || (_canContinue && Keyboard.IsFirePressed()))
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

        if (ticks % 80 < 40)
        {
            if (ticks > 600)
            {
                if (_done) //(_canContinue)
                    _textBlock.DirectDraw(spriteBatch, _pressFireCenterX, 300, PressFire, ColorPalette.White);
                else
                    _textBlock.DirectDraw(spriteBatch, _loadingPleaseWaitCenterX, 300, LoadingPleaseWait, ColorPalette.White);
            }
        }

        base.Draw(gameTime, ticks, spriteBatch);
    }
}