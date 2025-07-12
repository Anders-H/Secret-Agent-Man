using System.Collections.Generic;
using System.Linq;
using SecretAgentMan.Sprites;

namespace SecretAgentMan.Scenes.Rooms;

public class ObjectPositionPlaceholderList : List<ObjectPositionPlaceholder>
{
    public ObjectPositionPlaceholderList()
    {
        Add(new ObjectPositionPlaceholder(Player.PlayerStartX, Player.PlayerStartY));
    }

    public bool DistanceIsAcceptable(int otherX, int otherY) =>
        this.All(o => o.DistanceIsAcceptable(otherX, otherY));

    public ObjectPositionPlaceholder GetRandomAcceptableDistance()
    {
        var retryCount = 0;
        do
        {
            retryCount++;
            var o = GetRandom();

            if (DistanceIsAcceptable(o.X, o.Y) || retryCount > 1000)
                return o;

        } while (true);
    }

    public static ObjectPositionPlaceholder GetRandom()
    {
        var x = Game1.Random.Next(30, 597);
        var y = Game1.Random.Next(98, 268);
        return new ObjectPositionPlaceholder(x, y);
    }
}