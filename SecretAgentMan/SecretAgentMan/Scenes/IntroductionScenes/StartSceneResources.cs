using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RetroGame.RetroTextures;

namespace SecretAgentMan.Scenes.IntroductionScenes;

public static class StartSceneResources
{
    public static RetroTexture? IntroGraphics { get; set; }
    public static RetroTexture? StartScreenFrame { get; set; }
    public static RetroTexture? StartScreenGun { get; set; }
    public static RetroTexture? StartScreenLogo { get; set; }
    public static RetroTexture? CreditTexture { get; set; }

    public static void LoadContent(GraphicsDevice graphics, ContentManager content)
    {
        IntroGraphics = RetroTexture.LoadContent(graphics, content, 640, 360, 1, "load-screen-360p-nofilter");
        StartScreenFrame = RetroTexture.LoadContent(graphics, content, 640, 360, 1, "start-screen-frame");
        StartScreenGun = RetroTexture.LoadContent(graphics, content, 640, 360, 1, "start-screen-gun");
        StartScreenLogo = RetroTexture.LoadContent(graphics, content, 433, 54, 15, "start-screen-logo433x54");
        CreditTexture = RetroTexture.LoadContent(graphics, content, 326, 158, 1, "start-screen-portraits");
    }
}