using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace InstallSAM
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            Refresh();
            MessageBox.Show(this, @"Secret Agent Man is successfully installed on your computer.", @"Secret Agent Man", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (HasDotNet8())
            {
                lblMessage.Text = @".NET 8 is installed.";
                Refresh();
                System.Threading.Thread.Sleep(1000);
                Close();
            }
            else
            {
                lblMessage.Text = @"You need to install .NET Desktop Runtime 8. Find the section called "".NET Desktop Runtime"", and click on the installer link for the correct system. If you are unsure, it might be x64 you are looking for.";
                lblUrl.Visible = true;
                btnClose.Enabled = true;
            }
        }

        private bool HasDotNet8()
        {
            try
            {
                var proc = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "dotnet.exe",
                        Arguments = "--list-runtimes",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };

                proc.Start();
                var rows = new List<string>();

                while (!proc.StandardOutput.EndOfStream)
                    rows.Add(proc.StandardOutput.ReadLine());

                return rows.Any(row => row.StartsWith("Microsoft.WindowsDesktop.App 8.", StringComparison.CurrentCultureIgnoreCase));
            }
            catch
            {
                return false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lblUrl_Click(object sender, EventArgs e)
        {
            Process.Start(lblUrl.Text);
        }
    }
}