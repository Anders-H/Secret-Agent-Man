using System.Collections.Generic;

namespace SecretAgentMan.Scenes.Rooms;

public class RoomList : List<Room>
{
    public RoomList()
    {
        Add(new Room("brooklyn"));
        Add(new Room("queens"));
        Add(new Room("the bronx"));
        Add(new Room("manhattan"));
        Add(new Room("harlem"));
    }
}