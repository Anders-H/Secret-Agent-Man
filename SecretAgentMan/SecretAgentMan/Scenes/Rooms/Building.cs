using RetroGame.RetroTextures;

namespace SecretAgentMan.Scenes.Rooms;

public readonly struct Building
{
    public RetroTexture Texture { get; }
    public int X { get; }
    public int Y => 90 - Texture.Height;
    public int Width => Texture.Width;

    public Building(RetroTexture texture, int x)
    {
        Texture = texture;
        X = x;
    }
}