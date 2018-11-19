using System;
using StrongBeaver.Core.Constants;
using StrongBeaver.Core.Extensions;
using StrongBeaver.Core.Platform;
using UIKit;

namespace StrongBeaver.Platform
{
    public class IosDeviceInfo : IDeviceInfo
    {
        public IosDeviceInfo()
        {
            UIDevice device = UIDevice.CurrentDevice;

            Model = device.Model;
            FamilyType = GetDeviceFamilyType(device);
            Identifier = new Guid(device.IdentifierForVendor.AsString().ConvertToGuidString());
        }

        public DeviceFamilyTypeEnum FamilyType { get; }

        public string Family => FamilyType.ToString(GlobalConstatns.DEFAULT_ENUM_STRING_FORMAT);

        public string Manufacturer => IosPlatformInfo.MANUFACTURER;

        public string Model { get; }

        public Guid Identifier { get; }

        private static DeviceFamilyTypeEnum GetDeviceFamilyType(UIDevice device)
        {
            switch (device.UserInterfaceIdiom)
            {
                case UIUserInterfaceIdiom.Pad:
                    return DeviceFamilyTypeEnum.Tablet;

                case UIUserInterfaceIdiom.Phone:
                    return DeviceFamilyTypeEnum.Phone;

                case UIUserInterfaceIdiom.TV:
                    return DeviceFamilyTypeEnum.TV;

                case UIUserInterfaceIdiom.CarPlay:
                    return DeviceFamilyTypeEnum.Car;

                // case UIUserInterfaceIdiom.Unspecified:
                default:
                    return DeviceFamilyTypeEnum.Unknown;
            }
        }
    }
}
