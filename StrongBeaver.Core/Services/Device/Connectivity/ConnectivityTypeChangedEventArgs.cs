using System.Collections.Generic;

namespace StrongBeaver.Core.Services.Device.Connectivity
{
    public class ConnectivityTypeChangedEventArgs : ConnectivityChangedEventArgs
    {
        public ConnectivityTypeChangedEventArgs(bool isConnected, IEnumerable<ConnectionTypeEnum> connectionTypes)
            : base(isConnected)
        {
            ConnectionTypes = connectionTypes;
        }

        public IEnumerable<ConnectionTypeEnum> ConnectionTypes { get; set; }
    }
}
