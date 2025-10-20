using System;
using System.IO;
using System.Numerics;
using System.Text;

namespace Tool_EnDecrypt
{
    internal static class CryptoUtils
    {
        public static byte[] GetBytesFixed(string s, int length)
        {
            // Lấy UTF8 rồi pad/cắt đúng length
            var raw = Encoding.UTF8.GetBytes(s ?? "");
            var buf = new byte[length];
            for (int i = 0; i < length && i < raw.Length; i++) buf[i] = raw[i];
            return buf;
        }

        public static byte[] ReadAll(string path) => File.ReadAllBytes(path);
        public static void WriteAll(string path, byte[] data) => File.WriteAllBytes(path, data);

        public static byte[][] Chunk(byte[] data, int chunkSize)
        {
            int total = (data.Length + chunkSize - 1) / chunkSize;
            var chunks = new byte[total][];
            for (int i = 0; i < total; i++)
            {
                int ofs = i * chunkSize;
                int len = Math.Min(chunkSize, data.Length - ofs);
                chunks[i] = new byte[len];
                Buffer.BlockCopy(data, ofs, chunks[i], 0, len);
            }
            return chunks;
        }

        public static byte[] Concat(params byte[][] arrays)
        {
            int sum = 0; foreach (var a in arrays) sum += a.Length;
            byte[] r = new byte[sum];
            int ofs = 0;
            foreach (var a in arrays) { Buffer.BlockCopy(a, 0, r, ofs, a.Length); ofs += a.Length; }
            return r;
        }

        public static BigInteger BigIntFromBigEndian(byte[] be)
        {
            // BigInteger trong .NET là little-endian có dấu; thêm 0x00 để dương
            var le = new byte[be.Length + 1];
            for (int i = 0; i < be.Length; i++) le[i] = be[be.Length - 1 - i];
            le[le.Length - 1] = 0; // dấu dương
            return new BigInteger(le);
        }

        public static byte[] ToBigEndian(BigInteger x, int size)
        {
            var le = x.ToByteArray(); // little-endian, có dấu
            // chuyển sang big-endian không dấu, fix size
            // bỏ byte dấu nếu thừa
            int drop = (le.Length > 1 && le[^1] == 0) ? 1 : 0;
            int useful = le.Length - drop;
            byte[] be = new byte[size];
            for (int i = 0; i < useful && i < size; i++)
                be[size - 1 - i] = le[i];
            return be;
        }
    }
}
