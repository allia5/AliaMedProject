using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using FileMagic;
using Microsoft.AspNetCore.StaticFiles;

namespace Server.Utility
{
    public static class Utility
    {
        private const string encryptionKey = "AAECAwQFBgcICQoLDA0ODw==";



        private static readonly byte[] Salt = Encoding.ASCII.GetBytes("ASJCXLlJDTqJDHDRvcSEAHbc==");






        public static string GetFileType(byte[] byteArray)
        {
            // Define the file signatures for each supported file type
            Dictionary<string, string> fileSignatures = new Dictionary<string, string>
    {
         { ".jpg", "FFD8FF" },
    { ".pdf", "25504446" },
    { ".docx", "504B0304" },
    { ".pptx", "504B0304" },
    { ".png", "89504E47" },
    { ".txt", "None" },
    { ".html", "3C68746D6C" }
    };

            // Get the file signature from the byte array
            string fileSignature = BitConverter.ToString(byteArray.Take(4).ToArray()).Replace("-", "");

            // Determine the file type based on the file signature
            foreach (KeyValuePair<string, string> entry in fileSignatures)
            {
                if (fileSignature.StartsWith(entry.Value))
                {
                    return entry.Key;
                }
            }

            // If no matching file type was found, return "unknown"
            return "unknown";
        }

        public static string EncryptString(string plainText, string key)
        {
            byte[] keyBytes = new Rfc2898DeriveBytes(key, Salt).GetBytes(256 / 8);

            using Aes aes = Aes.Create();
            aes.Key = keyBytes;
            aes.GenerateIV();

            byte[] iv = aes.IV;

            using MemoryStream memoryStream = new MemoryStream();
            using CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
            using StreamWriter streamWriter = new StreamWriter(cryptoStream);
            streamWriter.Write(plainText);
            streamWriter.Close();
            cryptoStream.Close();
            byte[] cipherTextBytes = memoryStream.ToArray();

            byte[] combinedBytes = new byte[iv.Length + cipherTextBytes.Length];
            Array.Copy(iv, 0, combinedBytes, 0, iv.Length);
            Array.Copy(cipherTextBytes, 0, combinedBytes, iv.Length, cipherTextBytes.Length);

            string cipherText = Convert.ToBase64String(combinedBytes);
            return cipherText;
        }

        public static string DecryptString(string cipherText, string key)
        {
           
                byte[] combinedBytes = Convert.FromBase64String(cipherText);

                byte[] iv = new byte[16];
                byte[] cipherTextBytes = new byte[combinedBytes.Length - 16];

                Array.Copy(combinedBytes, 0, iv, 0, 16);
                Array.Copy(combinedBytes, 16, cipherTextBytes, 0, cipherTextBytes.Length);

                byte[] keyBytes = new Rfc2898DeriveBytes(key, Salt).GetBytes(256 / 8);

                using Aes aes = Aes.Create();
                aes.Key = keyBytes;
                aes.IV = iv;

                using MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
                using CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
                using StreamReader streamReader = new StreamReader(cryptoStream);
                string plainText = streamReader.ReadToEnd();
                return plainText;
          
           
        }






        public static string GenerateQRCodeStringFromGuid(Guid guid)
        {
            var bytes = guid.ToByteArray();
            var code = BitConverter.ToUInt32(bytes, 0) + BitConverter.ToUInt32(bytes, 4) +
                       BitConverter.ToUInt32(bytes, 8) + BitConverter.ToUInt32(bytes, 12);
            return "A" + code.ToString("00000") + "B";
        }



        public static string GenerateID(string firstname, string lastname, DateTime dateOfBirth)
        {
            // Concatenate the inputs into a string
            string inputStr = $"{firstname} {lastname} {dateOfBirth.ToShortDateString()}";

            // Hash the input string using SHA-256
            SHA256 sha256 = SHA256.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputStr);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);
            string hashStr = BitConverter.ToString(hashBytes).Replace("-", "");

            // Truncate the hash to 16 characters to generate a unique ID
            string id = hashStr.Substring(0, 16);

            return id;
        }
    


    public static TransactionScope CreateAsyncTransactionScope(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            var transactionOptions = new TransactionOptions
            {
                /* IsolationLevel = isolationLevel,*/
                Timeout = TransactionManager.MaximumTimeout,

            };
            return new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);
        }
        public static string EncryptGuid(Guid guid)
        {
            // Convertir le GUID en bytes
            byte[] guidBytes = guid.ToByteArray();

            // Initialiser l'algorithme de chiffrement AES avec la clé d'encryption
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = System.Text.Encoding.UTF8.GetBytes(encryptionKey);
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];

                // Créer un chiffreur pour encrypter les données
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Encrypter les données du GUID
                byte[] encryptedBytes = encryptor.TransformFinalBlock(guidBytes, 0, guidBytes.Length);

                // Convertir les données encryptées en base64 pour stockage ou transmission
                return Convert.ToBase64String(encryptedBytes);
            }
        }

     
        public static Guid DecryptGuid(string encryptedGuid)
        {
            // Convertir les données encryptées en bytes
            byte[] encryptedBytes = Convert.FromBase64String(encryptedGuid);

            // Initialiser l'algorithme de chiffrement AES avec la clé d'encryption
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = System.Text.Encoding.UTF8.GetBytes(encryptionKey);
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];

                // Créer un déchiffreur pour décrypter les données
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Décrypter les données du GUID
                byte[] guidBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                // Convertir les bytes du GUID en un GUID
                return new Guid(guidBytes);
            }
        }


        ///////////////////////
        private static readonly byte[] Key = {0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF,
    0xFE, 0xDC, 0xBA, 0x98, 0x76, 0x54, 0x32, 0x10,
    0x5A, 0x5B, 0x5C, 0x5D, 0x5E, 0x5F, 0x60, 0x61,
    0x62, 0x63, 0x64, 0x65, 0x66, 0x67, 0x68, 0x69 };
        private static readonly byte[] IV = {  0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
    0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0x10 };

        public static byte[] EncryptFile(byte[] fileBytes)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(fileBytes, 0, fileBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        return memoryStream.ToArray();
                    }
                }
            }
        }

        public static byte[] DecryptFile(byte[] encryptedFileBytes)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aesAlg.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(encryptedFileBytes, 0, encryptedFileBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        return memoryStream.ToArray();
                    }
                }
            }
        }
    


}
}
