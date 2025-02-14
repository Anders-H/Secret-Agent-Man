using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using RetroGame.Scene;
using RetroGame.Text;
using SharpDX.X3DAudio;

namespace SecretAgentMan;

public class MessageSystem : IRetroActor
{
    private readonly TextBlock _textBlock;
    private const int YStart = 8;
    private readonly List<Message> _messages;

    public MessageSystem()
    {
        _textBlock = new TextBlock(CharacterSet.Uppercase);
        _messages = new List<Message>();
    }

    public void AddMessage(string message) =>
        _messages.Add(new Message(message));

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
            message.X--;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var y = YStart;

        foreach (var message in _messages)
        {
            _textBlock.DirectDraw(spriteBatch, message.X, y, message.Text, ColorPalette.White);
            y += 8;
        }
    }
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