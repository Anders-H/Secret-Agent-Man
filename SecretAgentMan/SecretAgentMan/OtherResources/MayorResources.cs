using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RetroGame.RetroTextures;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace SecretAgentMan.OtherResources;

public static class MayorResources
{
    private static CurrentMayorSequence _currentMayorSequence;
    private static int _currentMayorSequenceIndex;
    private static int _talkCell;
    public static RetroTexture? MayorTexture { get; set; }
    public static RetroTexture? MayorStaticTexture { get; set; }
    private static RetroTexture? _currentTexture;
    private const int X = 14;
    private const int Y = 297;

    public static void LoadContent(GraphicsDevice graphics, ContentManager content)
    {
        _currentMayorSequence = CurrentMayorSequence.None;
        MayorTexture = RetroTexture.LoadContent(graphics, content, 50, 50, 4, "mayor50x50");
        MayorStaticTexture = RetroTexture.LoadContent(graphics, content, 50, 50, 24, "static_mayor");
        _currentTexture = MayorStaticTexture;
    }

    public static void SaySpyKilled(int killedSpyCount, int scoreAdded)
    {
        switch (killedSpyCount)
        {
            case 1:
                Game1.TypeWriter.SetText($"first spy eliminated! {scoreAdded} points!");
                break;
            case 2:
                Game1.TypeWriter.SetText($"second spy eliminated! {scoreAdded} points!");
                break;
            case 3:
                Game1.TypeWriter.SetText($"third spy eliminated! {scoreAdded} points!");
                break;
            case 4:
                Game1.TypeWriter.SetText($"fourth spy eliminated! {scoreAdded} points!");
                break;
            case 5:
                Game1.TypeWriter.SetText($"fifth spy eliminated! {scoreAdded} points!");
                break;
            default:
                Game1.TypeWriter.SetText($"spy number {killedSpyCount} eliminated! {scoreAdded} points!");
                break;
        }
    }

    public static void DoShortTalk()
    {
        _currentMayorSequence = CurrentMayorSequence.ShortTalk;
        _talkCell = 0;
    }

    public static void DoShortTalkAngry()
    {
        _currentMayorSequence = CurrentMayorSequence.ShortTalkAngry;
        _talkCell = 0;
    }

    public static void DoLongTalk()
    {

    }

    public static void Act(ulong ticks)
    {
        switch (_currentMayorSequence)
        {
            case CurrentMayorSequence.None:

                if (ticks % 6 == 0)
                {
                    _currentMayorSequenceIndex++;

                    if (_currentMayorSequenceIndex > 7)
                        _currentMayorSequenceIndex = 0;
                }

                break;
            case CurrentMayorSequence.ShortTalk:

                if (ticks % 8 == 0)
                {
                    switch (_talkCell)
                    {
                        case 0:
                            _currentTexture = MayorStaticTexture;
                            _currentMayorSequenceIndex = 8;
                            break;
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                            _currentMayorSequenceIndex = 8 + _talkCell;
                            break;
                        case 16:
                            _currentTexture = MayorTexture;
                            _currentMayorSequenceIndex = 0;
                            break;
                        case 17:
                            _currentMayorSequenceIndex = 0;
                            break;
                        case 18:
                        case 19:
                            _currentMayorSequenceIndex = 1;
                            break;
                        case 20:
                        case 21:
                            _currentMayorSequenceIndex = 0;
                            break;
                        case 22:
                        case 23:
                            _currentMayorSequenceIndex = 1;
                            break;
                        case 24:
                        case 25:
                            _currentMayorSequenceIndex = 0;
                            break;
                        case 26:
                        case 27:
                            _currentMayorSequenceIndex = 1;
                            break;
                        case 28:
                        case 29:
                            _currentMayorSequenceIndex = 0;
                            break;
                        default:
                            _currentTexture = MayorStaticTexture;
                            _currentMayorSequenceIndex = 0;
                            _currentMayorSequence = CurrentMayorSequence.None;
                            break;
                    }

                    _talkCell++;
                }

                break;
            case CurrentMayorSequence.ShortTalkAngry:

                if (ticks % 8 == 0)
                {
                    switch (_talkCell)
                    {
                        case 0:
                            _currentTexture = MayorStaticTexture;
                            _currentMayorSequenceIndex = 8;
                            break;
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                            _currentMayorSequenceIndex = 8 + _talkCell;
                            break;
                        case 16:
                            _currentTexture = MayorTexture;
                            _currentMayorSequenceIndex = 0;
                            break;
                        case 17:
                            _currentMayorSequenceIndex = 0;
                            break;
                        case 18:
                        case 19:
                            _currentMayorSequenceIndex = 1;
                            break;
                        case 20:
                        case 21:
                            _currentMayorSequenceIndex = 0;
                            break;
                        case 22:
                        case 24:
                        case 26:
                        case 28:
                        case 30:
                            _currentMayorSequenceIndex = 2;
                            break;
                        case 23:
                        case 25:
                        case 27:
                        case 29:
                        case 31:
                            _currentMayorSequenceIndex = 3;
                            break;
                        default:
                            _currentTexture = MayorStaticTexture;
                            _currentMayorSequenceIndex = 0;
                            _currentMayorSequence = CurrentMayorSequence.None;
                            break;
                    }

                    _talkCell++;
                }

                break;
            case CurrentMayorSequence.LongTalk:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public static void Draw(SpriteBatch spriteBatch)
    {
        _currentTexture!.Draw(spriteBatch, _currentMayorSequenceIndex, X, Y);
    }
}