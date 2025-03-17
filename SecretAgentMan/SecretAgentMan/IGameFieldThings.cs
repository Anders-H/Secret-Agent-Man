using Microsoft.Xna.Framework.Graphics;

namespace SecretAgentMan;

public interface IGameFieldThings
{
    public void Draw(SpriteBatch spriteBatch);
    public int IntX { get; }
    public int IntY { get; }
}