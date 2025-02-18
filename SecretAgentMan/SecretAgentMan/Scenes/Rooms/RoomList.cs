using System.Collections.Generic;

namespace SecretAgentMan.Scenes.Rooms;

public class RoomList : List<Room>
{
    public readonly int SpyCount;

    public RoomList(Player player, List<Fire> enemyFireList)
    {
        var room0 = new Room("brooklyn");
        const int spyCountRoom0 = 2;
        const int spyCountRoom1 = 4;
        const int spyCountRoom2 = 6;
        const int spyCountRoom3 = 10;
        const int spyCountRoom4 = 15;
        SpyCount = spyCountRoom0 + spyCountRoom1 + spyCountRoom2 + spyCountRoom3 + spyCountRoom4;

        for (var i = 0; i < 3; i++)
            room0.Npcs.Add(Npc.CreateInnocent(enemyFireList));

        for (var i = 0; i < spyCountRoom0; i++)
            room0.Npcs.Add(Npc.CreateSpy(player, enemyFireList));

        var room1 = new Room("queens");

        for (var i = 0; i < 6; i++)
            room1.Npcs.Add(Npc.CreateInnocent(enemyFireList));

        for (var i = 0; i < spyCountRoom1; i++)
            room1.Npcs.Add(Npc.CreateSpy(player, enemyFireList));

        var room2 = new Room("the bronx");

        for (var i = 0; i < 9; i++)
            room2.Npcs.Add(Npc.CreateInnocent(enemyFireList));

        for (var i = 0; i < spyCountRoom2; i++)
            room2.Npcs.Add(Npc.CreateSpy(player, enemyFireList));

        var room3 = new Room("manhattan");

        for (var i = 0; i < 12; i++)
            room3.Npcs.Add(Npc.CreateInnocent(enemyFireList));

        for (var i = 0; i < spyCountRoom3; i++)
            room3.Npcs.Add(Npc.CreateSpy(player, enemyFireList));

        var room4 = new Room("harlem");

        for (var i = 0; i < 15; i++)
            room4.Npcs.Add(Npc.CreateInnocent(enemyFireList));

        for (var i = 0; i < spyCountRoom4; i++)
            room4.Npcs.Add(Npc.CreateSpy(player, enemyFireList));

        Add(room0);
        Add(room1);
        Add(room2);
        Add(room3);
        Add(room4);
    }
}