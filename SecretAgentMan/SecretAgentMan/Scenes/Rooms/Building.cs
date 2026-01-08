using RetroGame.RetroTextures;

namespace SecretAgentMan.Scenes.Rooms;

public struct Building
{
    public RetroTexture Texture { get; }
    public int X { get; }
    public int Y => Texture.Height - 91;
    public int Width => Texture.Width;

    public Building(RetroTexture texture, int x)
    {
        Texture = texture;
        X = x;
    }
}