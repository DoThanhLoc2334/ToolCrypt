using System;
using System.Numerics;
using System.Security.Cryptography;

namespace Tool_EnDecrypt
{
    // Minh hoạ học thuật: KHÔNG dùng padding an toàn.
    internal class SimpleRSA
    {
        public BigInteger N { get; private set; }     // modulus
        public BigInteger E { get; private set; }     // public exponent
        public BigInteger D { get; private set; }     // private exponent
        public int KeySizeBits { get; private set; }

        public int ModulusBytes => (KeySizeBits + 7) / 8;

        public SimpleRSA(BigInteger n, BigInteger e, BigInteger d, int bits)
        {
            N = n; E = e; D = d; KeySizeBits = bits;
        }

        public static SimpleRSA Generate(int bits = 1024, BigInteger? eOpt = null)
        {
            if (bits < 512) throw new ArgumentException("Key too small for demo.");
            var e = eOpt ?? new BigInteger(65537);

            BigInteger p, q, phi;
            do
            {
                p = RandomPrime(bits / 2);
                q = RandomPrime(bits / 2);
                if (p == q) continue;

                var n = p * q;
                phi = (p - 1) * (q - 1);
                if (BigInteger.GreatestCommonDivisor(e, phi) != 1) continue;

                var d = ModInverse(e, phi);
                return new SimpleRSA(n, e, d, bits);
            } while (true);
        }

        // ============ Encrypt/Decrypt (raw, no padding) ============
        public byte[] EncryptBlock(byte[] plainBlock)
        {
            // block phải < N (về trị số). Ta zero-pad lên đúng ModulusBytes
            if (plainBlock.Length >= ModulusBytes) throw new ArgumentException("Block too large.");
            var m = CryptoUtils.BigIntFromBigEndian(plainBlock);
            if (m >= N) throw new ArgumentException("Block value >= modulus.");

            var c = BigInteger.ModPow(m, E, N);
            return CryptoUtils.ToBigEndian(c, ModulusBytes);
        }

        public byte[] DecryptBlock(byte[] cipherBlock)
        {
            if (cipherBlock.Length != ModulusBytes) throw new ArgumentException("Cipher block size mismatch.");
            var c = CryptoUtils.BigIntFromBigEndian(cipherBlock);
            var m = BigInteger.ModPow(c, D, N);
            // Trả về đúng ModulusBytes rồi caller tự cắt bỏ 0 ở đầu (nếu cần)
            return CryptoUtils.ToBigEndian(m, ModulusBytes);
        }

        // ============ Helpers ============
        private static BigInteger RandomPrime(int bits)
        {
            using var rng = RandomNumberGenerator.Create();
            while (true)
            {
                var cand = RandomOddBigInteger(bits, rng);
                if (IsProbablePrime(cand, 16)) return cand;
            }
        }

        private static BigInteger RandomOddBigInteger(int bits, RandomNumberGenerator rng)
        {
            var bytes = new byte[(bits + 7) / 8];
            rng.GetBytes(bytes);
            // Đặt bit cao để đủ độ dài
            int msbIndex = bytes.Length - 1;
            bytes[msbIndex] |= 0x80;
            // Đảm bảo số lẻ
            bytes[0] |= 0x01;
            // BigInteger little-endian: đảo
            Array.Reverse(bytes);
            return new BigInteger(bytes, isUnsigned: true, isBigEndian: false);
        }

        // Miller–Rabin
        private static bool IsProbablePrime(BigInteger n, int rounds)
        {
            if (n < 2) return false;
            if (n % 2 == 0) return n == 2;

            // n-1 = d*2^s
            BigInteger d = n - 1;
            int s = 0;
            while ((d & 1) == 0) { d >>= 1; s++; }

            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[n.ToByteArray().Length];

            for (int i = 0; i < rounds; i++)
            {
                BigInteger a;
                do
                {
                    rng.GetBytes(bytes);
                    a = new BigInteger(bytes, isUnsigned: true, isBigEndian: false);
                } while (a < 2 || a >= n - 2);

                var x = BigInteger.ModPow(a, d, n);
                if (x == 1 || x == n - 1) continue;

                bool cont = false;
                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, n);
                    if (x == n - 1) { cont = true; break; }
                }
                if (!cont) return false;
            }
            return true;
        }

        // Extended Euclid
        private static BigInteger ModInverse(BigInteger a, BigInteger m)
        {
            BigInteger m0 = m, x0 = 0, x1 = 1;
            if (m == 1) return 0;
            while (a > 1)
            {
                var q = a / m;
                (a, m) = (m, a % m);
                (x0, x1) = (x1 - q * x0, x0);
            }
            if (x1 < 0) x1 += m0;
            return x1;
        }
    }
}
