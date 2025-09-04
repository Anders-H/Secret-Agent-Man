using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using RetroGame.Text;
using SecretAgentMan.OtherResources;
using SecretAgentMan.Sprites;

namespace SecretAgentMan.Scenes.Rooms;

public class Room
{
    private readonly List<Airplane> _airplanes;
    public Briefcase? Briefcase { get; set; }
    public string DistrictName { get; }
    public List<Npc> Npcs { get; }
    public CoinList Coins { get; }
    public AmmoBoxList Ammos { get; }
    public ObjectPositionPlaceholderList ObjectPositions = [];
    
    public Room(string districtName)
    {
        _airplanes = [];
        DistrictName = districtName;
        Npcs = [];
        Coins = [];
        Ammos = [];
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

    public void Draw(SpriteBatch spriteBatch, TextBlock textBlock, Player player, bool shouldDrawPlayer)
    {
        var playerIsDrawn = !shouldDrawPlayer;
        var lastY = IngameScene.SpriteUpperLimit - 1;

        foreach (var t in Npcs.OrderBy(x => x.IntYForYSort))
        {
            if (!playerIsDrawn && player.Y >= lastY && player.Y <= t.IntYForYSort)
            {
                if (shouldDrawPlayer)
                {
                    player.Draw(spriteBatch, IngameBackgroundResources.CharactersTexture, player.CellIndex, Color.White);
                    playerIsDrawn = true;
                }
            }

            t.Draw(spriteBatch);
            lastY = t.IntYForYSort;
        }

        if (!playerIsDrawn)
            player.Draw(spriteBatch, IngameBackgroundResources.CharactersTexture, player.CellIndex, Color.White);

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

        Briefcase?.Draw(spriteBatch);
    }

    public void DrawAirPlanes(SpriteBatch spriteBatch)
    {
        foreach (var airplane in _airplanes)
            airplane.Draw(spriteBatch);
    }

    public bool IsClear() =>
        Npcs.Count(x => x.Status == Npc.StatusSpyUndetected) + Npcs.Count(x => x.Status == Npc.StatusSpyDetected) <= 0;

    public void ResetNpcs()
    {
        foreach (var npc in Npcs)
            npc.PutTheGunAway();
    }

    public void SetBriefcaseCollected()
    {
        Briefcase = null; // TODO
    }

    public void TurnBriefcaseToBomb()
    {
        Briefcase = null; // TODO
    }
        
}