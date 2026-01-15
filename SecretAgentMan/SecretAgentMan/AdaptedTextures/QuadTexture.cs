using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using RetroGame.RetroTextures;

namespace SecretAgentMan.AdaptedTextures;

public class QuadTexture
{
    public int Count => 4;
    public RetroTexture? Texture1 { get; set; }
    public RetroTexture? Texture2 { get; set; }
    public RetroTexture? Texture3 { get; set; }
    public RetroTexture? Texture4 { get; set; }

    public void LoadResources(GraphicsDevice graphicsDevice, ContentManager content, int width, int height, int cellCount, string name1, string name2, string name3, string name4)
    {
        Texture1 = RetroTexture.LoadContent(graphicsDevice, content, width, height, cellCount, name1);
        Texture2 = RetroTexture.LoadContent(graphicsDevice, content, width, height, cellCount, name2);
        Texture3 = RetroTexture.LoadContent(graphicsDevice, content, width, height, cellCount, name3);
        Texture4 = RetroTexture.LoadContent(graphicsDevice, content, width, height, cellCount, name4);
    }

    public RetroTexture? GetTexture(int index) =>
        index switch
        {
            0 => Texture1,
            1 => Texture2,
            2 => Texture3,
            3 => Texture4,
            _ => null,
        };
}