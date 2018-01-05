using System;

namespace StrongBeaver.Core.Platform
{
    public interface IDeviceInfo
    {
        DeviceFamilyTypeEnum FamilyType { get; }

        string Family { get; }

        string Manufacturer { get; }

        string Model { get; }

        Guid Identifier { get; }
    }
}
