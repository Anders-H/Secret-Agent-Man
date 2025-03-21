using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RetroGame.RetroTextures;

namespace SecretAgentMan.OtherResources;

public static class MayorResources
{
    public static RetroTexture? MayorTexture { get; set; }

    public static void LoadContent(GraphicsDevice graphics, ContentManager content) =>
        MayorTexture = RetroTexture.LoadContent(graphics, content, 50, 50, 4, "mayor50x50");

    public static void SaySpyKilled(int killedSpyCount, int scoreAdded, MessageSystem messageSystem)
    {
        switch (killedSpyCount)
        {
            case 1:
                messageSystem.AddMessage($"first spy eliminated! {scoreAdded} points!", false);
                break;
            case 2:
                messageSystem.AddMessage($"second spy eliminated! {scoreAdded} points!", false);
                break;
            case 3:
                messageSystem.AddMessage($"third spy eliminated! {scoreAdded} points!", false);
                break;
            case 4:
                messageSystem.AddMessage($"fourth spy eliminated! {scoreAdded} points!", false);
                break;
            case 5:
                messageSystem.AddMessage($"fifth spy eliminated! {scoreAdded} points!", false);
                break;
            default:
                messageSystem.AddMessage($"spy number {killedSpyCount} eliminated! {scoreAdded} points!", false);
                break;
        }
    }
}