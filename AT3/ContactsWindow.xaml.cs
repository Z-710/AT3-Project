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
using System.Threading;
using System.ComponentModel;


namespace AT3
{
    /// <summary>
    /// Interaction logic for ContactsWindow.xaml
    /// </summary>
    public partial class ContactsWindow : Window
    {
        Contacts myContacts = null;
        Logger myLogger = null;
        Settings mySettings = null;
        bool nextWindow = false;
        // Row states: 1 is edit mode, 0 shows edit button
        int row1State = 0;
        int row2State = 0;
        int row3State = 0;
        int row4State = 0;
        int row5State = 0;
        int row6State = 0;
        int row7State = 0;
        int portState = 0;
        public ContactsWindow()
        {
            InitializeComponent();
            // Initialise the Logger
            myLogger = Logger.GetInstance();
            // Initialise the Contacts
            myContacts = Contacts.GetInstance();
            myLogger.WriteLogMessage("Contacts Initialised");
            myContacts.ReadContacts();
            // Populate contacts form
            NameTextBox1.Text = Contacts.ContactArray[0].name;
            NameTextBox2.Text = Contacts.ContactArray[1].name;
            NameTextBox3.Text = Contacts.ContactArray[2].name;
            NameTextBox4.Text = Contacts.ContactArray[3].name;
            NameTextBox5.Text = Contacts.ContactArray[4].name;
            NameTextBox6.Text = Contacts.ContactArray[5].name;
            NameTextBox7.Text = Contacts.ContactArray[6].name;
            IPTextBox1.Text = Contacts.ContactArray[0].IPAddress;
            IPTextBox2.Text = Contacts.ContactArray[1].IPAddress;
            IPTextBox3.Text = Contacts.ContactArray[2].IPAddress;
            IPTextBox4.Text = Contacts.ContactArray[3].IPAddress;
            IPTextBox5.Text = Contacts.ContactArray[4].IPAddress;
            IPTextBox6.Text = Contacts.ContactArray[5].IPAddress;
            IPTextBox7.Text = Contacts.ContactArray[6].IPAddress;
            //Change textboxes to have no borders
            NameTextBox1.BorderBrush = Brushes.White;
            NameTextBox2.BorderBrush = Brushes.White;
            NameTextBox3.BorderBrush = Brushes.White;
            NameTextBox4.BorderBrush = Brushes.White;
            NameTextBox5.BorderBrush = Brushes.White;
            NameTextBox6.BorderBrush = Brushes.White;
            NameTextBox7.BorderBrush = Brushes.White;
            IPTextBox1.BorderBrush = Brushes.White;
            IPTextBox2.BorderBrush = Brushes.White;
            IPTextBox3.BorderBrush = Brushes.White;
            IPTextBox4.BorderBrush = Brushes.White;
            IPTextBox5.BorderBrush = Brushes.White;
            IPTextBox6.BorderBrush = Brushes.White;
            IPTextBox7.BorderBrush = Brushes.White;
            // Initialise the Settings
            mySettings = Settings.GetInstance();
            myLogger.WriteLogMessage("Settings Initialised");
            mySettings.ReadSettings();
            // Populate Settings form
            PortBox.Text = Settings.settingsStructure.port;
            // Setup the comms server
            // Create the thread object, passing in the
            // serverObject.RunServer method using a ThreadStart delegate.
            if (Contacts.CommsServerStarted == false)
            {
                Contacts.CommsServerStarted = true;
                CommsServer serverObject = new CommsServer(Settings.settingsStructure.port);
                Thread InstanceCaller = new Thread(
                    new ThreadStart(serverObject.RunServer));
                // Start the thread.
                InstanceCaller.Start();
                myLogger.WriteLogMessage("CommsServer Initialised");
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void EditButton1_Click(object sender, RoutedEventArgs e)
        {
            if (row1State == 0)
            {
                //When in edit button mode
                NameTextBox1.IsReadOnly = false;
                IPTextBox1.IsReadOnly = false;
                NameTextBox1.BorderBrush = Brushes.Gray;
                IPTextBox1.BorderBrush = Brushes.Gray;
                EditButton1.Content = "Save";
                row1State++;
                EditButton1_Copy.IsEnabled = false;
                EditButton3.IsEnabled = false;
                EditButton4.IsEnabled = false;
                EditButton5.IsEnabled = false;
                EditButton6.IsEnabled = false;
                EditButton7.IsEnabled = false;
            }
            else
            {
                //when in save button mode
                NameTextBox1.IsReadOnly = true;
                IPTextBox1.IsReadOnly = true;
                NameTextBox1.BorderBrush = Brushes.White;
                IPTextBox1.BorderBrush = Brushes.White;
                EditButton1.Content = "Edit";
                row1State--;
                //Update the array with updated details and write to disk
                Contacts.ContactArray[0].name = NameTextBox1.Text;
                Contacts.ContactArray[0].IPAddress = IPTextBox1.Text;
                myContacts.WriteContacts();
                myLogger.WriteLogMessage("Contact 1 Saved");
                EditButton1_Copy.IsEnabled = true;
                EditButton3.IsEnabled = true;
                EditButton4.IsEnabled = true;
                EditButton5.IsEnabled = true;
                EditButton6.IsEnabled = true;
                EditButton7.IsEnabled = true;
            }

        }

        private void EditButton1_Copy_Click(object sender, RoutedEventArgs e)
        {

            if (row2State == 0)
            {
                //When in edit button mode
                NameTextBox2.IsReadOnly = false;
                IPTextBox2.IsReadOnly = false;
                NameTextBox2.BorderBrush = Brushes.Gray;
                IPTextBox2.BorderBrush = Brushes.Gray;
                EditButton1_Copy.Content = "Save";
                row2State++;
                EditButton1.IsEnabled = false;
                EditButton3.IsEnabled = false;
                EditButton4.IsEnabled = false;
                EditButton5.IsEnabled = false;
                EditButton6.IsEnabled = false;
                EditButton7.IsEnabled = false;
            }
            else
            {
                //when in save button mode
                NameTextBox2.IsReadOnly = true;
                IPTextBox2.IsReadOnly = true;
                NameTextBox2.BorderBrush = Brushes.White;
                IPTextBox2.BorderBrush = Brushes.White;
                EditButton1_Copy.Content = "Edit";
                row2State--;
                //Update the array with updated details and write to disk
                Contacts.ContactArray[1].name = NameTextBox2.Text;
                Contacts.ContactArray[1].IPAddress = IPTextBox2.Text;
                myContacts.WriteContacts();
                myLogger.WriteLogMessage("Contact 2 Saved");
                EditButton1.IsEnabled = true;
                EditButton3.IsEnabled = true;
                EditButton4.IsEnabled = true;
                EditButton5.IsEnabled = true;
                EditButton6.IsEnabled = true;
                EditButton7.IsEnabled = true;
            }
        }

        private void EditButton3_Click(object sender, RoutedEventArgs e)
        {

            if (row3State == 0)
            {
                //When in edit button mode
                NameTextBox3.IsReadOnly = false;
                IPTextBox3.IsReadOnly = false;
                NameTextBox3.BorderBrush = Brushes.Gray;
                IPTextBox3.BorderBrush = Brushes.Gray;
                EditButton3.Content = "Save";
                row3State++;
                EditButton1.IsEnabled = false;
                EditButton1_Copy.IsEnabled = false;
                EditButton4.IsEnabled = false;
                EditButton5.IsEnabled = false;
                EditButton6.IsEnabled = false;
                EditButton7.IsEnabled = false;
            }
            else
            {
                //when in save button mode
                NameTextBox3.IsReadOnly = true;
                IPTextBox3.IsReadOnly = true;
                NameTextBox3.BorderBrush = Brushes.White;
                IPTextBox3.BorderBrush = Brushes.White;
                EditButton3.Content = "Edit";
                row3State--;
                //Update the array with updated details and write to disk
                Contacts.ContactArray[2].name = NameTextBox3.Text;
                Contacts.ContactArray[2].IPAddress = IPTextBox3.Text;
                myContacts.WriteContacts();
                myLogger.WriteLogMessage("Contact 3 Saved");
                EditButton1.IsEnabled = true;
                EditButton1_Copy.IsEnabled = true;
                EditButton4.IsEnabled = true;
                EditButton5.IsEnabled = true;
                EditButton6.IsEnabled = true;
                EditButton7.IsEnabled = true;
            }
        }

        private void EditButton4_Click(object sender, RoutedEventArgs e)
        {

            if (row4State == 0)
            {
                //When in edit button mode
                NameTextBox4.IsReadOnly = false;
                IPTextBox4.IsReadOnly = false;
                NameTextBox4.BorderBrush = Brushes.Gray;
                IPTextBox4.BorderBrush = Brushes.Gray;
                EditButton4.Content = "Save";
                row4State++;
                EditButton1.IsEnabled = false;
                EditButton1_Copy.IsEnabled = false;
                EditButton3.IsEnabled = false;
                EditButton5.IsEnabled = false;
                EditButton6.IsEnabled = false;
                EditButton7.IsEnabled = false;
            }
            else
            {
                //when in save button mode
                NameTextBox4.IsReadOnly = true;
                IPTextBox4.IsReadOnly = true;
                NameTextBox4.BorderBrush = Brushes.White;
                IPTextBox4.BorderBrush = Brushes.White;
                EditButton4.Content = "Edit";
                row4State--;
                //Update the array with updated details and write to disk
                Contacts.ContactArray[3].name = NameTextBox4.Text;
                Contacts.ContactArray[3].IPAddress = IPTextBox4.Text;
                myContacts.WriteContacts();
                myLogger.WriteLogMessage("Contact 4 Saved");
                EditButton1.IsEnabled = true;
                EditButton1_Copy.IsEnabled = true;
                EditButton3.IsEnabled = true;
                EditButton5.IsEnabled = true;
                EditButton6.IsEnabled = true;
                EditButton7.IsEnabled = true;
            }
        }

        private void EditButton5_Click(object sender, RoutedEventArgs e)
        {

            if (row5State == 0)
            {
                //When in edit button mode
                NameTextBox5.IsReadOnly = false;
                IPTextBox5.IsReadOnly = false;
                NameTextBox5.BorderBrush = Brushes.Gray;
                IPTextBox5.BorderBrush = Brushes.Gray;
                EditButton5.Content = "Save";
                row5State++;
                EditButton1.IsEnabled = false;
                EditButton1_Copy.IsEnabled = false;
                EditButton3.IsEnabled = false;
                EditButton4.IsEnabled = false;
                EditButton6.IsEnabled = false;
                EditButton7.IsEnabled = false;
            }
            else
            {
                //when in save button mode
                NameTextBox5.IsReadOnly = true;
                IPTextBox5.IsReadOnly = true;
                NameTextBox5.BorderBrush = Brushes.White;
                IPTextBox5.BorderBrush = Brushes.White;
                EditButton5.Content = "Edit";
                row5State--;
                //Update the array with updated details and write to disk
                Contacts.ContactArray[4].name = NameTextBox5.Text;
                Contacts.ContactArray[4].IPAddress = IPTextBox5.Text;
                myContacts.WriteContacts();
                myLogger.WriteLogMessage("Contact 5 Saved");
                EditButton1.IsEnabled = true;
                EditButton1_Copy.IsEnabled = true;
                EditButton3.IsEnabled = true;
                EditButton4.IsEnabled = true;
                EditButton6.IsEnabled = true;
                EditButton7.IsEnabled = true;
            }
        }

        private void EditButton6_Click(object sender, RoutedEventArgs e)
        {

            if (row6State == 0)
            {
                //When in edit button mode
                NameTextBox6.IsReadOnly = false;
                IPTextBox6.IsReadOnly = false;
                NameTextBox6.BorderBrush = Brushes.Gray;
                IPTextBox6.BorderBrush = Brushes.Gray;
                EditButton6.Content = "Save";
                row6State++;
                EditButton1.IsEnabled = false;
                EditButton1_Copy.IsEnabled = false;
                EditButton3.IsEnabled = false;
                EditButton4.IsEnabled = false;
                EditButton5.IsEnabled = false;
                EditButton7.IsEnabled = false;
            }
            else
            {
                //when in save button mode
                NameTextBox6.IsReadOnly = true;
                IPTextBox6.IsReadOnly = true;
                NameTextBox6.BorderBrush = Brushes.White;
                IPTextBox6.BorderBrush = Brushes.White;
                EditButton6.Content = "Edit";
                row6State--;
                //Update the array with updated details and write to disk
                Contacts.ContactArray[5].name = NameTextBox6.Text;
                Contacts.ContactArray[5].IPAddress = IPTextBox6.Text;
                myContacts.WriteContacts();
                myLogger.WriteLogMessage("Contact 6 Saved");
                EditButton1.IsEnabled = true;
                EditButton1_Copy.IsEnabled = true;
                EditButton3.IsEnabled = true;
                EditButton4.IsEnabled = true;
                EditButton5.IsEnabled = true;
                EditButton7.IsEnabled = true;
            }
        }

        private void EditButton7_Click(object sender, RoutedEventArgs e)
        {

            if (row7State == 0)
            {
                //When in edit button mode
                NameTextBox7.IsReadOnly = false;
                IPTextBox7.IsReadOnly = false;
                NameTextBox7.BorderBrush = Brushes.Gray;
                IPTextBox7.BorderBrush = Brushes.Gray;
                EditButton7.Content = "Save";
                row7State++;
                EditButton1.IsEnabled = false;
                EditButton1_Copy.IsEnabled = false;
                EditButton3.IsEnabled = false;
                EditButton4.IsEnabled = false;
                EditButton5.IsEnabled = false;
                EditButton6.IsEnabled = false;
            }
            else
            {
                //when in save button mode
                NameTextBox7.IsReadOnly = true;
                IPTextBox7.IsReadOnly = true;
                NameTextBox7.BorderBrush = Brushes.White;
                IPTextBox7.BorderBrush = Brushes.White;
                EditButton7.Content = "Edit";
                row7State--;
                //Update the array with updated details and write to disk
                Contacts.ContactArray[6].name = NameTextBox7.Text;
                Contacts.ContactArray[6].IPAddress = IPTextBox7.Text;
                myContacts.WriteContacts();
                myLogger.WriteLogMessage("Contact 7 Saved");
                EditButton1.IsEnabled = true;
                EditButton1_Copy.IsEnabled = true;
                EditButton3.IsEnabled = true;
                EditButton4.IsEnabled = true;
                EditButton5.IsEnabled = true;
                EditButton6.IsEnabled = true;
            }
        }

        private void PortButton_Click(object sender, RoutedEventArgs e)
        {
            if (portState == 0)
            {
                PortBox.IsReadOnly = false;
                portState++;
                PortButton.Content = "Save";
            }
            else
            {
                Settings.settingsStructure.port = PortBox.Text;
                mySettings.WriteSettings();
                myLogger.WriteLogMessage("Port Saved");
                PortBox.IsReadOnly = true;
                portState--;
                PortButton.Content = "Change";
            }
        }

        private void ContactButton1_Click(object sender, RoutedEventArgs e)
        {
            ContactButton1.Background = Brushes.LightSkyBlue;
            ContactButton2.Background = Brushes.White;
            ContactButton3.Background = Brushes.White;
            ContactButton4.Background = Brushes.White;
            ContactButton5.Background = Brushes.White;
            ContactButton6.Background = Brushes.White;
            ContactButton7.Background = Brushes.White;
            Contacts.selectedContact = 1;
        }

        private void ContactButton2_Click(object sender, RoutedEventArgs e)
        {
            ContactButton2.Background = Brushes.LightSkyBlue;
            ContactButton1.Background = Brushes.White;
            ContactButton3.Background = Brushes.White;
            ContactButton4.Background = Brushes.White;
            ContactButton5.Background = Brushes.White;
            ContactButton6.Background = Brushes.White;
            ContactButton7.Background = Brushes.White;
            Contacts.selectedContact = 2;
        }

        private void ContactButton3_Click(object sender, RoutedEventArgs e)
        {
            ContactButton3.Background = Brushes.LightSkyBlue;
            ContactButton1.Background = Brushes.White;
            ContactButton2.Background = Brushes.White;
            ContactButton4.Background = Brushes.White;
            ContactButton5.Background = Brushes.White;
            ContactButton6.Background = Brushes.White;
            ContactButton7.Background = Brushes.White;
            Contacts.selectedContact = 3;
        }

        private void ContactButton4_Click(object sender, RoutedEventArgs e)
        {
            ContactButton4.Background = Brushes.LightSkyBlue;
            ContactButton1.Background = Brushes.White;
            ContactButton3.Background = Brushes.White;
            ContactButton2.Background = Brushes.White;
            ContactButton5.Background = Brushes.White;
            ContactButton6.Background = Brushes.White;
            ContactButton7.Background = Brushes.White;
            Contacts.selectedContact = 4;
        }

        private void ContactButton5_Click(object sender, RoutedEventArgs e)
        {
            ContactButton5.Background = Brushes.LightSkyBlue;
            ContactButton1.Background = Brushes.White;
            ContactButton3.Background = Brushes.White;
            ContactButton4.Background = Brushes.White;
            ContactButton2.Background = Brushes.White;
            ContactButton6.Background = Brushes.White;
            ContactButton7.Background = Brushes.White;
            Contacts.selectedContact = 5;
        }

        private void ContactButton6_Click(object sender, RoutedEventArgs e)
        {
            ContactButton6.Background = Brushes.LightSkyBlue;
            ContactButton1.Background = Brushes.White;
            ContactButton3.Background = Brushes.White;
            ContactButton4.Background = Brushes.White;
            ContactButton5.Background = Brushes.White;
            ContactButton2.Background = Brushes.White;
            ContactButton7.Background = Brushes.White;
            Contacts.selectedContact = 6;
        }

        private void ContactButton7_Click(object sender, RoutedEventArgs e)
        {
            ContactButton7.Background = Brushes.LightSkyBlue;
            ContactButton1.Background = Brushes.White;
            ContactButton3.Background = Brushes.White;
            ContactButton4.Background = Brushes.White;
            ContactButton5.Background = Brushes.White;
            ContactButton6.Background = Brushes.White;
            ContactButton2.Background = Brushes.White;
            Contacts.selectedContact = 7;
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            myLogger.WriteLogMessage("Selected Contact: " + Contacts.selectedContact.ToString());
            if (Contacts.selectedContact > 0)
            {
                nextWindow = true;
                WritingWindow wW = new WritingWindow();
                wW.Show();
                this.Close();
            }
        }
        private void ContactsWindow_Closing(object sender, CancelEventArgs e)
        {
            if (nextWindow == false)
            {
                myLogger.WriteLogMessage("Contacts Login Window");
                //Close the application
                Environment.Exit(0);
            }
        }
    }
}
