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

    public RoomList(Player player, FireList enemyFireList, int zeroBasedLevel)
    {
        Rooms = [];
        var objectPositions = new ObjectPositionPlaceholderList();
        var spyCount = new int[10];
        var innocentCount = new int[10];
        var names = new string[10];

        switch (zeroBasedLevel)
        {
            case 0:
                if (Game1.Settings != null)
                {
                    // Overwrite names.
                    var namesInConfig = Game1.Settings.GetValue("Level1RoomNames");
                    var parts = namesInConfig.Split(';');

                    for (var i = 0; i < names.Length; i++)
                        names[i] = parts[i].ToLower().Trim();

                    // Overwrite NPC count.
                    var npcsInConfig = Game1.Settings.GetValue("Level1InnocentNpcCount");
                    parts = npcsInConfig.Split(',');

                    for (var i = 0; i < innocentCount.Length; i++)
                        innocentCount[i] = int.Parse(parts[i]);

                    // Overwrite spy count.
                    var spysInConfig = Game1.Settings.GetValue("Level1SpyCount");
                    parts = spysInConfig.Split(',');

                    for (var i = 0; i < spyCount.Length; i++)
                        spyCount[i] = int.Parse(parts[i]);
                }

                break;
            case 1:
                if (Game1.Settings != null)
                {
                    // Overwrite names.
                    var namesInConfig = Game1.Settings.GetValue("Level2RoomNames");
                    var parts = namesInConfig.Split(';');

                    for (var i = 0; i < names.Length; i++)
                        names[i] = parts[i].ToLower().Trim();

                    // Overwrite NPC count.
                    var npcsInConfig = Game1.Settings.GetValue("Level2InnocentNpcCount");
                    parts = npcsInConfig.Split(',');

                    for (var i = 0; i < innocentCount.Length; i++)
                        innocentCount[i] = int.Parse(parts[i]);

                    // Overwrite spy count.
                    var spysInConfig = Game1.Settings.GetValue("Level2SpyCount");
                    parts = spysInConfig.Split(',');

                    for (var i = 0; i < spyCount.Length; i++)
                        spyCount[i] = int.Parse(parts[i]);
                }

                break;
            case 2:
                if (Game1.Settings != null)
                {
                    // Overwrite names.
                    var namesInConfig = Game1.Settings.GetValue("Level3RoomNames");
                    var parts = namesInConfig.Split(';');

                    for (var i = 0; i < names.Length; i++)
                        names[i] = parts[i].ToLower().Trim();

                    // Overwrite NPC count.
                    var npcsInConfig = Game1.Settings.GetValue("Level3InnocentNpcCount");
                    parts = npcsInConfig.Split(',');

                    for (var i = 0; i < innocentCount.Length; i++)
                        innocentCount[i] = int.Parse(parts[i]);

                    // Overwrite spy count.
                    var spysInConfig = Game1.Settings.GetValue("Level3SpyCount");
                    parts = spysInConfig.Split(',');

                    for (var i = 0; i < spyCount.Length; i++)
                        spyCount[i] = int.Parse(parts[i]);
                }

                break;
            default:
                throw new SystemException("What level?!?");
        }

        for (var i = 0; i < 10; i++)
        {
            var room = new Room(names[i]);
            AddCoins(zeroBasedLevel + 1, ref objectPositions, i, ref room);
            AddAmmos(zeroBasedLevel + 1, ref objectPositions, i, ref room);

            for (var j = 0; j < innocentCount[i]; j++)
                room.Npcs.Add(Npc.CreateInnocent(enemyFireList, j));

            for (var j = 0; j < spyCount[i]; j++)
                room.Npcs.Add(Npc.CreateSpy(player, enemyFireList, j));

            switch (i)
            {
                case 0:
                    room.AddAirplane(2);
                    break;
                case 1:
                    room.AddAirplane(3);
                    break;
                case 2:
                    room.AddAirplane(2);
                    break;
                case 3:
                    room.AddAirplane(7);
                    break;
                case 4:
                    room.AddAirplane(2);
                    break;
                case 5:
                    room.AddAirplane(2);
                    break;
                case 6:
                    room.AddAirplane(4);
                    break;
                case 7:
                    room.AddAirplane(8);
                    break;
                case 8:
                    room.AddAirplane(3);
                    break;
                case 9:
                    room.AddAirplane(15);
                    break;
                default:
                    throw new SystemException("What room?!?");
            }

            Rooms.Add(room);
            SpyCount += spyCount[i];
        }
    }

    private static void AddCoins(int level, ref ObjectPositionPlaceholderList objectPositions, int roomIndex, ref Room room)
    {
        var coinCountInConfig = Game1.Settings!.GetValue($"Level{level}Coins");
        var parts = coinCountInConfig.Split(',');
        var coinsCount = new int[10];

        for (var i = 0; i < coinsCount.Length; i++)
            coinsCount[i] = int.Parse(parts[i]);

        for (var j = 0; j < coinsCount[roomIndex]; j++)
        {
            var position = objectPositions.GetRandomAcceptableDistance();
            objectPositions.Add(position);
            room.Coins.Add(new Coin(position.X, position.Y + 15, 0));
        }
    }

    private static void AddAmmos(int level, ref ObjectPositionPlaceholderList objectPositions, int roomIndex, ref Room room)
    {
        var coinCountInConfig = Game1.Settings!.GetValue($"Level{level}Ammo");
        var parts = coinCountInConfig.Split(',');
        var ammoCount = new int[10];

        for (var i = 0; i < ammoCount.Length; i++)
            ammoCount[i] = int.Parse(parts[i]);

        for (var j = 0; j < ammoCount[roomIndex]; j++)
        {
            var position = objectPositions.GetRandomAcceptableDistance();
            objectPositions.Add(position);
            room.Ammos.Add(new AmmoBox(position.X, position.Y + 15));
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

    public CoinList GetCoins(int room) =>
        Rooms[room].Coins;

    public AmmoBoxList GetAmmos(int room) =>
        Rooms[room].Ammos;

    public void TurnOneDeadNpcToGraveStone(int room)
    {
        foreach (var npc in Rooms[room].Npcs.Where(npc => npc.AliveStatus == Character.StatusDead && !npc.IsGraveStone))
        {
            npc.TurnToGraveStone();
            break;
        }
    }

    public void DrawBackground(SpriteBatch spriteBatch, int room, TextBlock text, Player player, bool shouldDrawPlayer) =>
        Rooms[room].Draw(spriteBatch, text, player, shouldDrawPlayer);

    public void DrawDecorations(SpriteBatch spriteBatch, int room) =>
        Rooms[room].DrawAirPlanes(spriteBatch);
}