using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NoLogin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool Unlocked = false;
        public MainWindow()
        {
            InitializeComponent();
            NoLogin.Settings.Load();
        }

        private void NavigationView_BackRequested(ModernWpf.Controls.NavigationView sender, ModernWpf.Controls.NavigationViewBackRequestedEventArgs args)
        {
            try
            {
                if (Unlocked)
                {
                    MainFrame.GoBack();
                }
            }
            catch { }
        }

        private void SwitchPage(ModernWpf.Controls.NavigationView sender, ModernWpf.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            if (Unlocked)
            {
                string s = "Pages\\" + args.SelectedItem.ToString().Substring(39);

                MainFrame.Source = new Uri(s + ".xaml", UriKind.Relative);
            }
            else
            {
                MainFrame.Source = new Uri("Pages\\Verify.xaml", UriKind.Relative);
            }
        }
    }
}
