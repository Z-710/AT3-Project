using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

/// <Acknowledgments>
/// How to create a singleton class
/// https://refactoring.guru/design-patterns/singleton/csharp/example
/// Reading and writing xml files
/// https://www.youtube.com/watch?v=0huAY0KJdI8
/// https://www.youtube.com/watch?v=HC60-OWSYuk
/// Putting data files in the user application path
/// https://stackoverflow.com/questions/867485/c-sharp-getting-the-path-of-appdata
/// How to use git ignore
/// https://youtu.be/ErJyWO8TGoM
/// </Acknowledgments>
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
                string encryptedAdmin = Encryption.Encrypt("admin123", null);
                Xtw.WriteElementString("pwd", encryptedAdmin);
                Xtw.WriteEndElement();
                Xtw.WriteStartElement("p2");
                Xtw.WriteElementString("type","user");
                string encryptedPassword = Encryption.Encrypt(pwd, null);
                Xtw.WriteElementString("pwd", encryptedPassword);
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
        public bool CheckPassword(string pwd)
        {
            string userType = "";
            string userPwd = "";
            bool pwdMatched = false;
            // Check to see if password file exists
            if (Xtr == null) ReadPasswordFile();
            if (Xtr == null)
            {
                return false;
            }
            else
            {

                // Check for password with type user
                while (Xtr.Read())
                {
                    if ((Xtr.NodeType == XmlNodeType.Element) && (Xtr.Name == "type"))
                    {
                        userType = Xtr.ReadElementString();
                        if ((userType == "user") || (userType == "admin"))
                        {
                            // Check the password matches
                            Xtr.Read();
                            if ((Xtr.NodeType == XmlNodeType.Element) && (Xtr.Name == "pwd"))
                            {
                                userPwd = Xtr.ReadElementString();
                                string decryptedPassword = Encryption.Decrypt(userPwd, null);
                                if (pwd == decryptedPassword) pwdMatched = true;
                            }
                        }
                    }
                }
            }
            Xtr.Close();
            Xtr = null;
            return pwdMatched;
        }
        public bool UserPasswordExists()
        {
            string userType = "";
            bool userPwdExists = false;
            // Check to see if password file exists
            if (Xtr == null) ReadPasswordFile();
            if (Xtr == null)
            {
                return false;
            }
            else
            {

                // Check for password with type user
                while (Xtr.Read())
                {
                    if ((Xtr.NodeType == XmlNodeType.Element) && (Xtr.Name == "type"))
                    {
                        userType = Xtr.ReadElementString();
                        if (userType == "user")
                        {
                            userPwdExists = true;
                        }
                    }
                }
                Xtr.Close();
                Xtr = null;
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
