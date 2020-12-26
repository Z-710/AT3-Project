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
using System.Windows.Shapes;

namespace AT3
{
    /// <summary>
    /// Interaction logic for WritingWindow.xaml
    /// </summary>
    public partial class WritingWindow : Window
    {
        Contacts myContacts = null;
        Logger myLogger = null;
        Settings mySettings = null;
        Messages myMessages = null;
        public WritingWindow()
        {
            InitializeComponent();
            // Initialise the Logger
            myLogger = Logger.GetInstance();
            myLogger.WriteLogMessage("Writing Window Initialised");
            // Initialise the Contacts
            myContacts = Contacts.GetInstance();
            // Initialise the Settings
            mySettings = Settings.GetInstance();
            // Initialise the Messages (passing in the selected contact)
            myMessages = Messages.GetInstance(Contacts.selectedContact);
            myMessages.ReadMessages();
            // Populate messages form 
            MessageBox1.Text = Messages.MessageArray[0].message + " "+ Messages.MessageArray[0].time;
            MessageBox2.Text = Messages.MessageArray[2].message + " "+ Messages.MessageArray[2].time;
            MessageBox3.Text = Messages.MessageArray[4].message + " "+ Messages.MessageArray[4].time;
            ReceiveBox1.Text = Messages.MessageArray[1].message + " "+ Messages.MessageArray[1].time;
            ReceiveBox2.Text = Messages.MessageArray[3].message + " "+ Messages.MessageArray[3].time;
            ReceiveBox3.Text = Messages.MessageArray[5].message + " " +Messages.MessageArray[5].time;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ContactsButton_Click(object sender, RoutedEventArgs e)
        {
            ContactsWindow cW = new ContactsWindow();
            cW.Show();
            this.Close();
        }
    }
}
