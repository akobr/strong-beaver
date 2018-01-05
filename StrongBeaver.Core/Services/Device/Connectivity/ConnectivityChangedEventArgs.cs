using System;

namespace StrongBeaver.Core.Services.Device.Connectivity
{
    public class ConnectivityChangedEventArgs : EventArgs
    {
        public ConnectivityChangedEventArgs(bool isConnected)
        {
            IsConnected = isConnected;
        }

        public bool IsConnected { get; set; }
    }
}
