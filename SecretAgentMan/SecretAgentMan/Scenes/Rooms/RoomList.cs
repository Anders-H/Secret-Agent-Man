using System;
using System.Collections.Generic;
using System.Linq;
using RetroGame.Text;
using SecretAgentMan.Sprites;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace SecretAgentMan.Scenes.Rooms;

public class RoomList
{
    private List<Room> Rooms { get; }
    public readonly int SpyCount; 

    public RoomList(Player player, List<Fire> enemyFireList)
    {
        Rooms = [];
        int[] spyCount = [1, 2, 3, 4, 5, 10, 15];
        int[] innocentCount = [3, 4, 5, 6, 7, 10, 12];
        string[] names = ["morristown", "kearny", "brooklyn, new york", "queens, new york", "the bronx, new york", "manhattan, new york", "harlem, new york"];

        for (var i = 0; i < spyCount.Length; i++)
        {
            var room = new Room(names[i]);

            for (var j = 0; j < innocentCount[i]; j++)
                room.Npcs.Add(Npc.CreateInnocent(enemyFireList, j));

            for (var j = 0; j < spyCount[i]; j++)
                room.Npcs.Add(Npc.CreateSpy(player, enemyFireList, j));

            switch (i)
            {
                case 0:
                    room.AddAirplane();
                    break;
                case 1:
                    room.AddAirplane();
                    room.AddAirplane();
                    room.Coins.Add(new Coin(500, 150, 0));
                    break;
                case 2:
                    room.AddAirplane();
                    room.AddAirplane();
                    break;
                case 3:
                    room.AddAirplane();
                    room.AddAirplane();
                    room.AddAirplane();
                    room.AddAirplane();

                    for (var c = 0; c < 10; c++)
                        room.Coins.Add(new Coin(22 + 64 * c, 300, c % 4));

                    break;
                case 4:
                    room.AddAirplane();
                    room.AddAirplane();
                    room.AddAirplane();
                    break;
                case 5:
                    room.AddAirplane();
                    room.AddAirplane();


                    for (var c = 0; c < 16; c++)
                        room.Coins.Add(new Coin(7 + 40 * c, 150, c % 4));

                    break;
                case 6:
                    room.AddAirplane();
                    room.AddAirplane();
                    room.AddAirplane();
                    room.AddAirplane();
                    room.AddAirplane();

                    for (var c = 0; c < 20; c++)
                        room.Coins.Add(new Coin(7 + 32 * c, 300, c % 4));

                    break;
                default:
                    throw new SystemException("What room?!?");
            }

            Rooms.Add(room);
            SpyCount += spyCount[i];
        }
    }

    public int Count => Rooms.Count;

    public string GetDistrictName(int room) =>
        Rooms[room].DistrictName;

    public bool RoomIsClear(int room) =>
        Rooms[room].IsClear();

    public void Act(int room, ulong ticks) =>
        Rooms[room].Act(ticks);

    public List<Npc> GetNpcs(int room) =>
        Rooms[room].Npcs;

    public List<Coin> GetCoins(int room) =>
        Rooms[room].Coins;

    public void TurnOneDeadNpcToGraveStone(int room)
    {
        foreach (var npc in Rooms[room].Npcs.Where(npc => npc.AliveStatus == Character.StatusDead && !npc.IsGraveStone))
        {
            npc.TurnToGraveStone();
            break;
        }
    }

    public void DrawBackground(SpriteBatch spriteBatch, int room, TextBlock text, Player player) =>
        Rooms[room].Draw(spriteBatch, text, player);

    public void DrawDecorations(SpriteBatch spriteBatch, int room) =>
        Rooms[room].DrawAirPlanes(spriteBatch);
}