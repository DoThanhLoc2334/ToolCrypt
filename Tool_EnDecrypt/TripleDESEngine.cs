using System;
using System.IO;
using System.Security.Cryptography;

namespace Tool_EnDecrypt
{
    internal static class TripleDESEngine
    {
        // Key: 16 hoặc 24 bytes (16 => K1=K3), IV: 8 bytes. Chế độ CBC, PKCS7.
        public static void EncryptFile(string inputPath, string outputPath, byte[] key, byte[] iv)
        {
            Validate(key, iv);
            using var tdes = new TripleDESCryptoServiceProvider
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                Key = ExpandKey(key),
                IV = iv
            };
            using var fsIn = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
            using var fsOut = new FileStream(outputPath, FileMode.Create, FileAccess.Write);
            using var cs = new CryptoStream(fsOut, tdes.CreateEncryptor(), CryptoStreamMode.Write);
            fsIn.CopyTo(cs);
        }

        public static void DecryptFile(string inputPath, string outputPath, byte[] key, byte[] iv)
        {
            Validate(key, iv);
            using var tdes = new TripleDESCryptoServiceProvider
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                Key = ExpandKey(key),
                IV = iv
            };
            using var fsIn = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
            using var fsOut = new FileStream(outputPath, FileMode.Create, FileAccess.Write);
            using var cs = new CryptoStream(fsOut, tdes.CreateDecryptor(), CryptoStreamMode.Write);
            fsIn.CopyTo(cs);
        }

        private static void Validate(byte[] key, byte[] iv)
        {
            if (!(key.Length == 16 || key.Length == 24))
                throw new ArgumentException("3DES key must be 16 or 24 bytes.");
            if (iv.Length != 8)
                throw new ArgumentException("3DES IV must be 8 bytes.");
        }

        // Nếu key 16 bytes => (K1||K2||K1)
        private static byte[] ExpandKey(byte[] key)
        {
            if (key.Length == 24) return key;
            byte[] k = new byte[24];
            Buffer.BlockCopy(key, 0, k, 0, 16);
            // copy K1 vào K3
            Buffer.BlockCopy(key, 0, k, 16, 8);
            return k;
        }
    }
}
