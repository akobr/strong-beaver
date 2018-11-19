namespace StrongBeaver.Core.Platform
{
    public interface IPlatformProvider
    {
        IDeviceInfo DeviceInfo { get; }

        IPlatformInfo PlatformInfo { get; }

        IApplicationInfo ApplicationInfo { get; }

        IEnvironmentInfo EnvironmentInfo { get; }
    }
}