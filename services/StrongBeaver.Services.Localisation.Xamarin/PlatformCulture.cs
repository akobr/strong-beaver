using System;

namespace StrongBeaver.Services.Localisation
{
    public class PlatformCulture
    {
        public PlatformCulture(string platformCultureString)
        {
            if (string.IsNullOrEmpty(platformCultureString))
            {
                throw new ArgumentException("Expected culture identifier.", nameof(platformCultureString));
            }

            // .NET expects dash, not underscore
            PlatformString = platformCultureString.Replace("_", "-");

            int dashIndex = PlatformString.IndexOf("-", StringComparison.Ordinal);

            if (dashIndex > 0)
            {
                string[] parts = PlatformString.Split('-');
                LanguageCode = parts[0];
                LocaleCode = parts[1];
            }
            else
            {
                LanguageCode = PlatformString;
                LocaleCode = "";
            }
        }

        public string PlatformString { get; }

        public string LanguageCode { get; }

        public string LocaleCode { get; }

        public override string ToString()
        {
            return PlatformString;
        }
    }
}
