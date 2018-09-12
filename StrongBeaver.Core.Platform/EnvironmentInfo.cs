namespace StrongBeaver.Core.Platform
{
    public class EnvironmentInfo : IEnvironmentInfo
    {
        public EnvironmentInfo(IPlatformInfo platform, IDeviceInfo device, IApplicationInfo application)
        {
            Platform = platform;
            Device = device;
            Application = application;
        }

        public IPlatformInfo Platform { get; }

        public IDeviceInfo Device { get; }

        public IApplicationInfo Application { get; }
    }
}
