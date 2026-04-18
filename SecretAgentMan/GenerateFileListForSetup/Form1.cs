using System.Text;

namespace GenerateFileListForSetup;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        var s = new StringBuilder();
        s.AppendLine("// Main exe");
        s.AppendLine(@"Source: ""D:\GitRepos\Secret-Agent-Man\SecretAgentMan\SecretAgentMan\bin\Release\net8.0-windows\win-x64\{#MyAppExeName}""; DestDir: ""{app}""; Flags: ignoreversion");
        var gameDirectory = new DirectoryInfo(@"D:\GitRepos\Secret-Agent-Man\SecretAgentMan\SecretAgentMan\bin\Release\net8.0-windows\win-x64\");
        s.AppendLine("//Supporting files");

        foreach (var fileInfo in gameDirectory.GetFiles())
        {
            if (fileInfo.Name.EndsWith(".exe", StringComparison.CurrentCultureIgnoreCase))
                continue;

            if (fileInfo.Name.EndsWith(".pdb", StringComparison.CurrentCultureIgnoreCase))
                continue;

            if (string.Compare(fileInfo.Name, "cheat.dat", StringComparison.CurrentCultureIgnoreCase) == 0)
                continue;

            s.AppendLine($@"Source: ""D:\GitRepos\Secret-Agent-Man\SecretAgentMan\SecretAgentMan\bin\Release\net8.0-windows\win-x64\{fileInfo.Name}""; DestDir: ""{{app}}""; Flags: ignoreversion");
        }

        s.AppendLine("// Main content");
        var contentDirectory = new DirectoryInfo(@"D:\GitRepos\Secret-Agent-Man\SecretAgentMan\SecretAgentMan\bin\Release\net8.0-windows\win-x64\Content\");

        foreach (var fileInfo in contentDirectory.GetFiles())
        {
            if (string.Compare(fileInfo.Name, "cheat.dat", StringComparison.CurrentCultureIgnoreCase) == 0)
                continue;

            if (fileInfo.Name.EndsWith(".pdb", StringComparison.CurrentCultureIgnoreCase))
                continue;

            s.AppendLine($@"Source: ""D:\GitRepos\Secret-Agent-Man\SecretAgentMan\SecretAgentMan\bin\Release\net8.0-windows\win-x64\Content\{fileInfo.Name}""; DestDir: ""{{app}}\Content""; Flags: ignoreversion");
        }

        s.AppendLine("// Background buildings");
        var backgrounds = new DirectoryInfo(@"D:\GitRepos\Secret-Agent-Man\SecretAgentMan\SecretAgentMan\bin\Release\net8.0-windows\win-x64\Content\bg");

        foreach (var fileInfo in backgrounds.GetFiles())
        {
            s.AppendLine($@"Source: ""D:\GitRepos\Secret-Agent-Man\SecretAgentMan\SecretAgentMan\bin\Release\net8.0-windows\win-x64\Content\bg\{fileInfo.Name}""; DestDir: ""{{app}}\Content\bg""; Flags: ignoreversion");
        }

        s.AppendLine("// Background silhouettes");
        var silhouettes = new DirectoryInfo(@"D:\GitRepos\Secret-Agent-Man\SecretAgentMan\SecretAgentMan\bin\Release\net8.0-windows\win-x64\Content\bg\bg");

        foreach (var fileInfo in silhouettes.GetFiles())
        {
            s.AppendLine($@"Source: ""D:\GitRepos\Secret-Agent-Man\SecretAgentMan\SecretAgentMan\bin\Release\net8.0-windows\win-x64\Content\bg\bg\{fileInfo.Name}""; DestDir: ""{{app}}\Content\bg\bg""; Flags: ignoreversion");
        }

        s.AppendLine("// Background skies");
        var sky = new DirectoryInfo(@"D:\GitRepos\Secret-Agent-Man\SecretAgentMan\SecretAgentMan\bin\Release\net8.0-windows\win-x64\Content\bg\sky");

        foreach (var fileInfo in sky.GetFiles())
        {
            s.AppendLine($@"Source: ""D:\GitRepos\Secret-Agent-Man\SecretAgentMan\SecretAgentMan\bin\Release\net8.0-windows\win-x64\Content\bg\sky\{fileInfo.Name}""; DestDir: ""{{app}}\Content\bg\sky""; Flags: ignoreversion");
        }

        textBox1.Text = s.ToString();
    }
}