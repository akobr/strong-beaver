namespace StrongBeaver.Core.Platform
{
    public interface IPlatformFactory
    {
        IDeviceInfo BuildDeviceInfo();

        IPlatformInfo BuildPlatformInfo();

        IApplicationInfo BuildApplicationInfo();
    }
}