using System.Collections.Generic;
using System.Linq;
using SecretAgentMan.Sprites;

namespace SecretAgentMan.Scenes.Rooms;

public class ObjectPositionPlaceholderList : List<ObjectPositionPlaceholder>
{
    public ObjectPositionPlaceholderList()
    {
        Add(new ObjectPositionPlaceholder { X = Player.PlayerStartX, Y = Player.PlayerStartY });
    }

    public bool DistanceIsAcceptable(int otherX, int otherY) =>
        this.All(o => o.DistanceIsAcceptable(otherX, otherY));
}