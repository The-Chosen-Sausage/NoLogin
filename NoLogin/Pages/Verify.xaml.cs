using System.Threading;
using System;
using System.IO;
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
using CommonSettingsHandling;

namespace NoLogin
{
    /// <summary>
    /// Interaction logic for Verify.xaml
    /// </summary>
    public partial class Verify : Page
    {
        static bool AddingUser = false;
        public Verify()
        {

            InitializeComponent();
            PassBox.Focus();
            if ((Settings.Variables["PasswordEnabled"] == "False") || MainWindow.Unlocked) { Unlock(); }
            
        }
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
           
            if (e.Key == Key.Return)
            {
                
                if (AddingUser)
                {
                    AddUser(((PasswordBox)sender).Password);
                }
                else
                {
                    VerifyPass(((PasswordBox)sender).Password);
                }
                ((PasswordBox)sender).Password = "";
                
                
            }
        }
        private void AddUser(string Pass)
        {
            Hashing.Store("Default", Pass);
            AddingUser = false;
            FailedText.Content = "Now enter your password.";
            FailedText.Visibility = Visibility.Visible;
        }
        private void VerifyPass(string Pass)
        {
            try
            {
                Hashing.Verify("Default", Pass);
                Unlock();
                
                
            }
            catch(UnauthorizedAccessException)
            {
                FailedText.Content = "Verification failed.";
                FailedText.Visibility = Visibility.Visible;
                
            }
            catch (FileNotFoundException)
            {
                FailedText.Content = "No saved password found. \r\nPlease enter a new one.";
                FailedText.Visibility = Visibility.Visible;
                AddingUser = true;
            }
        }
        void Unlock()
        {
            MainWindow.Unlocked = true;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).NavView.IsEnabled = true;
                    (window as MainWindow).HomeNavItem.IsSelected = true;
                }
            }
        }
    }
}
