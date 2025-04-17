using System.Diagnostics;

namespace SecretAgentManLoader;

public partial class Form1 : Form
{
    private readonly string _videoPath;
    private readonly string _gamePath;
    private int _seconds = 0;

    public Form1()
    {
        InitializeComponent();
        var location = System.Reflection.Assembly.GetEntryAssembly()!.Location;
        var fileInfo = new FileInfo(location);
        var directoryName = fileInfo.DirectoryName;
        _videoPath = Path.Combine(directoryName!, "havet-logo.mp4");
        _gamePath = Path.Combine(directoryName!, "SecretAgentMan.exe");

#if DEBUG
        _gamePath = @"D:\GitRepos\Secret-Agent-Man\SecretAgentMan\SecretAgentMan\bin\Release\net8.0-windows\win-x64\SecretAgentMan.exe";
#endif
        axWindowsMediaPlayer1.uiMode = "none";
    }

    private void Form1_Shown(object sender, EventArgs e)
    {
        Refresh();
        timer1.Interval = 1000;
        timer1.Start();
        axWindowsMediaPlayer1.URL = _videoPath;
        var screen = Screen.PrimaryScreen!;
        Left = screen.Bounds.Left;
        Top = screen.Bounds.Top;
        Width = screen.Bounds.Width;
        Height = screen.Bounds.Height;
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        _seconds++;

        switch (_seconds)
        {
            case 10:
                Process.Start(_gamePath);
                break;
            case 15:
                Close();
                break;
        }
    }
}