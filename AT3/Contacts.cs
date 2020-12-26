using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Threading.Tasks;

namespace AT3
{
    class Contacts
    {
        
        // Singleton object required
        private static Contacts _instance = null;
        // Fixed number of max contacts 
        public const int numContacts = 7;
        // Define array of contact structures that will be used by the contacts window
        public struct ContactStruct
        {
            public string name;
            public string IPAddress;
        }
        public static ContactStruct[] ContactArray = new ContactStruct[numContacts];
        public static int selectedContact = 0;
       
        // Use GetInstance function to get access to the single object  
        public static Contacts GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Contacts();
            }
            return _instance;
        }
        private Contacts()
        {
            // Initialise the contacts array
            int arrayIndex;
            for (arrayIndex = 0; arrayIndex < numContacts; arrayIndex++)
            {
                ContactArray[arrayIndex].name = "";
                ContactArray[arrayIndex].IPAddress = "";
            }

        }
        ~Contacts()
        {

        }
        public void WriteContacts()
        {
            // Contacts file created under %appdata% which is C:\Users\<CurrentUser>\AppData\Roaming
            var AT3folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AT3");
            var Chatterpillarfolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AT3\\Chatterpillar");
            var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AT3\\Chatterpillar\\contacts.xml");
            XmlTextWriter Xtw = null;
            int arrayIndex = 0;
            try
            {
                // Create the AT3 and Chatterpillar folders if they do not exist
                // Determine whether the directory exists.
                if (Directory.Exists(AT3folder))
                {

                }
                else
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(AT3folder);
                }
                if (Directory.Exists(Chatterpillarfolder))
                {

                }
                else
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(Chatterpillarfolder);
                }
                // Write the Contacts given by user to Contacts file
                Xtw = new XmlTextWriter(fileName, Encoding.UTF8);
                Xtw.Formatting = Formatting.Indented;
                Xtw.WriteStartDocument();
                Xtw.WriteComment("Chatterpillar Contacts File");
                Xtw.WriteStartElement("Contacts");
                for (arrayIndex = 0; arrayIndex < numContacts; arrayIndex++)
                {
                    Xtw.WriteStartElement("c" + (arrayIndex + 1).ToString());
                    Xtw.WriteElementString("name", Contacts.ContactArray[arrayIndex].name);
                    Xtw.WriteElementString("IP", Contacts.ContactArray[arrayIndex].IPAddress);
                    Xtw.WriteEndElement();
                }
                Xtw.WriteEndElement();
                Xtw.WriteEndDocument();
                Xtw.Flush();
                Xtw.Close();
                Xtw = null;
            }
            catch (Exception ex)
            {

            }

        }
        

        public void ReadContacts()
        {

            // Contacts file created under %appdata% which is C:\Users\<CurrentUser>\AppData\Roaming
            var AT3folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AT3");
            var Chatterpillarfolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AT3\\Chatterpillar");
            var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AT3\\Chatterpillar\\contacts.xml");
            // Xml Reader object and Xml fields
            XmlTextReader Xtr = null;
            string contactName = "";
            string contactIP = "";
            try
            {
                // Create the AT3 and Chatterpillar folders if they do not exist
                // Determine whether the directory exists.
                if (Directory.Exists(AT3folder))
                {

                }
                else
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(AT3folder);
                }
                if (Directory.Exists(Chatterpillarfolder))
                {

                }
                else
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(Chatterpillarfolder);
                }
                // See if Contacts File exists 
                if (File.Exists(fileName))
                {
                    // Open the file in the xml text reader
                    Xtr = new XmlTextReader(fileName);
                    // Populate the contacts array
                    int arrayIndex = 0; 
                    while (Xtr.Read())
                    {
                        if ((Xtr.NodeType == XmlNodeType.Element) && (Xtr.Name == "name"))
                        {
                            contactName = Xtr.ReadElementString();
                            Xtr.Read();
                            if ((Xtr.NodeType == XmlNodeType.Element) && (Xtr.Name == "IP"))
                            {
                                contactIP = Xtr.ReadElementString();
                                if (arrayIndex < numContacts)
                                {
                                    // Populate array element
                                    ContactArray[arrayIndex].name = contactName;
                                    ContactArray[arrayIndex].IPAddress = contactIP;
                                    arrayIndex++;
                                }
                            }
                        }
         

                    }
                    Xtr.Close();
                    Xtr = null;
                }
                else
                {
                    // Ensure XmlTextReader is Null
                    Xtr = null;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
