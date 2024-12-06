using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AppServiceCore.Util
{
    public class StringCipher
    {
        private const int Keysize = 256; // Key size in bits
        private const int DerivationIterations = 1000; // Iterations for PBKDF2

        public static string Encrypt(string plainText, string passPhrase = "AB1C76BE-FED8-407A-A287-0FB5B1F33C02")
        {
            // Generate salt and IV
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            //var ivStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate128BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            using (var keyDerivationFunction = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations, HashAlgorithmName.SHA256))
            {
                var keyBytes = keyDerivationFunction.GetBytes(Keysize / 8);

                using (var aes = Aes.Create())
                {
                    aes.KeySize = Keysize;
                    aes.BlockSize = 128;
                    aes.Key = keyBytes;
                    aes.IV = ivStringBytes;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    using (var encryptor = aes.CreateEncryptor())
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();

                                // Combine salt, IV, and cipher text into a single array
                                var cipherTextBytes = saltStringBytes
                                    .Concat(ivStringBytes)
                                    .Concat(memoryStream.ToArray())
                                    .ToArray();

                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(string cipherText, string passPhrase = "AB1C76BE-FED8-407A-A287-0FB5B1F33C02")
        {
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);

            // Extract salt, IV, and actual cipher text
            //var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            //var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(16).ToArray();
            //var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).ToArray();

            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray(); // Extract salt (32 bytes)
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(16).ToArray(); // Extract IV (16 bytes)
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) + 16).ToArray(); // Remaining is cipher text

            using (var keyDerivationFunction = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations, HashAlgorithmName.SHA256))
            {
                var keyBytes = keyDerivationFunction.GetBytes(Keysize / 8);

                using (var aes = Aes.Create())
                {
                    aes.KeySize = Keysize;
                    aes.BlockSize = 128;
                    aes.Key = keyBytes;
                    aes.IV = ivStringBytes;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    using (var decryptor = aes.CreateDecryptor())
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 bytes = 256 bits
            RandomNumberGenerator.Fill(randomBytes); // Use RandomNumberGenerator for cryptographic randomness
            return randomBytes;
        }

        private static byte[] Generate128BitsOfRandomEntropy()
        {
            var randomBytes = new byte[16]; // 16 bytes = 128 bits
            RandomNumberGenerator.Fill(randomBytes); // Use RandomNumberGenerator for cryptographic randomness
            return randomBytes;
        }
    }
}