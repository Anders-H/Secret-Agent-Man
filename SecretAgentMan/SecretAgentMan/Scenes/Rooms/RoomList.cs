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

    public RoomList(Player player, FireList enemyFireList, int zeroBasedLevel) : this(player, enemyFireList, zeroBasedLevel, static (index, name) => new Room(index, name))
    {
    }

    internal RoomList(Player player, FireList enemyFireList, int zeroBasedLevel, Func<int, string, Room> createRoom)
    {
        Rooms = [];
        var level = LevelConfiguration.Load(zeroBasedLevel,
            key => Game1.Settings?.HasSetting(key) == true ? Game1.Settings.GetValue(key) : null, Game1.Random);

        var decoration = new AirPlaneDecorationService(zeroBasedLevel);

        
        for (var i = 0; i < 10; i++)
        {
            var room = createRoom(i, level.Names[i]);

            AddCoins(level.Coins[i], room);
            AddAmmos(level.Ammo[i], room);

            for (var j = 0; j < level.Innocents[i]; j++)
                room.Npcs.Add(Npc.CreateInnocent(enemyFireList, j));

            for (var j = 0; j < level.Spies[i]; j++)
                room.Npcs.Add(Npc.CreateSpy(player, enemyFireList, j));

            decoration.AddPlanes(room);
            Rooms.Add(room);
            SpyCount += level.Spies[i];
        }

        AddBriefcases();
    }

    public int SuggestKillsRequired()
    {
#if DEBUG
        return 1;
#else
        var lowLimit = SpyCount / 2;
        var requiredCount = Game1.Random.Next(lowLimit, SpyCount + 1);
        
        while (requiredCount % 5 != 0)
            requiredCount--;

        return requiredCount;
#endif
    }

    public Room GetRoom(int index) =>
            Rooms[index];

    private void AddBriefcases()
    {
        var availableRooms = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        for (var color = Briefcase.Silver; color <= Briefcase.Brown; color++)
        {
            var room = TakeRoom();
            var position = ReservePosition(room);
            room.Briefcase = new Briefcase(color, position.X, position.Y);
        }

        var bombRoom = TakeRoom();
        var bombPosition = ReservePosition(bombRoom);
        bombRoom.Bomb = new Bomb(bombPosition.X, bombPosition.Y, 0);

        Room TakeRoom()
        {
            var index = Game1.Random.Next(availableRooms.Count);
            var room = Rooms[availableRooms[index]];
            availableRooms.RemoveAt(index);
            return room;
        }
    }

    private static ObjectPositionPlaceholder ReservePosition(Room room)
    {
        var position = room.ObjectPositions.GetRandomAcceptableDistanceWidthBorder();
        room.ObjectPositions.Add(position);
        return position;
    }

    private static void AddCoins(int count, Room room)
    {
        for (var i = 0; i < count; i++)
        {
            var position = room.ObjectPositions.GetRandomAcceptableDistance();
            room.ObjectPositions.Add(position);
            room.Coins.Add(new Coin(position.X, position.Y + 15, 0));
        }
    }

    private static void AddAmmos(int count, Room room)
    {
        for (var i = 0; i < count; i++)
        {
            var position = room.ObjectPositions.GetRandomAcceptableDistance();
            room.ObjectPositions.Add(position);
            room.Ammos.Add(new AmmoBox(position.X, position.Y + 15));
        }
    }

    public int Count =>
        Rooms.Count;

    public void TurnOneDeadNpcToGraveStone(int room)
    {
        foreach (var npc in Rooms[room].Npcs.Where(npc => npc.AliveStatus == Character.StatusDead && !npc.IsGraveStone))
        {
            npc.TurnToGraveStone();
            break;
        }
    }

    public void DrawBackground(SpriteBatch spriteBatch, int room, TextBlock text, Player player, bool shouldDrawPlayer)
    {
        Rooms[room].Draw(spriteBatch, text, player, shouldDrawPlayer);
        Rooms[room].Airplanes.Draw(spriteBatch);
    }
}
