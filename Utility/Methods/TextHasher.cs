using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Methods
{
    public enum HashType { MD5, SHA512, SHA256, SHA384, SHA1 }
    public enum HashSubType {Normal, Cng, Managed, CryptoServiceProvider}

    public static class TextHasher
    {
        public static string Hash(this string input, HashType hash, HashSubType subType = HashSubType.Normal)
        {
            Func<HashAlgorithm, string> hashFunction = alg => HashingHelper(input, alg);

            switch (subType)
            {
                case HashSubType.Normal:
                    return hashFunction(NormalHashes(hash));
                case HashSubType.Cng:
                    return hashFunction(CngHashes(hash));
                case HashSubType.Managed:
                    return hashFunction(ManagedHashes(hash));
                case HashSubType.CryptoServiceProvider:
                    return hashFunction(CSPHashes(hash));
                default: return "error"; // unreachable
            }
        }

        private static string HashingHelper(string text, HashAlgorithm algorithm)
        {
            Func<string, byte[]> getHash = input => algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            var sb = new StringBuilder();
            Array.ForEach(getHash(text), b => sb.Append(b.ToString("X")));

            return sb.ToString();
        }

        private static HashAlgorithm NormalHashes(HashType hash)
        {
            switch (hash)
            {
                case HashType.MD5:
                    return MD5.Create();
                case HashType.SHA1:
                    return SHA1.Create();
                case HashType.SHA256:
                    return SHA256.Create();
                case HashType.SHA384:
                    return SHA384.Create();
                case HashType.SHA512:
                    return SHA512.Create();
                default: return null; // unreachable
            }
        }

        private static HashAlgorithm CngHashes(HashType hash)
        {
            switch (hash)
            {
                case HashType.MD5:
                    return MD5Cng.Create();
                case HashType.SHA1:
                    return SHA1Cng.Create();
                case HashType.SHA256:
                    return SHA256Cng.Create();
                case HashType.SHA384:
                    return SHA384Cng.Create();
                case HashType.SHA512:
                    return SHA512Cng.Create();
                default: return null; // unreachable
            }
        }

        private static HashAlgorithm ManagedHashes(HashType hash)
        {
            switch (hash)
            {
                case HashType.SHA1:
                    return SHA1Managed.Create();
                case HashType.SHA256:
                    return SHA256Managed.Create();
                case HashType.SHA384:
                    return SHA384Managed.Create();
                case HashType.SHA512:
                    return SHA512Managed.Create();
                default: return null; // unreachable
            }
        }

        private static HashAlgorithm CSPHashes(HashType hash)
        {
            switch (hash)
            {
                case HashType.MD5:
                    return MD5CryptoServiceProvider.Create();
                case HashType.SHA1:
                    return SHA1CryptoServiceProvider.Create();
                case HashType.SHA256:
                    return SHA256CryptoServiceProvider.Create();
                case HashType.SHA384:
                    return SHA384CryptoServiceProvider.Create();
                case HashType.SHA512:
                    return SHA512CryptoServiceProvider.Create();
                default: return null; // unreachable
            }
        }
    }
}
