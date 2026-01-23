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
        s.AppendLine(@"Source: ""D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\{#MyAppExeName}""; DestDir: ""{app}""; Flags: ignoreversion");
        var loaderDirectory = new DirectoryInfo(@"D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\");

        foreach (var fileInfo in loaderDirectory.GetFiles())
        {
            if (fileInfo.Name.EndsWith(".exe", StringComparison.CurrentCultureIgnoreCase))
                continue;

            if (fileInfo.Name.EndsWith(".pdb", StringComparison.CurrentCultureIgnoreCase))
                continue;

            s.AppendLine($@"Source: ""D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\{fileInfo.Name}""; DestDir: ""{{app}}""; Flags: ignoreversion");
        }

        var gameDirectory = new DirectoryInfo(@"D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\");

        foreach (var fileInfo in gameDirectory.GetFiles())
        {
            if (string.Compare(fileInfo.Name, "cheat.dat", StringComparison.CurrentCultureIgnoreCase) == 0)
                continue;

            if (fileInfo.Name.EndsWith(".pdb", StringComparison.CurrentCultureIgnoreCase))
                continue;

            s.AppendLine($@"Source: ""D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\{fileInfo.Name}""; DestDir: ""{{app}}""; Flags: ignoreversion");
        }

        var contentDirectory = new DirectoryInfo(@"D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\");

        foreach (var fileInfo in contentDirectory.GetFiles())
        {
            if (string.Compare(fileInfo.Name, "cheat.dat", StringComparison.CurrentCultureIgnoreCase) == 0)
                continue;

            if (fileInfo.Name.EndsWith(".pdb", StringComparison.CurrentCultureIgnoreCase))
                continue;

            s.AppendLine($@"Source: ""D:\GitRepos\Secret-Agent-Man\SecretAgentMan\PublishedProgram\Content\{fileInfo.Name}""; DestDir: ""{{app}}\Content""; Flags: ignoreversion");
        }

        textBox1.Text = s.ToString();
    }
}