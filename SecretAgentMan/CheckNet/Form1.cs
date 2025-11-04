using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace CheckNet;

public partial class Form1 : Form
{
    private bool _hasNet8Win;

    public Form1()
    {
        InitializeComponent();
    }

    private void Form1_Shown(object sender, EventArgs e)
    {
        Refresh();
        _hasNet8Win = CheckNet8Win();

        if (_hasNet8Win)
        {
            lblStatus.Text = @"All runtime requirements are met.";
            timer1.Enabled = true;
        }
        else
        {
            lblStatus.Text = @"To run this game, you need to install the .NET 8.0 Desktop Runtime for Windows. Visit the link below, find the header "".NET Desktop Runtime"" (you need to scroll down a bit) and select the version that correspond to your system. If you don't know, select ""x64"".";
        }
    }

    private bool CheckNet8Win()
    {
        try
        {
            var programFiles = Environment.ExpandEnvironmentVariables("%ProgramW6432%");
            var programFilesX86 = Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%");
            var folders = new List<string>();

            return false;
        }
        catch
        {
            // Vi vet inte, så vi chansar.
            return true;
        }
    }

    private void lblUrl_Click(object sender, EventArgs e)
    {
        Process.Start(lblUrl.Text);
    }

    private void button1_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        Close();
    }
}