using Foundation;
using System;
using System.Globalization;

namespace StrongBeaver.Core.Platform
{
    public abstract class BaseIosApplicationInfo : IApplicationInfo
    {
        protected BaseIosApplicationInfo()
        {
            string appVersion = NSBundle.MainBundle.InfoDictionary["CFBundleShortVersionString"].ToString();
            string buildVersion = NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString();
            string fullVersion = $"{appVersion}.{buildVersion}";

            Version = new Version(fullVersion);
            VersionSimple = float.Parse($"{Version.Major}.{Version.Minor}", NumberStyles.Float, CultureInfo.InvariantCulture);
        }

        public abstract bool IsBackgroundTask { get; }

        public abstract string Name { get; }

        public Version Version { get; }

        public float VersionSimple { get; }

        public abstract Guid Identifier { get; }

        public abstract string Secret { get; }
    }
}
