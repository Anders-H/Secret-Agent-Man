using System;

namespace SecretAgentMan.Scenes.Rooms;

public class ObjectPositionPlaceholder
{
    public const int ClosestAllowedDistance = 20;
    public int X { get; set; }
    public int Y { get; set; }

    public int GetDistance(int otherX, int otherY)
    {
        var dx = X - otherX;
        var dy = Y - otherY;
        return (int)Math.Sqrt(dx * dx + dy * dy);
    }

    public bool DistanceIsAcceptable(int otherX, int otherY) =>
        GetDistance(otherX, otherY) >= ClosestAllowedDistance;
}