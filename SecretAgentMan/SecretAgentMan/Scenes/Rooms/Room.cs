using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using RetroGame.Text;
using SecretAgentMan.Sprites;

namespace SecretAgentMan.Scenes.Rooms;

public class Room
{
    public string DistrictName { get; }
    public List<Npc> Npcs { get; }
    public CoinList Coins { get; }
    private readonly List<Airplane> _airplanes;
    
    public Room(string districtName)
    {
        _airplanes = [];
        DistrictName = districtName;
        Npcs = [];
        Coins = [];
    }

    public void AddAirplane(int count)
    {
        for (var i = 0; i < count; i++)
            _airplanes.Add(new Airplane());
    }

    public void Act(ulong ticks)
    {
        foreach (var npc in Npcs)
            npc.Act(ticks);

        foreach (var airplane in _airplanes)
            airplane.Act(ticks);
    }

    public void Draw(SpriteBatch spriteBatch, TextBlock textBlock, Player player)
    {
        var playerIsDrawn = false;
        var lastY = IngameScene.SpriteUpperLimit - 1;

        foreach (var t in Npcs.OrderBy(x => x.IntY))
        {
            if (!playerIsDrawn && player.Y >= lastY && player.Y <= t.IntY)
            {
                player.Draw(spriteBatch, Game1.CharactersTexture, player.CellIndex, Color.White);
                playerIsDrawn = true;
            }

            t.Draw(spriteBatch);
            lastY = t.IntY;
        }

        if (!playerIsDrawn)
            player.Draw(spriteBatch, Game1.CharactersTexture, player.CellIndex, Color.White);

        if (Game1.Cheat)
        {
            foreach (var npc in Npcs)
            {
                switch (npc.Status)
                {
                    case Npc.StatusInnocent:
                        textBlock.DirectDraw(spriteBatch, npc.IntX - 10, npc.IntY + 20, "innocent", ColorPalette.White);
                        break;
                    case Npc.StatusSpyUndetected:
                        textBlock.DirectDraw(spriteBatch, npc.IntX - 10, npc.IntY + 20, "undetected", ColorPalette.White);
                        break;
                    case Npc.StatusSpyDetected:
                        textBlock.DirectDraw(spriteBatch, npc.IntX - 10, npc.IntY + 20, "spy!!!", ColorPalette.White);
                        break;
                }
            }
        }
    }

    public void DrawAirPlanes(SpriteBatch spriteBatch)
    {
        foreach (var airplane in _airplanes)
        {
            airplane.Draw(spriteBatch);
        }
    }

    public bool IsClear() =>
        Npcs.Count(x => x.Status == Npc.StatusSpyUndetected) + Npcs.Count(x => x.Status == Npc.StatusSpyDetected) <= 0;
}