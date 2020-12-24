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
        public ContactsWindow()
        {
            InitializeComponent();
            DoneButton.Visibility = Visibility.Hidden;
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

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void EditButton1_Click(object sender, RoutedEventArgs e)
        {
            NameTextBox1.IsReadOnly = false;
            IPTextBox1.IsReadOnly = false;
            EditButton1.IsEnabled = false;
            DoneButton.Visibility = Visibility.Visible; 
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            NameTextBox1.IsReadOnly = true;
            IPTextBox1.IsReadOnly = true;
            EditButton1.IsEnabled = true;
            DoneButton.Visibility = Visibility.Hidden;
        }
    }
}
