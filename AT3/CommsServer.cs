using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Threading;
using System.Diagnostics;

/// <acknowledgments>
/// How to write an SSL streams application
/// https://docs.microsoft.com/en-us/dotnet/api/system.net.security.sslstream?view=net-5.0
/// </acknowledgments>

namespace AT3
{
    public class CommsServer
    {
        // Certificate file that will be used
        public const string serverCertificateFile = "test.z-710.com.cer";
        private int serverPortNum;
        // Logger
        Logger myLogger = null;
        // Use GetInstance function to get access to the single object  
        public CommsServer(string port)
        {
            // Initialise the Logger
            myLogger = Logger.GetInstance();
            serverPortNum = Convert.ToInt32(port);
        }
        ~CommsServer()
        {
        }
        private static X509Certificate serverCertificate = null;
        // The certificate parameter specifies the name of the file
        // containing the machine certificate.
        public void RunServer()
        {
            serverCertificate = X509Certificate.CreateFromCertFile(serverCertificateFile);
            // Create a TCP/IP (IPv4) socket and listen for incoming connections
            TcpListener listener = new TcpListener(IPAddress.Any, serverPortNum);
            listener.Start();
            while (true)
            {
                myLogger.WriteLogMessage("Waiting for a client to connect...");
                // Application blocks while waiting for an incoming connection.
                // Type CNTL-C to terminate the server.
                TcpClient client = listener.AcceptTcpClient();
                ProcessClient(client);
            } 
        }
        private void ProcessClient(TcpClient client)
        {
            // A client has connected. Create the
            // SslStream using the client's network stream.
            SslStream sslStream = new SslStream(
                client.GetStream(), false);
            
            // Authenticate the server but don't require the client to authenticate.
            try
            {
                sslStream.AuthenticateAsServer(serverCertificate, clientCertificateRequired: false, checkCertificateRevocation: true);

                // Display the properties and settings for the authenticated stream.
                DisplaySecurityLevel(sslStream);
                DisplaySecurityServices(sslStream);
                DisplayCertificateInformation(sslStream);
                DisplayStreamProperties(sslStream);

                // Set timeouts for the read and write to 5 seconds.
                sslStream.ReadTimeout = 5000;
                sslStream.WriteTimeout = 5000;
                // Read a message from the client.
                myLogger.WriteLogMessage("Waiting for client message...");
                string messageData = ReadMessage(sslStream);
                myLogger.WriteLogMessage("Received: " + messageData);

                // Write a message to the client.
                byte[] message = Encoding.UTF8.GetBytes("Hello from the server.<EOF>");
                myLogger.WriteLogMessage("Sending hello message.");
                sslStream.Write(message);
            }
            catch (AuthenticationException e)
            {
                myLogger.WriteLogMessage("Exception: " + e.Message);
                if (e.InnerException != null)
                {
                    myLogger.WriteLogMessage("Inner exception: " + e.InnerException.Message);
                }
                myLogger.WriteLogMessage("Authentication failed - closing the connection.");
                sslStream.Close();
                client.Close();
                return;
            }
            finally
            {
                // The client stream will be closed with the sslStream
                // because we specified this behavior when creating
                // the sslStream.
                sslStream.Close();
                client.Close();
            }
        }
        private string ReadMessage(SslStream sslStream)
        {
            // Read the  message sent by the client.
            // The client signals the end of the message using the
            // "<EOF>" marker.
            byte[] buffer = new byte[2048];
            StringBuilder messageData = new StringBuilder();
            int bytes = -1;
            do
            {
                // Read the client's test message.
                bytes = sslStream.Read(buffer, 0, buffer.Length);

                // Use Decoder class to convert from bytes to UTF8
                // in case a character spans two buffers.
                Decoder decoder = Encoding.UTF8.GetDecoder();
                char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
                decoder.GetChars(buffer, 0, bytes, chars, 0);
                messageData.Append(chars);
                // Check for EOF or an empty message.
                if (messageData.ToString().IndexOf("<EOF>") != -1)
                {
                    break;
                }
            } while (bytes != 0);

            return messageData.ToString();
        }
        private void DisplaySecurityLevel(SslStream stream)
        {
            myLogger.WriteLogMessage("Cipher:"+ stream.CipherAlgorithm + "strength " + stream.CipherStrength);
            myLogger.WriteLogMessage("Hash: "+ stream.HashAlgorithm + " strength " + stream.HashStrength);
            myLogger.WriteLogMessage("Key exchange: "+ stream.KeyExchangeAlgorithm + " strength " + stream.KeyExchangeStrength);
            myLogger.WriteLogMessage("Protocol: "+ stream.SslProtocol);
        }
        private void DisplaySecurityServices(SslStream stream)
        {
            myLogger.WriteLogMessage("Is authenticated: "+ stream.IsAuthenticated + " as server? " + stream.IsServer);
            myLogger.WriteLogMessage("IsSigned: "+ stream.IsSigned);
            myLogger.WriteLogMessage("Is Encrypted: "+ stream.IsEncrypted);
        }
        private void DisplayStreamProperties(SslStream stream)
        {
            myLogger.WriteLogMessage("Can read: "+ stream.CanRead  + " write " + stream.CanWrite);
            myLogger.WriteLogMessage("Can timeout: "+ stream.CanTimeout);
        }
        private void DisplayCertificateInformation(SslStream stream)
        {
            myLogger.WriteLogMessage("Certificate revocation list checked: "+ stream.CheckCertRevocationStatus);

            X509Certificate localCertificate = stream.LocalCertificate;
            if (stream.LocalCertificate != null)
            {
                myLogger.WriteLogMessage("Local cert was issued to "+ localCertificate.Subject +
                    " and is valid from " + localCertificate.GetEffectiveDateString() 
                    + " until " + localCertificate.GetExpirationDateString());
            }
            else
            {
                myLogger.WriteLogMessage("Local certificate is null.");
            }
            // Display the properties of the client's certificate.
            X509Certificate remoteCertificate = stream.RemoteCertificate;
            if (stream.RemoteCertificate != null)
            {
                myLogger.WriteLogMessage("Remote cert was issued to " + remoteCertificate.Subject +
                    " and is valid from " + remoteCertificate.GetEffectiveDateString() 
                    + " until " + remoteCertificate.GetExpirationDateString());
            }
            else
            {
                myLogger.WriteLogMessage("Remote certificate is null.");
            }
        }
 
    }
}
