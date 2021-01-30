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
/// </acknowledgments>
/// 
namespace AT3
{
    public class CommsClient
    {
        static X509Certificate clientCertificate = null;
        static SslStream serverSslStream = null;
        private int clientPortNum;
        // Certificate file that will be used
        private const string clientCertificateFile = "test.z-710.com.cer";
        private const string serverCertificateName = "test.z-710.com";
        // Logger
        Logger myLogger = null;
        // Contacts
        Contacts myContacts = null;
        // Messages
        Messages myMessages = null;
        public CommsClient(string port)
        {
            // Initialise the Logger
            myLogger = Logger.GetInstance();
            clientPortNum = Convert.ToInt32(port);
            // Initialise the Contacts
            myContacts = Contacts.GetInstance();
            // Initialise the Messages
            myMessages = Messages.GetInstance();
            // Set the comms state to listening 
            CommsFSM.SetNextState(CommsFSM.Command.Start);
            // Check the current state 
            myLogger.WriteLogMessage("Current state is " + CommsFSM.GetCurrentState().ToString());
        }
        ~CommsClient()
        {
        }
        // The following method is invoked by the RemoteCertificateValidationDelegate.
        public bool ValidateServerCertificate(
              object sender,
              X509Certificate certificate,
              X509Chain chain,
              SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            myLogger.WriteLogMessage("Certificate error:"+ sslPolicyErrors.ToString());

            // Do not allow this client to communicate with unauthenticated servers.
            return false;
        }
        public X509Certificate SelectLocalCertificate(
        object sender,
        string targetHost,
        X509CertificateCollection localCertificates,
        X509Certificate remoteCertificate,
        string[] acceptableIssuers)
        {
            myLogger.WriteLogMessage("Client will use the certificate passed in");
            return clientCertificate;

        }
        public void RunClient()
        {
            myLogger.WriteLogMessage("CommsClient Started");
            // Set local certificate from the certificate passed in 
            clientCertificate = X509Certificate.CreateFromCertFile(clientCertificateFile);
            while (true)
            {
                if (CommsFSM.GetCurrentState() == CommsFSM.ProcessState.UserConnected)
                {
                    // Create a TCP/IP client socket.
                    // Contacts.ContactArray[Contacts.selectedContact].IPAddress is the host running the server application.
                    TcpClient client = new TcpClient(Contacts.ContactArray[Contacts.selectedContact].IPAddress, clientPortNum);
                    myLogger.WriteLogMessage("Client connected.");
                    // Create an SSL stream that will close the client's stream.
                    SslStream sslStream = new SslStream(
                        client.GetStream(),
                        false,
                        new RemoteCertificateValidationCallback(ValidateServerCertificate),
                        new LocalCertificateSelectionCallback(SelectLocalCertificate)
                        );
                    // The server name must match the name on the server certificate.
                    try
                    {
                        sslStream.AuthenticateAsClient(serverCertificateName);
                    }
                    catch (AuthenticationException e)
                    {
                        myLogger.WriteLogMessage("Exception:" + e.Message.ToString());
                        if (e.InnerException != null)
                        {
                            myLogger.WriteLogMessage("Inner exception:" + e.InnerException.Message.ToString());
                        }
                        myLogger.WriteLogMessage("Authentication failed - closing the connection.");
                        client.Close();
                        break;
                    }
                    Messages.Messagestruct msg;
                    msg.type = "";
                    msg.message = "";
                    msg.messageSent = false;
                    msg.time = "";
                    msg.newMessageProcessed = true;
                    serverSslStream = sslStream;

                    while (CommsFSM.GetCurrentState() == CommsFSM.ProcessState.UserConnected)
                    {
                        // Write any new messages to the server.
                        if (serverSslStream != null)
                        {
                            if (myMessages.GetUserMessage(ref msg, Contacts.selectedContact))
                            {
                                myLogger.WriteLogMessage("MessageSender: type " + msg.type
                           + " datetime " + msg.time + " msg "
                           + msg.message + " newest message ");
                                byte[] message = Encoding.UTF8.GetBytes(msg.message + "<EOF>");
                                serverSslStream.Write(message);
                            }

                        }
                        Thread.Sleep(1000);
                    }
                    myLogger.WriteLogMessage("Closing Client Connection.");
                    // Close the client connection.
                    client.Close();
                    serverSslStream = null;
                }
                Thread.Sleep(1000);
            }
        }
        public void MessageReceiver()
        {
            myLogger.WriteLogMessage("Message receiver started");
            string messageData;
            while (true)
            {
                if (serverSslStream != null)
                {
                    // Read message from the server.
                    messageData = ReadMessage(serverSslStream);
                    if (messageData != "end")
                    {
                        // Build up the message structure and add to the array
                        Messages.Messagestruct msg;
                        msg.type = "receive";
                        msg.message = messageData;
                        msg.messageSent = true;
                        msg.newMessageProcessed = false;
                        DateTime now = DateTime.Now;
                        msg.time = now.ToString();
                        myMessages.AddMessage(msg, Contacts.selectedContact);
                        myLogger.WriteLogMessage("addreceivedmessage: type " + msg.type
                            + " datetime " + msg.time + " msg "
                            + msg.message + " newest message " + Messages.perContactInfo[Contacts.selectedContact].newestMessage);
                    }
                    else
                    {
                        myLogger.WriteLogMessage("End received");
                    }
                }
                Thread.Sleep(1000);
            }
        }
        private string ReadMessage(SslStream sslStream)
        {
            // Read the  message sent by the server.
            // The end of the message is signaled using the
            // "<EOF>" marker.
            byte[] buffer = new byte[2048];
            StringBuilder messageData = new StringBuilder();
            StringBuilder endString = new StringBuilder("end");
            int bytes = -1;
            do
            {
                try
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
                }
                catch
                {
                    myLogger.WriteLogMessage("Error in ReadMessage caught");
                    messageData = endString;
                    break;
                }
            } while (bytes != 0);
            // Remove <EOF> from the string
            messageData.Replace("<EOF>", "");
            return messageData.ToString();
        }
    }
}
