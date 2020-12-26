using System;
using System.IO;
using System.Text;
using System.Xml;

namespace AT3
{
    class Messages
    {
        // Singleton object required
        private static Messages _instance = null;
        // Fixed number of max Messages 
        public const int numMessages = 1000;
        // Store the selected contact passed into this class
        private static int selectedContact = 0;
        private static string messageFile = "";
        // Define array of message structures that will be used by the Messages window
        public struct Messagestruct
        {
            public string type;
            public string message;
            public string time;
        }
        public static Messagestruct[] MessageArray = new Messagestruct[numMessages];
        // Use GetInstance function to get access to the single object  
        public static Messages GetInstance(int contact)
        {
            if (_instance == null)
            {
                _instance = new Messages();
            }
            selectedContact = contact;
            messageFile = "contact" + selectedContact.ToString() + "messages.xml";
            return _instance;
        }
        private Messages()
        {

        }
        ~Messages()
        {

        }
        public void WriteMessages()
        {
            // Messages file created under %appdata% which is C:\Users\<CurrentUser>\AppData\Roaming
            var AT3folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AT3");
            var Chatterpillarfolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AT3\\Chatterpillar");
            var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AT3\\Chatterpillar\\" + messageFile);
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
                // Write the Messages given by user to Messages file
                Xtw = new XmlTextWriter(fileName, Encoding.UTF8);
                Xtw.Formatting = Formatting.Indented;
                Xtw.WriteStartDocument();
                Xtw.WriteComment("Chatterpillar Messages File");
                Xtw.WriteStartElement("Messages");
                for (arrayIndex = 0; arrayIndex < numMessages; arrayIndex++)
                {
                    Xtw.WriteStartElement("c" + (arrayIndex + 1).ToString());
                    Xtw.WriteElementString("name", Messages.MessageArray[arrayIndex].type);
                    Xtw.WriteElementString("IP", Messages.MessageArray[arrayIndex].message);
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


        public void ReadMessages()
        {
            // Initialise the Messages array
            int arrayIndex;
            for (arrayIndex = 0; arrayIndex < numMessages; arrayIndex++)
            {
                MessageArray[arrayIndex].type = "";
                MessageArray[arrayIndex].message = "";
                MessageArray[arrayIndex].time = "";
            }
            // Messages file created under %appdata% which is C:\Users\<CurrentUser>\AppData\Roaming
            var AT3folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AT3");
            var Chatterpillarfolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AT3\\Chatterpillar");
            var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AT3\\Chatterpillar\\" + messageFile);
            // Xml Reader object and Xml fields
            XmlTextReader Xtr = null;
            string messageType = "";
            string messageMessage = "";
            string messageTime = "";
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
                // See if Messages File exists 
                if (File.Exists(fileName))
                {
                    // Open the file in the xml text reader
                    Xtr = new XmlTextReader(fileName);
                    // Populate the Messages array
                    arrayIndex = 0;
                    while (Xtr.Read())
                    {
                        if ((Xtr.NodeType == XmlNodeType.Element) && (Xtr.Name == "type"))
                        {
                            messageType = Xtr.ReadElementString();
                            Xtr.Read();
                            if ((Xtr.NodeType == XmlNodeType.Element) && (Xtr.Name == "message"))
                            {
                                messageMessage = Xtr.ReadElementString();
                                Xtr.Read();
                                if ((Xtr.NodeType == XmlNodeType.Element) && (Xtr.Name == "time"))
                                {
                                    messageTime = Xtr.ReadElementString();
                                    if (arrayIndex < numMessages)
                                    {
                                        // Populate message element
                                        MessageArray[arrayIndex].type = messageType;
                                        MessageArray[arrayIndex].message = messageMessage;
                                        MessageArray[arrayIndex].time = messageTime;
                                        arrayIndex++;
                                    }
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
