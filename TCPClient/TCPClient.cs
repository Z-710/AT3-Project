using System;
using System.Collections;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.IO;

/// <acknowledgments>
/// Understanding TLS 1.2,1.3
/// https://www.youtube.com/watch?v=AlE5X1NlHgg&t=512s
/// How to write an SSL streams application
/// https://docs.microsoft.com/en-us/dotnet/api/system.net.security.sslstream?view=net-5.0
/// How to get input from the console window
/// https://www.programiz.com/csharp-programming/basic-input-output
/// </acknowledgments>

namespace TCPClient
{
    public class SslTcpClient
    {
        static X509Certificate clientCertificate = null;
        private static Hashtable certificateErrors = new Hashtable();
        static SslStream serverSslStream = null;
        // The following method is invoked by the RemoteCertificateValidationDelegate.
        public static bool ValidateServerCertificate(
              object sender,
              X509Certificate certificate,
              X509Chain chain,
              SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

            // Do not allow this client to communicate with unauthenticated servers.
            return false;
        }
        public static X509Certificate SelectLocalCertificate(
        object sender,
        string targetHost,
        X509CertificateCollection localCertificates,
        X509Certificate remoteCertificate,
        string[] acceptableIssuers)
        {
            Console.WriteLine("Client will use the certificate passed in");
            return clientCertificate;
      
        }
        public static void RunClient(string machineName, string serverCertificateName, string clientCertificateFile)
        {
            // Set local certificate from the certificate passed in 
            clientCertificate = X509Certificate.CreateFromCertFile(clientCertificateFile);
            // Create a TCP/IP client socket.
            // machineName is the host running the server application.
            TcpClient client = new TcpClient(machineName, 8080);
            Console.WriteLine("Client connected.");
            // Create an SSL stream that will close the client's stream.
            SslStream sslStream = new SslStream(
                client.GetStream(),
                false,
                new RemoteCertificateValidationCallback(ValidateServerCertificate),
                new LocalCertificateSelectionCallback(SelectLocalCertificate)
                );
            serverSslStream = sslStream;
            // The server name must match the name on the server certificate.
            try
            {
                sslStream.AuthenticateAsClient(serverCertificateName);
            }
            catch (AuthenticationException e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
                if (e.InnerException != null)
                {
                    Console.WriteLine("Inner exception: {0}", e.InnerException.Message);
                }
                Console.WriteLine("Authentication failed - closing the connection.");
                client.Close();
                return;
            }
            // Allow user to enter and send messages until "end" is entered
            string msgString = "";
            while (msgString != "end<EOF>")
            {
                // Ask for the string to send
                Console.Write("Enter a string - ");
                msgString = Console.ReadLine()+"<EOF>";
                // Encode the message into a byte array.
                byte[] message = Encoding.UTF8.GetBytes(msgString);
                // Send message to the server.
                sslStream.Write(message);
                sslStream.Flush();

            }
            // Close the client connection.
            client.Close();
            Console.WriteLine("Client closed.");
        }
        public static void MessageReceiver()
        {
            while (true)
            {
                if (serverSslStream != null)
                {
                    // Read message from the server.
                    string serverMessage = ReadMessage(serverSslStream);
                    Console.WriteLine("Server says: {0}", serverMessage);
                }
                Thread.Sleep(1000);
            }
        }
        static string ReadMessage(SslStream sslStream)
        {
            // Read the  message sent by the server.
            // The end of the message is signaled using the
            // "<EOF>" marker.
            byte[] buffer = new byte[2048];
            StringBuilder messageData = new StringBuilder();
            int bytes = -1;
            do
            {
                bytes = sslStream.Read(buffer, 0, buffer.Length);

                // Use Decoder class to convert from bytes to UTF8
                // in case a character spans two buffers.
                Decoder decoder = Encoding.UTF8.GetDecoder();
                char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
                decoder.GetChars(buffer, 0, bytes, chars, 0);
                messageData.Append(chars);
                // Check for EOF.
                if (messageData.ToString().IndexOf("<EOF>") != -1)
                {
                    break;
                }
            } while (bytes != 0);

            return messageData.ToString();
        }
        private static void DisplayUsage()
        {
            Console.WriteLine("To start the client specify:");
            Console.WriteLine("TCPClient machineName serverCertificateName clientCertificateFile");
            Environment.Exit(1);
        }
        public static int Main(string[] args)
        {
            string serverCertificateName = null;
            string machineName = null;
            string clientCertificateFile=null;
            if (args == null || args.Length < 3)
            {
                DisplayUsage();
            }
            // User can specify the machine name, server certificate name and client certificate file.
            // Server name must match the name on the server's certificate.
            machineName = args[0];
            serverCertificateName = args[1];
            clientCertificateFile = args[2];
            Thread InstanceCaller = new Thread(new ThreadStart(MessageReceiver));
            // Start the thread.
            InstanceCaller.Start();
            SslTcpClient.RunClient(machineName, serverCertificateName, clientCertificateFile);
            return 0;            
        }

            

    }
}
