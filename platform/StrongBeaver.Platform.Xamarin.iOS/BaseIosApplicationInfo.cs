using System;
using System.Globalization;
using Foundation;
using StrongBeaver.Core.Platform;

namespace StrongBeaver.Platform
{
    public abstract class BaseIosApplicationInfo : IApplicationInfo
    {
        protected BaseIosApplicationInfo()
        {
            string appVersion = NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString();

            Version = new Version(appVersion);
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
