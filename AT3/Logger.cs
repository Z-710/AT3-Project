using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace AT3
{
    class Logger
    {
        private FileStream fs;

        public Logger()
        {
            //Constructor - Create and open the log file
            OpenLogFile();
            
        }
        ~Logger()
        {
            //Destructor - Close the log file 
            CloseLogFile();
        }
        public void WriteLogMessage(string logMessage)
        {
            DateTime now = DateTime.Now;
            byte[] info = new UTF8Encoding(true).GetBytes(now + " " + logMessage);
            // Add some information to the file.
            fs.Write(info, 0, info.Length);
        }
        private void OpenLogFile()
        {
            // Log file created under %appdata% which is C:\Users\<CurrentUser>\AppData\Roaming
            var AT3folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AT3");
            var Chatterpillarfolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AT3\\Chatterpillar");
            var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AT3\\Chatterpillar\\logger.txt");
            
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
                // Create the file, or overwrite if the file exists.
                using (fs = File.Create(fileName))
                {
                    WriteLogMessage("Logger Started");
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void CloseLogFile()
        {
            fs.Close();
        }
    }
}
