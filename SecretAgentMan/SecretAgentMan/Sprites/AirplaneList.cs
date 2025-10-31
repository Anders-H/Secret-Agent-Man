using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace SecretAgentMan.Sprites;

public class AirplaneList : List<Airplane>
{
    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var airplane in this)
            airplane.Draw(spriteBatch);
    }
}