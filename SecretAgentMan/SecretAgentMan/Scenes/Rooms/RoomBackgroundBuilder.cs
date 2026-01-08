using System;
using RetroGame.RetroTextures;

namespace SecretAgentMan.Scenes.Rooms;

public class RoomBackgroundBuilder
{
    public RoomBackground Build()
    {
        var roomBackground = new RoomBackground();
        const int x = 319;
        const int width = 640;
        var currentX = x;
        const int gap = 2;

        do
        {
            var building = GetRandomBuilding(x);
            
            if (building == null)
                break;

            roomBackground.AddBuilding(building, currentX);
            currentX -= (building.Width + gap);

            if (currentX <= 0)
                break;

        } while (true);

        currentX = gap + roomBackground.RoomBuildings[0].X;

        do
        {
            var building = GetRandomBuilding(width - currentX);

            if (building == null)
                break;

            roomBackground.AddBuilding(building, currentX);
            currentX += (building.Width + gap);

            if (currentX >= width)
                break;

        } while (true);

        return roomBackground;
    }

    private RetroTexture? GetRandomBuilding(int maxWidth)
    {
        for (var i = 0; i < 50; i++)
        {
            var building = RoomBackground.GetByIndex(Game1.Random.Next(0, RoomBackground.Count));

            if (building == null)
                throw new SystemException("Building texture not found.");

            if (building.Width <= maxWidth)
                return building;
        }

        return null;
    }
}