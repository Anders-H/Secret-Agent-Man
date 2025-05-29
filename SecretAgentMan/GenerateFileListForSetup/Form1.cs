using System.IO;
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
        s.AppendLine(@"Source: ""D:\GitRepos\Secret-Agent-Man\SecretAgentMan\SecretAgentManLoader\bin\Release\net8.0-windows\{#MyAppExeName}""; DestDir: ""{app}""; Flags: ignoreversion");
        var loaderDirectory = new DirectoryInfo(@"D:\GitRepos\Secret-Agent-Man\SecretAgentMan\SecretAgentManLoader\bin\Release\net8.0-windows\");

        foreach (var fileInfo in loaderDirectory.GetFiles())
        {
            if (fileInfo.Name.EndsWith(".exe", StringComparison.CurrentCultureIgnoreCase))
                continue;

            if (fileInfo.Name.EndsWith(".pdb", StringComparison.CurrentCultureIgnoreCase))
                continue;

            s.AppendLine($@"Source: ""D:\GitRepos\Secret-Agent-Man\SecretAgentMan\SecretAgentManLoader\bin\Release\net8.0-windows\{fileInfo.Name}""; DestDir: ""{{app}}""; Flags: ignoreversion");
        }

        var gameDirectory = new DirectoryInfo(@"D:\GitRepos\Secret-Agent-Man\SecretAgentMan\SecretAgentMan\bin\Release\net8.0-windows\win-x64\");

        foreach (var fileInfo in gameDirectory.GetFiles())
        {
            if (string.Compare(fileInfo.Name, "cheat.dat", StringComparison.CurrentCultureIgnoreCase) == 0)
                continue;

            if (fileInfo.Name.EndsWith(".pdb", StringComparison.CurrentCultureIgnoreCase))
                continue;

            s.AppendLine($@"Source: ""D:\GitRepos\Secret-Agent-Man\SecretAgentMan\SecretAgentMan\bin\Release\net8.0-windows\win-x64\{fileInfo.Name}""; DestDir: ""{{app}}""; Flags: ignoreversion");
        }

        var contentDirectory = new DirectoryInfo(@"D:\GitRepos\Secret-Agent-Man\SecretAgentMan\SecretAgentMan\bin\Release\net8.0-windows\win-x64\Content\");

        foreach (var fileInfo in contentDirectory.GetFiles())
        {
            if (string.Compare(fileInfo.Name, "cheat.dat", StringComparison.CurrentCultureIgnoreCase) == 0)
                continue;

            if (fileInfo.Name.EndsWith(".pdb", StringComparison.CurrentCultureIgnoreCase))
                continue;

            s.AppendLine($@"Source: ""D:\GitRepos\Secret-Agent-Man\SecretAgentMan\SecretAgentMan\bin\Release\net8.0-windows\win-x64\Content\{fileInfo.Name}""; DestDir: ""{{app}}\Content""; Flags: ignoreversion");
        }

        textBox1.Text = s.ToString();
    }
}