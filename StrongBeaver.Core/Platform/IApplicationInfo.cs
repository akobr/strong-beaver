using System;

namespace StrongBeaver.Core.Platform
{
    public interface IApplicationInfo
    {
        bool IsBackgroundTask { get; }

        string Name { get; }

        Version Version { get; }

        float VersionSimple { get; }

        Guid Identifier { get; }

        string Secret { get; }
    }
}
