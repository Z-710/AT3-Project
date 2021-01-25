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
    /// Interaction logic for ConnectDialog.xaml
    /// </summary>
    public partial class ConnectDialog : Window
    {
        public ConnectDialog()
        {
            InitializeComponent();
            DialogTextBox.Text = Contacts.ContactArray[Contacts.selectedContact].name + " has connected to you. Do you want to chat?" ;
        }
        // The accept button is a button whose IsDefault property is set to true. This event is raised whenever this button is clicked, or the ENTER key is pressed.
        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void IgnoreButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
