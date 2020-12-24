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
    /// Interaction logic for ContactsWindow.xaml
    /// </summary>
    public partial class ContactsWindow : Window
    {
        Contacts myContacts = null;
        Logger myLogger = null;
        // Row states: 1 is edit mode, 0 shows edit button
        int row1State = 0;
        int row2State = 0;
        int row3State = 0;
        int row4State = 0;
        int row5State = 0;
        int row6State = 0;
        int row7State = 0;
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
                myLogger.WriteLogMessage("Contacts Saved");
            }

        }


    }
}
