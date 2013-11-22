using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Methods
{
    public enum HashType { MD5, SHA512, SHA256, SHA384, SHA1 }
    public enum HashSubType { Normal, Cng, Managed, CryptoServiceProvider }

    /// <summary>
    /// Serves to remove boilerplate, by generating a checksum directly on the string.
    /// </summary>
    public static class TextHasher
    {
        private const string BASE = "System.Security.Cryptography.";

        public const string
            BASE_MD5     = BASE + "MD5",
            BASE_SHA1    = BASE + "SHA1",
            BASE_SHA256  = BASE + "SHA256",
            BASE_SHA384  = BASE + "SHA384",
            BASE_SHA512  = BASE + "SHA512",
            TYPE_CNG     = "Cng",
            TYPE_MANAGED = "Managed",
            TYPE_CSP     = "CryptoServiceProvider";

        /// <summary>
        /// Generates a hash of the specified hash type
        /// </summary>
        /// <param name="input">Any string</param>
        /// <param name="hash">The enum "HashType"</param>
        /// <returns>A string of the generated hash-code in hex-code</returns>
        public static string GenerateHash(this string input, HashType hash)
        {
            return input.GenerateHash(hash, HashSubType.Normal);
        }

        /// <summary>
        /// Generates a hash of the specified hash type
        /// </summary>
        /// <param name="input">Any string</param>
        /// <param name="hash">The enum "HashType"</param>
        /// <param name="subType">The sub-type of the hashing algorithm i.e. Cng, Managed, etc.</param>
        /// <returns>A string of the generated hash-code in hex-code</returns>
        public static string GenerateHash(this string input, HashType hash, HashSubType subType)
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

        /// <summary>
        /// Takes care of actually computing the hashing by using the specified algorithm along with the text
        /// to create a string based on the byte-array created by the ComputeHash method
        /// </summary>
        /// <param name="text">The string you want to hash</param>
        /// <param name="algorithm">The hashing algorithm that's used to generate the checksum</param>
        /// <returns>The chekcsum based on the input</returns>
        private static string HashingHelper(string text, HashAlgorithm algorithm)
        {
            Func<string, byte[]> getHash = input => algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            var sb = new StringBuilder();
            Array.ForEach(getHash(text), b => sb.Append(b.ToString("X")));

            return sb.ToString();
        }

        /// <summary>
        /// Generates the default types of the specified hashing algorithms
        /// </summary>
        /// <param name="hash">Enumerator to switch on</param>
        /// <returns>The hashing algorithm based on the enum</returns>
        private static HashAlgorithm NormalHashes(HashType hash)
        {
            switch (hash)
            {
                case HashType.MD5:
                    return MD5.Create(BASE_MD5);
                case HashType.SHA1:
                    return SHA1.Create(BASE_SHA1);
                case HashType.SHA256:
                    return SHA256.Create(BASE_SHA256);
                case HashType.SHA384:
                    return SHA384.Create(BASE_SHA384);
                case HashType.SHA512:
                    return SHA512.Create(BASE_SHA512);
                default: return null; // unreachable
            }
        }

        /// <summary>
        /// Generates a hash using Cng
        /// </summary>
        /// <param name="hash">Enumerator to switch on</param>
        /// <returns>The hashing algorithm based on the enum</returns>
        private static HashAlgorithm CngHashes(HashType hash)
        {
            switch (hash)
            {
                case HashType.MD5:
                    return MD5.Create(BASE_MD5 + TYPE_CNG);
                case HashType.SHA1:
                    return SHA1.Create(BASE_SHA1 + TYPE_CNG);
                case HashType.SHA256:
                    return SHA256.Create(BASE_SHA256 + TYPE_CNG);
                case HashType.SHA384:
                    return SHA384.Create(BASE_SHA384 + TYPE_CNG);
                case HashType.SHA512:
                    return SHA512.Create(BASE_SHA512 + TYPE_CNG);
                default: return null; // unreachable
            }
        }

        /// <summary>
        /// Generates a managed hash
        /// Note: MD5 isn't available
        /// </summary>
        /// <param name="hash">Enumerator to switch on</param>
        /// <returns>The hashing algorithm based on the enum</returns>
        private static HashAlgorithm ManagedHashes(HashType hash)
        {
            switch (hash)
            {
                // MD5 not available
                case HashType.SHA1:
                    return SHA1.Create(BASE_SHA1 + TYPE_MANAGED);
                case HashType.SHA256:
                    return SHA256.Create(BASE_SHA256 + TYPE_MANAGED);
                case HashType.SHA384:
                    return SHA384.Create(BASE_SHA384 + TYPE_MANAGED);
                case HashType.SHA512:
                    return SHA512.Create(BASE_SHA512 + TYPE_MANAGED);
                default: return null; // unreachable
            }
        }

        /// <summary>
        /// Generates a hash using the CryptoServiceProvider
        /// </summary>
        /// <param name="hash">Enumerator to switch on</param>
        /// <returns>The hashing algorithm based on the enum</returns>
        private static HashAlgorithm CSPHashes(HashType hash)
        {
            switch (hash)
            {
                case HashType.MD5:
                    return MD5.Create(BASE_MD5 + TYPE_CSP);
                case HashType.SHA1:
                    return SHA1.Create(BASE_SHA1 + TYPE_CSP);
                case HashType.SHA256:
                    return SHA256.Create(BASE_SHA256 + TYPE_CSP);
                case HashType.SHA384:
                    return SHA384.Create(BASE_SHA384 + TYPE_CSP);
                case HashType.SHA512:
                    return SHA512.Create(BASE_SHA512 + TYPE_CSP);
                default: return null; // unreachable
            }
        }
    }
}
