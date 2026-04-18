using System;

namespace SecretAgentMan;

public class Program
{
    [STAThread]
    private static void Main()
    {
#if !DEBUG
        VideoIntroPlayer.Play("havet-logo.mp4");
#endif
        using var game = new Game1();
        game.Run();
    }
}