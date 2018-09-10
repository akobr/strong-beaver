using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using StrongBeaver.Core.Constants;

namespace StrongBeaver.Core.Extensions
{
    public static class GuidExtensions
    {
        private const int GUID_STRING_LENGTH = 32;
        private const char GUID_ZERO_CHARACTER = '0';
        private static readonly char[] ValidGuidCharacters = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'};

        private static HashSet<char> validGuidCharacterSet = null;
        private static readonly object setLocker = new object();

        public static string RemoveGuidSeparators(this string text)
        {
            return Regex.Replace(text, GlobalConstatns.DEFAULT_GUID_SEPARATOR, string.Empty);
        }

        public static string ConvertToGuidCharacters(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            StringBuilder result = new StringBuilder(text.Length);

            foreach (char character in text)
            {
                result.Append(ConvertToGuidCharacter(character));
            }

            return result.ToString();
        }

        public static string ConvertToGuidString(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return new string(GUID_ZERO_CHARACTER, GUID_STRING_LENGTH);
            }

            string withoutSeparators = RemoveGuidSeparators(text);

            return withoutSeparators.Length > GUID_STRING_LENGTH
                ? ConvertLongTextToGuidString(withoutSeparators)
                : ConvertShortTextToGuidString(withoutSeparators);
        }

        private static string ConvertLongTextToGuidString(string text)
        {
            string firstHalf = text.Substring(0, GUID_STRING_LENGTH / 2);
            string secondHalf = text.Substring(text.Length - (GUID_STRING_LENGTH/2 + 1), GUID_STRING_LENGTH/2);

            return $"{ConvertToGuidCharacters(firstHalf)}{ConvertToGuidCharacters(secondHalf)}";
        }

        private static string ConvertShortTextToGuidString(string text)
        {
            return ConvertToGuidCharacters(text)
                + new string(GUID_ZERO_CHARACTER, GUID_STRING_LENGTH - text.Length);
        }

        private static char ConvertToGuidCharacter(char character)
        {
            if(validGuidCharacterSet == null)
            {
                TryCreateCharacterSet();
            }

            char lowerCharacter = char.ToLowerInvariant(character);

            if(validGuidCharacterSet.Contains(lowerCharacter))
            {
                return lowerCharacter;
            }

            return ValidGuidCharacters[lowerCharacter % ValidGuidCharacters.Length];
        }

        private static void TryCreateCharacterSet()
        {
            lock (setLocker)
            {
                if (validGuidCharacterSet == null)
                {
                    validGuidCharacterSet = new HashSet<char>(ValidGuidCharacters);
                }
            }
        }
    }
}