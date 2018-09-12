namespace StrongBeaver.Core.Platform
{
    public interface IEnvironmentInfo
    {
        IPlatformInfo Platform { get; }

        IDeviceInfo Device { get; }

        IApplicationInfo Application { get; }
    }
}