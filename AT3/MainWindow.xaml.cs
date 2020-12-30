using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AT3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Define the variables used
        private bool firstTimePassword = false;
        Password myPassword = null;
        Logger myLogger = null;
        public MainWindow()
        {
            InitializeComponent();
            // Initialise the Logger
            myLogger = Logger.GetInstance();
            myLogger.WriteLogMessage("Logger Initialised");
            // Initialise the Password
            myPassword = Password.GetInstance();
            myLogger.WriteLogMessage("Password Initialised");
            // Check whether user password has been entered previously 
            if (myPassword.UserPasswordExists())
            {
                // Ask for the existing password
            }
            else
            {
                // Ask for the user password 
                EnterPassword.Content = "New Password";
                firstTimePassword = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (firstTimePassword)
            {
                // Store the first time password
                myPassword.WritePassword(PasswordBox.Password, Password.PasswordType.User);
                myLogger.WriteLogMessage("New Password Saved");
                PasswordBox.Password = "";
                EnterPassword.Content = "Enter Password";
                firstTimePassword = false;
            }
            else
            {
                if (myPassword.CheckPassword(PasswordBox.Password))
                {
                    // Display if password is correct
                    myLogger.WriteLogMessage("Password Match");
                    Confirmation.Content = "Correct Password";
                    PasswordBox.Password = "";
                    // Open contacts window and close login window
                    ContactsWindow cW = new ContactsWindow();
                    cW.Show();
                    this.Close();

                }
                else
                {
                    //Display if password is incorrect
                    myLogger.WriteLogMessage("Password Mismatch");
                    Confirmation.Content = "Incorrect Password";
                    PasswordBox.Password = "";
                }
            }
        }
    }
}
