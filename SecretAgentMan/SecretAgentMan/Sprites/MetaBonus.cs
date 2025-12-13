using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using RetroGame.Text;

namespace SecretAgentMan.Sprites;

public class MetaBonus
{
    private const int YStart = 310;
    private int _currentY;
    private ulong _lastInformationChangeAt;
    private string _info;
    private bool _isVisible;
    private const int X = 517;
    public short CurrentBonusLevel { get; private set; }
    public ulong BonusReached52At { get; set; }

    public MetaBonus()
    {
        _currentY = YStart;
        _lastInformationChangeAt = 0;
        _isVisible = false;
        _info = "";
        CurrentBonusLevel = 0;
        BonusReached52At = 0;
    }

    public void IncreaseBonus(ulong ticks, short amount)
    {
        CurrentBonusLevel += amount;
        SetBonusChange(ticks, $"+{amount}");
    }

    public void DecreaseBonus(ulong ticks, short amount)
    {
        CurrentBonusLevel -= amount;

        if (CurrentBonusLevel < 0)
            CurrentBonusLevel = 0;

        SetBonusChange(ticks, $"-{amount}");
    }

    public int GetBonusGageSpriteFrameIndex()
    {
        if (CurrentBonusLevel <= 0)
            return 0;

        var frame = CurrentBonusLevel / 2;

        frame = frame switch
        {
            < 0 => 0,
            > 25 => 25,
            _ => frame
        };

        return frame;
    }

    public void Reset()
    {
        BonusReached52At = 0;
        CurrentBonusLevel = 0;
    }

    public void Update(ulong ticks)
    {
        _isVisible = ticks - _lastInformationChangeAt < 200;

        if (ticks % 6 == 0)
            _currentY--;
    }

    public void Draw(ulong ticks, SpriteBatch spriteBatch, TextBlock textBlock)
    {
        if (!_isVisible)
            return;

        var color = ticks % 8 == 0 ? ColorPalette.White : ColorPalette.Green;
        textBlock.DirectDraw(spriteBatch, X, _currentY, _info, color);
    }

    private void SetBonusChange(ulong ticks, string changeString)
    {
        _lastInformationChangeAt = ticks;
        _currentY = YStart;
        _info = changeString;
    }
}