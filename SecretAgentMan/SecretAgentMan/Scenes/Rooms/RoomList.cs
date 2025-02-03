using System.Collections.Generic;

namespace SecretAgentMan.Scenes.Rooms;

public class RoomList : List<Room>
{
    public RoomList()
    {
        var room0 = new Room("brooklyn");

        for (var i = 0; i < 3; i++)
            room0.Npcs.Add(Npc.CreateInnocent());

        for (var i = 0; i < 2; i++)
            room0.Npcs.Add(Npc.CreateSpy());

        var room1 = new Room("queens");

        for (var i = 0; i < 6; i++)
            room1.Npcs.Add(Npc.CreateInnocent());

        for (var i = 0; i < 4; i++)
            room1.Npcs.Add(Npc.CreateSpy());

        var room2 = new Room("the bronx");

        for (var i = 0; i < 9; i++)
            room2.Npcs.Add(Npc.CreateInnocent());

        for (var i = 0; i < 6; i++)
            room2.Npcs.Add(Npc.CreateSpy());

        var room3 = new Room("manhattan");

        for (var i = 0; i < 12; i++)
            room3.Npcs.Add(Npc.CreateInnocent());

        for (var i = 0; i < 8; i++)
            room3.Npcs.Add(Npc.CreateSpy());

        var room4 = new Room("harlem");

        for (var i = 0; i < 15; i++)
            room4.Npcs.Add(Npc.CreateInnocent());

        for (var i = 0; i < 10; i++)
            room4.Npcs.Add(Npc.CreateSpy());

        Add(room0);
        Add(room1);
        Add(room2);
        Add(room3);
        Add(room4);
    }
}