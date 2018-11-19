using System;

namespace StrongBeaver.Core.Platform
{
    public interface IPlatformInfo
    {
        string OsManufacturer { get; }

        string Architecture { get; }

        PlatformTypeEnum PlatformType { get; }

        string PlatformName { get; }

        string OsName { get; }

        Version OsVersion { get; }

        float OsVersionSimple { get; }
    }
}
