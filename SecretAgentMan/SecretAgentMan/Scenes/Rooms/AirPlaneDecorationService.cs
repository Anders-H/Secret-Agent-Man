using System;

namespace SecretAgentMan.Scenes.Rooms;

public class AirPlaneDecorationService
{
    private readonly int _level;

    public AirPlaneDecorationService(int level)
    {
        _level = level;
    }

    public void AddPlanes(Room room)
    {
        switch (room.RoomIndex)
        {
            case 0:
                room.AddAirplane(2);
                break;
            case 1:
                room.AddAirplane(_level + 1);
                break;
            case 2:
                room.AddAirplane(2);
                break;
            case 3:
                room.AddAirplane(1);
                break;
            case 4:
                room.AddAirplane(2);
                break;
            case 5:
                room.AddAirplane(1);
                break;
            case 6:
                room.AddAirplane(4);
                break;
            case 7:
                room.AddAirplane(_level + 2);
                break;
            case 8:
                room.AddAirplane(3);
                break;
            case 9:
                room.AddAirplane(Game1.Random.Next(3 + _level));
                break;
            default:
                throw new SystemException("What room?!?");
        }
    }
}