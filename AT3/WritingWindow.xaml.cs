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
        // Position of the view window in the array
        int cursorPosition = 0;
        // Variables to hold messages that are displayed
        public const int messagesShown = 10;
        Messages.Messagestruct [] viewArray = new Messages.Messagestruct[messagesShown];
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
            // Set the scrollbar thumb size
            MessageScrollBar.Minimum = 0;
            MessageScrollBar.Maximum = Messages.newestMessage;
            MessageScrollBar.ViewportSize = messagesShown;
            myLogger.WriteLogMessage("Number of messages is " + Messages.newestMessage.ToString());
            MessageScrollBar.Value = Messages.newestMessage;
            // The cursor starts at the bottom of the list 
            cursorPosition = Messages.newestMessage;
            // Populate the message form 
            myMessages.GetMessages(cursorPosition, messagesShown, ref viewArray);
            MessageBox1.Text = viewArray[0].time + viewArray[0].message;
            if (viewArray[0].type == "send") MessageBox1.Background = Brushes.LightSkyBlue;
            else MessageBox1.Background = Brushes.LightGray;
            MessageBox2.Text = viewArray[1].time + viewArray[1].message;
            if (viewArray[1].type == "send") MessageBox2.Background = Brushes.LightSkyBlue;
            else MessageBox2.Background = Brushes.LightGray;
            MessageBox3.Text = viewArray[2].time + viewArray[2].message;
            if (viewArray[2].type == "send") MessageBox3.Background = Brushes.LightSkyBlue;
            else MessageBox3.Background = Brushes.LightGray;
            MessageBox4.Text = viewArray[3].time + viewArray[3].message;
            if (viewArray[3].type == "send") MessageBox4.Background = Brushes.LightSkyBlue;
            else MessageBox4.Background = Brushes.LightGray;
            MessageBox5.Text = viewArray[4].time + viewArray[4].message;
            if (viewArray[4].type == "send") MessageBox5.Background = Brushes.LightSkyBlue;
            else MessageBox5.Background = Brushes.LightGray;
            MessageBox6.Text = viewArray[5].time + viewArray[5].message;
            if (viewArray[5].type == "send") MessageBox6.Background = Brushes.LightSkyBlue;
            else MessageBox6.Background = Brushes.LightGray;
            MessageBox7.Text = viewArray[6].time + viewArray[6].message;
            if (viewArray[6].type == "send") MessageBox7.Background = Brushes.LightSkyBlue;
            else MessageBox7.Background = Brushes.LightGray;
            MessageBox8.Text = viewArray[7].time + viewArray[7].message;
            if (viewArray[7].type == "send") MessageBox8.Background = Brushes.LightSkyBlue;
            else MessageBox8.Background = Brushes.LightGray;
            MessageBox9.Text = viewArray[8].time + viewArray[8].message;
            if (viewArray[8].type == "send") MessageBox9.Background = Brushes.LightSkyBlue;
            else MessageBox9.Background = Brushes.LightGray;
            MessageBox10.Text = viewArray[9].time + viewArray[9].message;
            if (viewArray[9].type == "send") MessageBox10.Background = Brushes.LightSkyBlue;
            else MessageBox10.Background = Brushes.LightGray;

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

        private void ScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
