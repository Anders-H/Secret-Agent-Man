using System;

namespace SecretAgentMan.Scenes.Rooms;

public readonly struct ObjectPositionPlaceholder
{
    public const int ClosestAllowedDistance = 20;
    public int X { get; }
    public int Y { get; }

    public ObjectPositionPlaceholder(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int GetDistance(ObjectPositionPlaceholder other)
    {
        var dx = X - other.X;
        var dy = Y - other.Y;
        return (int)Math.Sqrt(dx * dx + dy * dy);
    }

    public int GetDistance(int otherX, int otherY)
    {
        var dx = X - otherX;
        var dy = Y - otherY;
        return (int)Math.Sqrt(dx * dx + dy * dy);
    }

    public bool DistanceIsAcceptable(int otherX, int otherY) =>
        GetDistance(otherX, otherY) >= ClosestAllowedDistance;

    public bool DistanceIsAcceptable(ObjectPositionPlaceholder other) =>
        GetDistance(other.X, other.Y) >= ClosestAllowedDistance;
}