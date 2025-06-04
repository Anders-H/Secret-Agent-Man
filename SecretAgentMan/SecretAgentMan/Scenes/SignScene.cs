namespace SecretAgentMan.Scenes;

public class SignScene : RetroGame.Scene.IngameScene
{
    private readonly string _message;

    public SignScene(RetroGame.RetroGame parent, string message) : base(parent)
    {
        _message = message;
    }
}