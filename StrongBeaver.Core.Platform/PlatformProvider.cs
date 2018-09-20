using System;
using System.Threading;

namespace StrongBeaver.Core.Platform
{
    public class PlatformProvider : IPlatformProvider
    {
        private readonly Lazy<IDeviceInfo> deviceInfo;
        private readonly Lazy<IPlatformInfo> platformInfo;
        private readonly Lazy<IApplicationInfo> applicationInfo;
        private readonly Lazy<IEnvironmentInfo> environmentInfo;

        public PlatformProvider(IPlatformFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            deviceInfo = new Lazy<IDeviceInfo>(factory.BuildDeviceInfo, LazyThreadSafetyMode.PublicationOnly);
            platformInfo = new Lazy<IPlatformInfo>(factory.BuildPlatformInfo, LazyThreadSafetyMode.PublicationOnly);
            applicationInfo = new Lazy<IApplicationInfo>(factory.BuildApplicationInfo, LazyThreadSafetyMode.PublicationOnly);
            environmentInfo = new Lazy<IEnvironmentInfo>(BuildEnvironmentInfo, LazyThreadSafetyMode.PublicationOnly);
        }

        public IDeviceInfo DeviceInfo => deviceInfo.Value;

        public IPlatformInfo PlatformInfo => platformInfo.Value;

        public IApplicationInfo ApplicationInfo => applicationInfo.Value;

        public IEnvironmentInfo EnvironmentInfo => environmentInfo.Value;

        private IEnvironmentInfo BuildEnvironmentInfo()
        {
            return new EnvironmentInfo(platformInfo.Value, deviceInfo.Value, applicationInfo.Value);
        }
    }
}
