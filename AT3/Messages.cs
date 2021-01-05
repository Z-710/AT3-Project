﻿using System;
using System.IO;
using System.Text;
using System.Xml;

namespace AT3
{
    class Messages
    {
        // Singleton object required for each contact
        private static Messages _instance = null;
        // Fixed number of max Messages 
        public const int numMessages = 50;
        // Define array of message structures that will be used by the Messages window
        public struct Messagestruct
        {
            public string type;
            public string message;
            public string time;
        }
        public static Messagestruct[,] MessageArray = new Messagestruct[numMessages, Contacts.numContacts];
        // Define array of per contact information
        public struct perContactInfoStruct
        {
            public string messageFile;
            public int newestMessage;
        }
        public static perContactInfoStruct[] perContactInfo = new perContactInfoStruct[Contacts.numContacts];
        // Use GetInstance function to get access to the single object  
        public static Messages GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Messages();
                // Initialise the Message arrays
                for (int contactNum = 0; contactNum < Contacts.numContacts; contactNum++)
                {
                    for (int arrayIndex = 0; arrayIndex < numMessages; arrayIndex++)
                    {
                        MessageArray[arrayIndex, contactNum].type = "";
                        MessageArray[arrayIndex, contactNum].message = "";
                        MessageArray[arrayIndex, contactNum].time = "";
                    }
                }
                // Setup the per contact default information
                for (int contactNum = 0; contactNum < Contacts.numContacts; contactNum++)
                {
                    perContactInfo[contactNum].newestMessage = 0; 
                    perContactInfo[contactNum].messageFile =  "contact" + (contactNum).ToString()  + "messages.xml";
                    // Read the messages file for this contact
                    ReadMessages(contactNum);
                }

                return _instance;
            }
            else
            {
                return _instance;
            }

        }
        private Messages()
        {

        }
        ~Messages()
        {

        }
        public void WriteMessages(int contact)
        {
            // Messages file created under %appdata% which is C:\Users\<CurrentUser>\AppData\Roaming
            var AT3folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AT3");
            var Chatterpillarfolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AT3\\Chatterpillar");
            var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AT3\\Chatterpillar\\" + perContactInfo[contact].messageFile);
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
                for (arrayIndex = 0; arrayIndex <= perContactInfo[contact].newestMessage; arrayIndex++)
                {
                    Xtw.WriteStartElement("m" + (arrayIndex + 1).ToString());
                    Xtw.WriteElementString("type", Messages.MessageArray[arrayIndex,contact].type);
                    string encryptedMessage = Encryption.Encrypt(Messages.MessageArray[arrayIndex, contact].message, null);
                    Xtw.WriteElementString("message", encryptedMessage);
                    Xtw.WriteElementString("time", Messages.MessageArray[arrayIndex, contact].time);
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


        private static void ReadMessages(int contact)
        {
            // Messages file created under %appdata% which is C:\Users\<CurrentUser>\AppData\Roaming
            var AT3folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AT3");
            var Chatterpillarfolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AT3\\Chatterpillar");
            var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AT3\\Chatterpillar\\" + perContactInfo[contact].messageFile);
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
                    int arrayIndex = 0;
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
                                        MessageArray[arrayIndex, contact].type = messageType;
                                        string decryptedMessage = Encryption.Decrypt(messageMessage, null);
                                        MessageArray[arrayIndex, contact].message = decryptedMessage;
                                        MessageArray[arrayIndex, contact].time = messageTime;
                                        // Set the most recent message 
                                        perContactInfo[contact].newestMessage = arrayIndex;
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
        public void GetMessages(int cursor, int numMsgs, ref Messagestruct[] msgArray, int contact)
        {
            // Clear out the message array
            for (int i = 0; i < numMsgs; i++)
            {
                msgArray[i].type = "";
                msgArray[i].message = "";
                msgArray[i].time = "";
            }
            // Work through the array starting at the cursor position and return most recent messages
            for (int i = 0; i < numMsgs && i < cursor; i++)
            {
                msgArray[i] = MessageArray[cursor - i, contact];
            }    
        }
        public void AddMessage(Messagestruct msg, int contact)
        {
            // Add a new message to the messages array, shuffling down elements if full
            if (perContactInfo[contact].newestMessage == (numMessages - 1))
            {
                for (int i = 0; i < (numMessages - 1); i++)
                {
                    MessageArray[i, contact] = MessageArray[i + 1, contact];
                }
            }
            else
            {
                perContactInfo[contact].newestMessage++;
            }
            MessageArray[perContactInfo[contact].newestMessage, contact] = msg;
        }
    }
}
