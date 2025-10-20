using System;
using System.Text;

namespace Tool_EnDecrypt
{
    public static class CryptoUtils
    {
        // Simple hash function thay thế SHA256/SHA1
        public static byte[] SimpleHash(string input, int outputLength)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hash = new byte[outputLength];
            
            // Simple hash algorithm
            for (int i = 0; i < outputLength; i++)
            {
                hash[i] = 0;
                for (int j = 0; j < inputBytes.Length; j++)
                {
                    hash[i] ^= (byte)(inputBytes[j] + i + j);
                    hash[i] = (byte)((hash[i] << 3) | (hash[i] >> 5)); // Rotate left 3 bits
                }
            }
            
            return hash;
        }
        
        // Simple random number generator thay thế RandomNumberGenerator
        private static uint seed = (uint)DateTime.Now.Ticks;
        
        public static byte[] GenerateRandomBytes(int length)
        {
            byte[] result = new byte[length];
            for (int i = 0; i < length; i++)
            {
                // Linear Congruential Generator (LCG)
                seed = (seed * 1103515245 + 12345) & 0x7fffffff;
                result[i] = (byte)(seed & 0xFF);
            }
            return result;
        }
        
        // Simple key derivation function
        public static byte[] DeriveKey(string password, int keyLength)
        {
            return SimpleHash(password, keyLength);
        }
    }
}
