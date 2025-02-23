using System.Collections.Generic;

namespace SecretAgentMan.Scenes.Rooms;

public class RoomList : List<Room>
{
    public readonly int SpyCount;

    public RoomList(Player player, List<Fire> enemyFireList)
    {
        int[] spyCount = [1, 2, 3, 4, 5, 10, 15];
        int[] innocentCount = [3, 4, 5, 6, 7, 10, 12];
        string[] names = ["morristown", "kearny", "brooklyn, new york", "queens, new york", "the bronx, new york", "manhattan, new york", "harlem, new york"];

        for (var i = 0; i < spyCount.Length; i++)
        {
            var room = new Room(names[i]);

            for (var j = 0; j < innocentCount[i]; j++)
                room.Npcs.Add(Npc.CreateInnocent(enemyFireList));

            for (var j = 0; j < spyCount[i]; j++)
                room.Npcs.Add(Npc.CreateSpy(player, enemyFireList));

            switch (i)
            {
                case 1:
                case 5:
                    room.AddAirplane();
                    room.AddAirplane();
                    break;
                case 2:
                case 4:
                    room.AddAirplane();
                    room.AddAirplane();
                    room.AddAirplane();
                    break;
                case 3:
                    room.AddAirplane();
                    room.AddAirplane();
                    room.AddAirplane();
                    room.AddAirplane();
                    break;
                default:
                    room.AddAirplane();
                    break;
            }

            Add(room);
            SpyCount += spyCount[i];
        }
    }
}