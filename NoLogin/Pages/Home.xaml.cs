using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using Microsoft.WindowsAPICodePack.Dialogs;
using CommonSettingsHandling;
namespace NoLogin
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
        }

        
        private void LaunchExploer(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog o =new CommonOpenFileDialog();
            
            var i = o.ShowDialog();
            if (i == CommonFileDialogResult.Ok)
            {
                try { Process.Start(o.FileName); } catch { };
            }
        }

        private void CMD(object sender, RoutedEventArgs e)
        {
            Process.Start("cmd");
        }

        private void StartMenu(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog o =new CommonOpenFileDialog();
            o.InitialDirectory = @"C:\ProgramData\Microsoft\Windows\Start Menu\Programs";
            var i=o.ShowDialog();
            if (i==CommonFileDialogResult.Ok)
            {
                try { Process.Start(o.FileName); } catch { };
            }

        }

        private void TaskMgr(object sender, RoutedEventArgs e)
        {
            Process.Start("taskmgr");
        }
    }
    
}
