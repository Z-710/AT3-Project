using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Configuration;
using System.Security;
using System.Security.Cryptography;

/// <Acknowledgments>
/// How to create a static class
/// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/static-classes-and-static-class-members
/// How to encrypt and decrypt using DPAPI
/// https://jonathancrozier.com/blog/leveraging-the-dpapi-to-encrypt-sensitive-configurationsettings
/// </Acknowledgments>
namespace AT3
{
    public static class Encryption
    {
        public static string Encrypt(string clearText, byte[] entropy)
        {
            if (clearText == null) throw new ArgumentNullException(nameof(clearText));

            byte[] clearBytes = Encoding.UTF8.GetBytes(clearText);
            byte[] encryptedBytes = ProtectedData.Protect(clearBytes, entropy, DataProtectionScope.CurrentUser);

            return Convert.ToBase64String(encryptedBytes);
        }
        public static string Decrypt(string encryptedText, byte[] entropy)
        {
            if (encryptedText == null) throw new ArgumentNullException(nameof(encryptedText));

            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] clearBytes = ProtectedData.Unprotect(encryptedBytes, entropy, DataProtectionScope.CurrentUser);

            return Encoding.UTF8.GetString(clearBytes);
        }
    }
}
