using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using RetroGame.Scene;
using RetroGame.Text;

namespace SecretAgentMan.OtherResources;

public class MessageSystem : IRetroActor
{
    private readonly TextBlock _textBlock;
    private const int YStart = 59;
    private readonly List<Message> _messages;
    private int _currentMayorCell;
    private bool _isAngry;

    public MessageSystem()
    {
        _isAngry = false;
        _currentMayorCell = 0;
        _textBlock = new TextBlock(CharacterSet.Uppercase);
        _messages = new List<Message>();
    }

    public void AddMessage(string message, bool isAngry)
    {
        _isAngry = isAngry;
        _messages.Add(new Message(message));
    }

    public void Act(ulong ticks)
    {
        foreach (var message in _messages)
        {
            if (message.IsDead)
            {
                _messages.Remove(message);
                return;
            }
        }

        foreach (var message in _messages)
        {
            switch (_messages.Count)
            {
                case 1:
                case 2:
                case 3:
                    message.X--;
                    break;
                case 4:
                case 5:
                    message.X -= 2;
                    break;
                case 6:
                    message.X -= 3;
                    break;
                case 7:
                    message.X -= 4;
                    break;
                case 8:
                    message.X -= 5;
                    break;
                case 9:
                    message.X -= 6;
                    break;
                case 10:
                    message.X -= 7;
                    break;
                default:
                    message.X -= 8;
                    break;
            }
        }

        if (_isAngry)
        {
            if (ticks % 5 == 0)
                _currentMayorCell = _currentMayorCell != 2 ? 2 : 3;
        }
        else
        {
            if (ticks % 20 == 0)
                _currentMayorCell = _currentMayorCell != 0 ? 0 : 1;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var y = YStart;

        if (MayorVisible())
            MayorResources.MayorTexture?.Draw(spriteBatch, _currentMayorCell, 582, 8, ColorPalette.White);

        foreach (var message in _messages)
        {
            _textBlock.DirectDraw(spriteBatch, message.X, y, message.Text, ColorPalette.White);
            var messageEndX = message.X + (message.Text.Length + 3) * 8;

            if (messageEndX > 50)
                y += 8;
        }
    }

    private bool MayorVisible() =>
        _messages.Any(x => x.X > 100);
}

public class Message
{
    public int X { get; set; }
    public string Text { get; }
    public int DeadAtX { get; }

    public Message(string text)
    {
        X = 640;
        Text = text;
        DeadAtX = -text.Length * 8;
    }

    public bool IsDead =>
        X < DeadAtX;
}