using System;

namespace StrongBeaver.Core.Platform
{
    public interface IPlatformModel
    {
        IPlatformInfo Platform { get; }

        IDeviceInfo Device { get; }

        IApplicationInfo Application { get; }
    }
}