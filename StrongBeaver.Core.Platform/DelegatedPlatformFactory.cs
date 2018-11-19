using System;

namespace StrongBeaver.Core.Platform
{
    public class DelegatedPlatformFactory : IPlatformFactory
    {
        private readonly Func<IDeviceInfo> deviceInfoFactory;
        private readonly Func<IPlatformInfo> platformInfoFactory;
        private readonly Func<IApplicationInfo> applicationInfoFactory;

        public DelegatedPlatformFactory(
            Func<IDeviceInfo> deviceInfoFactory,
            Func<IPlatformInfo> platformInfoFactory,
            Func<IApplicationInfo> applicationInfoFactory)
        {
            this.deviceInfoFactory = deviceInfoFactory;
            this.platformInfoFactory = platformInfoFactory;
            this.applicationInfoFactory = applicationInfoFactory;
        }

        public IDeviceInfo BuildDeviceInfo()
        {
            return deviceInfoFactory?.Invoke();
        }

        public IPlatformInfo BuildPlatformInfo()
        {
            return platformInfoFactory?.Invoke();
        }

        public IApplicationInfo BuildApplicationInfo()
        {
            return applicationInfoFactory?.Invoke();
        }
    }
}
