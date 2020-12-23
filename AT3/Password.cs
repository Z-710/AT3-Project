using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace AT3
{
    class Password
    {
        // Singleton object required
        private static Password _instance = null;
        // Our Xml objects 
        private static XmlTextReader Xtr = null;
        private static XmlTextWriter Xtw = null;
        // Define a type for both password types
        public enum PasswordType
        {
            Admin,
            User
        }
        // Use GetInstance function to get access to the single object  
        public static Password GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Password();
            }
            return _instance;
        }
        private Password()
        {


        }
        ~Password()
        {

        }
        public void WritePassword(string pwd, PasswordType type)
        {
            // Password file created under %appdata% which is C:\Users\<CurrentUser>\AppData\Roaming
            var AT3folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AT3");
            var Chatterpillarfolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AT3\\Chatterpillar");
            var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AT3\\Chatterpillar\\password.xml");

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
                // Write the password given by user to password file
                Xtw = new XmlTextWriter(fileName, Encoding.UTF8);
                Xtw.Formatting = Formatting.Indented;
                Xtw.WriteStartDocument();
                Xtw.WriteComment("Chatterpillar Password File");
                Xtw.WriteStartElement("Password");
                Xtw.WriteStartElement("p1");
                Xtw.WriteElementString("type", "admin");
                Xtw.WriteElementString("pwd", "admin123");
                Xtw.WriteEndElement();
                Xtw.WriteStartElement("p2");
                Xtw.WriteElementString("type","user");
                Xtw.WriteElementString("pwd", pwd);
                Xtw.WriteEndElement();
                Xtw.WriteEndElement();
                Xtw.WriteEndDocument();
                Xtw.Flush();
                Xtw.Close();
            }
            catch (Exception ex)
            {

            }

        }
        public void CheckPassword(string pwd, PasswordType type)
        {


        }
        public bool UserPasswordExists()
        {
            string userType = "";
            bool userPwdExists = false;
            // Check to see if password file exists
            ReadPasswordFile();
            if (Xtr == null)
            {
                return false;
            }
            else
            {
                // Check for password with type user
                while (Xtr.Read())
                {
                    if ((Xtr.NodeType == XmlNodeType.Element) && (Xtr.Name == "Type"))
                    {
                        userType = Xtr.ReadElementString();
                        if (userType == "user")
                        {
                            userPwdExists = true;
                        }
                    }
                }
                return userPwdExists;
            }
        }
        private void ReadPasswordFile()
        {
            // Password file created under %appdata% which is C:\Users\<CurrentUser>\AppData\Roaming
            var AT3folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AT3");
            var Chatterpillarfolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AT3\\Chatterpillar");
            var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AT3\\Chatterpillar\\password.xml");

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
                // See if Password File exists 
                if (File.Exists(fileName))
                {
                    // Open the file in the xml text reader
                    Xtr = new XmlTextReader(fileName);

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
