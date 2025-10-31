using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using RetroGame.Text;
using SecretAgentMan.OtherResources;
using SecretAgentMan.Sprites;

namespace SecretAgentMan.Scenes.Rooms;

public class Room
{
    public readonly AirplaneList Airplanes;
    public Briefcase? Briefcase { get; set; }
    public string DistrictName { get; }
    public NpcList Npcs { get; }
    public CoinList Coins { get; }
    public AmmoBoxList Ammos { get; }
    public ObjectPositionPlaceholderList ObjectPositions = [];
    public Bomb? Bomb { get; set; }

    public Room(string districtName)
    {
        Airplanes = [];
        DistrictName = districtName;
        Npcs = [];
        Coins = [];
        Ammos = [];
    }

    public void AddAirplane(int count)
    {
        for (var i = 0; i < count; i++)
            Airplanes.Add(new Airplane());
    }

    public void Act(ulong ticks)
    {
        Npcs.Act(ticks);

        foreach (var airplane in Airplanes)
            airplane.Act(ticks);
    }

    public void Draw(SpriteBatch spriteBatch, TextBlock textBlock, Player player, bool shouldDrawPlayer)
    {
        var playerIsDrawn = !shouldDrawPlayer;
        var lastY = IngameScene.SpriteUpperLimit - 1;

        foreach (var t in Npcs.YSorted())
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
}