using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RetroGame.RetroTextures;

namespace SecretAgentMan.OtherResources;

public static class IngameBackgroundResources
{
    public static RetroTexture? CharactersTexture { get; set; }
    public static RetroTexture? WaterTexture { get; set; }
    public static RetroTexture? AirplaneRightTexture { get; set; }
    public static RetroTexture? AirplaneLeftTexture { get; set; }

    public static void LoadContent(GraphicsDevice graphics, ContentManager content)
    {
        CharactersTexture = RetroTexture.LoadContent(graphics, content, 25, 25, 32, 1, "player25x25");
        WaterTexture = RetroTexture.LoadContent(graphics, content, 640, 30, 1, 18, "water640x30");
        AirplaneRightTexture = RetroTexture.LoadContent(graphics, content, 5, 3, 25, 1, "plane5x3");
        AirplaneLeftTexture = RetroTexture.LoadContent(graphics, content, 5, 3, 25, 1, "planeflipped5x3");
    }
}