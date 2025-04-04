using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RetroGame.RetroTextures;

namespace SecretAgentMan.OtherResources;

public static class MayorResources
{
    public static RetroTexture? MayorTexture { get; set; }

    public static void LoadContent(GraphicsDevice graphics, ContentManager content) =>
        MayorTexture = RetroTexture.LoadContent(graphics, content, 50, 50, 4, "mayor50x50");

    public static void SaySpyKilled(int killedSpyCount, int scoreAdded)
    {
        switch (killedSpyCount)
        {
            case 1:
                Game1.TypeWriter.SetText($"first spy eliminated! {scoreAdded} points!");
                break;
            case 2:
                Game1.TypeWriter.SetText($"second spy eliminated! {scoreAdded} points!");
                break;
            case 3:
                Game1.TypeWriter.SetText($"third spy eliminated! {scoreAdded} points!");
                break;
            case 4:
                Game1.TypeWriter.SetText($"fourth spy eliminated! {scoreAdded} points!");
                break;
            case 5:
                Game1.TypeWriter.SetText($"fifth spy eliminated! {scoreAdded} points!");
                break;
            default:
                Game1.TypeWriter.SetText($"spy number {killedSpyCount} eliminated! {scoreAdded} points!");
                break;
        }
    }
}