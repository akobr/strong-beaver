using System.Globalization;
using System.Threading;
using Foundation;
using StrongBeaver.Core.Services;

namespace StrongBeaver.Services.Localisation
{
    /// <summary>
    /// Based on Xamarin DEV documentation.
    /// https://developer.xamarin.com/guides/xamarin-forms/advanced/localization/
    /// </summary>
    public class IosLocalisationService : BaseDisposableService, ILocalisationService
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            var netLanguage = "en";

            if (NSLocale.PreferredLanguages.Length > 0)
            {
                var pref = NSLocale.PreferredLanguages[0];
                netLanguage = IosToDotNetLanguage(pref);
            }

            // this gets called a lot - try/catch can be expensive so consider caching or something
            CultureInfo cultureInfo = null;

            try
            {
                cultureInfo = new CultureInfo(netLanguage);
            }
            catch (CultureNotFoundException)
            {
                // iOS locale not valid .NET culture (eg. "en-ES" : English in Spain)
                // fallback to first characters, in this case "en"

                try
                {
                    string fallback = ToDotNetFallbackLanguage(new PlatformCulture(netLanguage));
                    cultureInfo = new CultureInfo(fallback);
                }
                catch (CultureNotFoundException)
                {
                    // iOS language not valid .NET culture, falling back to English
                    cultureInfo = new CultureInfo("en");
                }
            }

            return cultureInfo;
        }

        public void SetCulture(CultureInfo cultureInfo)
        {
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }

        private string IosToDotNetLanguage(string iosLanguage)
        {
            var netLanguage = iosLanguage;

            //certain languages need to be converted to CultureInfo equivalent
            switch (iosLanguage)
            {
                case "ms-MY":   // "Malaysian (Malaysia)" not supported .NET culture
                case "ms-SG":   // "Malaysian (Singapore)" not supported .NET culture
                    netLanguage = "ms"; // closest supported
                    break;

                case "gsw-CH":  // "Schwiizertüütsch (Swiss German)" not supported .NET culture
                    netLanguage = "de-CH"; // closest supported
                    break;

                    // add more application-specific cases here (if required)
                    // ONLY use cultures that have been tested and known to work
            }

            return netLanguage;
        }

        private string ToDotNetFallbackLanguage(PlatformCulture platformCultureHelper)
        {
            // use the first part of the identifier (two chars, usually);
            string netLanguage = platformCultureHelper.LanguageCode;

            switch (platformCultureHelper.LanguageCode)
            {
                case "pt":
                    netLanguage = "pt-PT"; // fallback to Portuguese (Portugal)
                    break;

                case "gsw":
                    netLanguage = "de-CH"; // equivalent to German (Switzerland) for this app
                    break;

                    // add more application-specific cases here (if required)
                    // ONLY use cultures that have been tested and known to work
            }

            return netLanguage;
        }
    }
}
