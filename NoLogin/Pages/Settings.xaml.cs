using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using CommonSettingsHandling;

namespace NoLogin.Pages
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
            PassCheck.IsChecked = Convert.ToBoolean(CommonSettingsHandling.Settings.Variables["PasswordEnabled"]);
        }

        private void CheckChanged(object sender, RoutedEventArgs e)
        {
            CommonSettingsHandling.Settings.Set("PasswordEnabled", PassCheck.IsChecked);
            CommonSettingsHandling.Settings.Save();
        }
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Return)
            {
                try
                {
                    AddUser(((PasswordBox)sender).Password);
                    Caption.Content = "Password set successfully.";
                    Caption.Foreground = Brushes.Green;
                    Caption.Visibility = Visibility.Visible;
                    ((PasswordBox)sender).Password = "";

                }
                catch(Exception ex)
                {
                    Caption.Content = ex;
                    Caption.Foreground = Brushes.Red;
                    Caption.Visibility = Visibility.Visible;
                    ((PasswordBox)sender).Password = "";
                }
            }
        }
        private void AddUser(string Pass)
        {
            Hashing.Store("Default", Pass);
        }
    }
}
