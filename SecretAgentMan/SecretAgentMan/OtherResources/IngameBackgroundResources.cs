using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RetroGame.RetroTextures;

namespace SecretAgentMan.OtherResources;

public static class IngameBackgroundResources
{
    public static RetroTexture? CharactersTexture { get; set; }
    public static RetroTextureVertical? WaterTexture { get; set; }
    public static RetroTexture? AirplaneRightTexture { get; set; }
    public static RetroTexture? AirplaneLeftTexture { get; set; }
    public static RetroTextureVertical? BackgroundLayer01 { get; set; }
    public static RetroTextureVertical? BackgroundLayer02 { get; set; }
    public static RetroTextureVertical? BackgroundLayer03 { get; set; }
    public static RetroTextureVertical? BackgroundLayer04 { get; set; }

    public static void LoadContent(GraphicsDevice graphics, ContentManager content)
    {
        CharactersTexture = RetroTexture.LoadContent(graphics, content, 25, 25, 32, "player25x25");
        WaterTexture = RetroTextureVertical.LoadContent(graphics, content, 640, 30, 18, "water640x30");
        AirplaneRightTexture = RetroTexture.LoadContent(graphics, content, 5, 3, 25, "plane5x3");
        AirplaneLeftTexture = RetroTexture.LoadContent(graphics, content, 5, 3, 25, "planeflipped5x3");
        BackgroundLayer01 = RetroTextureVertical.LoadContent(graphics, content, 640, 91, 4, "background-640x91");
        BackgroundLayer02 = RetroTextureVertical.LoadContent(graphics, content, 640, 91, 4, "sky-640x91");
        BackgroundLayer03 = RetroTextureVertical.LoadContent(graphics, content, 640, 91, 4, "skyline_bg-640x91");
        BackgroundLayer04 = RetroTextureVertical.LoadContent(graphics, content, 640, 91, 4, "skyline_fg-640x91");
    }
}