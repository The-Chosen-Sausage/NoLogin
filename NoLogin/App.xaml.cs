using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.IO;
namespace NoLogin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            try
            {
                string action = Environment.GetCommandLineArgs()[1];
                if (action == "/install")
                {
                    try
                    {
                        Install();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                if (action == "/uninstall")
                {
                    try
                    {
                        Uninstall();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
                Environment.Exit(0);
            }
            catch { }
        }

        private void Uninstall()
        {
            if (!File.Exists("Utilman.exe")) {
                if (File.Exists("Utilman.bk.exe"))
                {
                    File.Move("Utilman.bk.exe", "Utilman.exe");
                }
                return; }
            File.Delete("Utilman.exe");
            File.Move("Utilman.bk.exe", "Utilman.exe");
        }

        void Install()
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/C takeown /f Utilman.exe";
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            p.WaitForExit();
            if (!p.StandardOutput.ReadToEnd().Contains("SUCCESS"))
            {
                throw new UnauthorizedAccessException();
            }
            File.Move("Utilman.exe", "Utilman.bk.exe");
            File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\The Chosen Sausage\NoLogin\Launcher.exe","Utilman.exe");
            
        }
        void runcmd(string s)
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/C "+s;
            
            p.Start();
            p.WaitForExit();
            
        }
    }
    
}
