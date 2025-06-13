using Microsoft.Xna.Framework.Media;
using SecretAgentMan.OtherResources;

namespace SecretAgentMan.Scenes;

public class CutScene1 : RetroGame.Scene.IngameScene
{
    public CutScene1(RetroGame.RetroGame parent) : base(parent)
    {
        MediaPlayer.Stop();
    }

    public override void BeginScene()
    {
        MediaPlayer.Play(Songs.CutSceneSong!);
    }
}