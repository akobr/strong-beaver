using System;
using System.Collections.Generic;
using StrongBeaver.Core.Services;

namespace StrongBeaver.Services.Connectivity
{
    public interface IConnectivityService : IService, IDisposable
    {
        event EventHandler<ConnectivityChangedEventArgs> ConnectivityChanged;

        bool IsInternetAccessible { get; }

        NetworkAccessEnum NetworkAccess { get; }

        IEnumerable<NetworkProfileEnum> Profiles { get; }
    }
}
