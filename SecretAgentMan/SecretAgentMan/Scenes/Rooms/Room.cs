using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using RetroGame.Text;

namespace SecretAgentMan.Scenes.Rooms;

public class Room
{
    public string DistrictName { get; }
    public List<Npc> Npcs { get; }

    public Room(string districtName)
    {
        DistrictName = districtName;
        Npcs = [];
    }

    public void Act(ulong ticks)
    {
        foreach (var npc in Npcs)
        {
            npc.Act(ticks);
        }
    }

    public void Draw(SpriteBatch spriteBatch, TextBlock textBlock)
    {
        foreach (var npc in Npcs)
        {
            npc.Draw(spriteBatch, Game1.CharactersTexture, npc.CellIndex, Color.White);
        }

        if (Game1.Cheat)
        {
            foreach (var npc in Npcs)
            {
                switch (npc.Status)
                {
                    case Npc.STATUS_INNOCENT:
                        textBlock.DirectDraw(spriteBatch, (int)npc.X - 10, (int)npc.Y + 20, "innocent", ColorPalette.White);
                        break;
                    case Npc.STATUS_SPY_UNDETECTED:
                        textBlock.DirectDraw(spriteBatch, (int)npc.X - 10, (int)npc.Y + 20, "undetected", ColorPalette.White);
                        break;
                    case Npc.STATUS_SPY_DETECTED:
                        textBlock.DirectDraw(spriteBatch, (int)npc.X - 10, (int)npc.Y + 20, "spy!!!", ColorPalette.White);
                        break;
                }
            }
        }
    }
}