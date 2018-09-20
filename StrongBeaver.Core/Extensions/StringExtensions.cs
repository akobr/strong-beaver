using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StrongBeaver.Core.Extensions
{
    public static class StringExtensions
    {
        public static Stream ToStream(this string text)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(text);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static Task<Stream> ToStreamAsync(this string text)
        {
            return Task.Run(() => ToStream(text));
        }

        public static bool EqualsToAny(this string text, StringComparison comparisonType, params string[] stringsToCompare)
        {
            foreach (string stringToCompare in stringsToCompare)
            {
                if (string.Equals(text, stringToCompare, comparisonType))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool Equals(this string text, StringComparison comparisonType, string stringToCompare)
        {
            return string.Equals(text, stringToCompare, comparisonType);
        }

        public static string CalculateHashMd5(this string text)
        {
            return CalculateHash(text, new MD5CryptoServiceProvider());
        }

        public static string CalculateHashSha1(this string text)
        {
            return CalculateHash(text, new SHA1CryptoServiceProvider());
        }

        public static string CalculateHash(this string text, HashAlgorithm algorithm)
        {
            if (algorithm == null)
            {
                throw new ArgumentNullException(nameof(algorithm));
            }

            byte[] md5 = algorithm.ComputeHash(Encoding.UTF8.GetBytes(text));
            return BitConverter.ToString(md5);
        }
    }
}