using StrongBeaver.Core.Constants;
using System;
using System.Globalization;
using UIKit;

namespace StrongBeaver.Core.Platform
{
    public class IosPlatformInfo : IPlatformInfo
    {
        internal const string MANUFACTURER = "Apple";
        private const string APP_ARCHITECTURE = "ARM";

        public IosPlatformInfo()
        {
            UIDevice device = UIDevice.CurrentDevice;

            OsName = device.SystemName;
            OsVersion = new Version(device.SystemVersion);
            OsVersionSimple = float.Parse($"{OsVersion.Major}.{OsVersion.Minor}", NumberStyles.Float, CultureInfo.InvariantCulture);
        }

        public string OsManufacturer => MANUFACTURER;

        public string Architecture => APP_ARCHITECTURE;

        public PlatformTypeEnum PlatformType => PlatformTypeEnum.AppleIOS;

        public string PlatformName => PlatformType.ToString(GlobalConstatns.DEFAULT_ENUM_STRING_FORMAT);

        public string OsName { get; }

        public Version OsVersion { get; }

        public float OsVersionSimple { get; }
    }
}
