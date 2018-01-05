namespace StrongBeaver.Core.Platform
{
    public class DefaultPlatformModel : IPlatformModel
    {
        public DefaultPlatformModel(IPlatformInfo platform, IDeviceInfo device, IApplicationInfo application)
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
