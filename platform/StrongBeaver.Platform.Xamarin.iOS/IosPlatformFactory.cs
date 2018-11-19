using System;
using StrongBeaver.Core.Platform;

namespace StrongBeaver.Platform
{
    public class IosPlatformFactory : IPlatformFactory
    {
        private readonly Func<IApplicationInfo> applicationInfoFactory;

        public IosPlatformFactory(Func<BaseIosApplicationInfo> applicationInfoFactory)
        {
            this.applicationInfoFactory = applicationInfoFactory ?? throw new ArgumentNullException(nameof(applicationInfoFactory));
        }

        public IDeviceInfo BuildDeviceInfo()
        {
            return new IosDeviceInfo();
        }

        public IPlatformInfo BuildPlatformInfo()
        {
            return new IosPlatformInfo();
        }

        public IApplicationInfo BuildApplicationInfo()
        {
            return applicationInfoFactory();
        }
    }
}