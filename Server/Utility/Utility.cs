using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace Server.Utility
{
    public static class Utility
    {
        private const string encryptionKey = "AAECAwQFBgcICQoLDA0ODw==";



        public static string GenerateQRCodeStringFromGuid(Guid guid)
        {
            // Convertit le GUID en une chaîne de texte
            var text = guid.ToString();

            // Crée un objet BarcodeWriter qui générera le code QR
            var writer = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.QR_CODE, // Format de code QR
                Options = new QrCodeEncodingOptions // Options d'encodage
                {
                    Height = 250, // Hauteur de l'image
                    Width = 250, // Largeur de l'image
                    Margin = 1 // Marge autour du code QR
                }
            };

            // Génère les données de pixels de l'image du code QR à partir de la chaîne de texte
            var pixelData = writer.Write(text);

            // Crée une image bitmap à partir des données de pixels du code QR
            using (var bitmap = new Bitmap(pixelData.Width, pixelData.Height))
            {
                var bitmapData = bitmap.LockBits(new Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                try
                {
                    // Copie les données de pixels dans l'image bitmap
                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }

                // Convertit l'image bitmap en une chaîne de texte Base64
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    var imageBytes = ms.ToArray();
                    return Convert.ToBase64String(imageBytes);
                }
            }
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
    }
}
