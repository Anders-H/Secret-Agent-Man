using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace SecretAgentMan;

public class FireList : List<Fire>
{
    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var f in this)
            f.Draw(spriteBatch);
    }
}