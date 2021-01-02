using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel;

/// <acknowledgments>
/// How to output the date and time
/// https://docs.microsoft.com/en-us/dotnet/api/system.datetime?view=net-5.0
/// How to output process id and thread id
/// https://www.codegrepper.com/code-examples/csharp/c%23+current+thread+id
/// https://stackoverflow.com/questions/3003975/how-to-get-the-current-processid
/// </acknowledgments>
namespace AT3
{
    class Logger
    {
        // Handle for logfile
        private static FileStream fs = null;
        // Singleton object required
        private static Logger _instance = null;

        // Use GetInstance function to get access to the single object  
        public static Logger GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Logger();
            }
                return _instance;
        }
        private Logger()
        {
            // Open the log file
            OpenLogFile();

        }
        ~Logger()
        {
            // Close the log file
            CloseLogFile();
        }
        public void WriteLogMessage(string logMessage)
        {
            // Get the date and time, process id and thread id
            DateTime now = DateTime.Now;
            int nProcessID = Process.GetCurrentProcess().Id;
            int curThread = Thread.CurrentThread.ManagedThreadId;
            byte[] info = new UTF8Encoding(true).GetBytes(now + " pid: "+ nProcessID.ToString() +" tid: " + curThread.ToString() +" "+ logMessage + "\r\n");
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
                fs = File.Create(fileName);
                
                WriteLogMessage("Log Started");
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
