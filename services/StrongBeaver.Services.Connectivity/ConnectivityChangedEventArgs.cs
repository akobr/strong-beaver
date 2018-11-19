using System;
using System.Collections.Generic;

namespace StrongBeaver.Services.Connectivity
{
    public class ConnectivityChangedEventArgs : EventArgs
    {
        public ConnectivityChangedEventArgs(NetworkAccessEnum networkAccess, IEnumerable<NetworkProfileEnum> profiles)
        {
            NetworkAccess = networkAccess;
            Profiles = profiles;
        }

        public NetworkAccessEnum NetworkAccess { get; }

        public IEnumerable<NetworkProfileEnum> Profiles { get; }
    }
}
