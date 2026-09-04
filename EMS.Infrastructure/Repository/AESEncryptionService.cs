using System.Security.Cryptography;
using System.Text;
using EMS.Application.Interfaces;
using EMS.Application.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EMS.Infrastructure.Repository
{
    internal class AESEncryptionService : IEncryptionService
    {
        private readonly byte[] key;
        private readonly ILogger<AESEncryptionService> logger;
        public AESEncryptionService(IOptions<EncryptionSettings> settings, ILogger<AESEncryptionService> logger)
        {

            this.key = Convert.FromBase64String(
                settings.Value.Key);

            this.logger = logger;   
        }

        public string EncryptData(string plainText)
        {
            //logger.LogInformation("Encrypting data using AES encryption: {PlainText}.", plainText);
            using var aes = Aes.Create();

            aes.Key = key;

            aes.GenerateIV();

            using var encryptor =
                aes.CreateEncryptor();

            var plainBytes =
                Encoding.UTF8.GetBytes(plainText);

            var encryptedBytes =
                encryptor.TransformFinalBlock(
                    plainBytes,
                    0,
                    plainBytes.Length);

            var result = aes.IV
                .Concat(encryptedBytes)
                .ToArray();

            return Convert.ToBase64String(result);
        }

        public string DecryptData(string cipherText)
        {
            var fullCipher =
     Convert.FromBase64String(cipherText);

            using var aes = Aes.Create();

            aes.Key = key;

            var iv = new byte[16];

            Array.Copy(
                fullCipher,
                0,
                iv,
                0,
                iv.Length);

            aes.IV = iv;

            var cipherBytes =
                fullCipher
                .Skip(16)
                .ToArray();

            using var decryptor =
                aes.CreateDecryptor();

            var decryptedBytes =
                decryptor.TransformFinalBlock(
                    cipherBytes,
                    0,
                    cipherBytes.Length);

            return Encoding.UTF8.GetString(
                decryptedBytes);
        }
    }
}