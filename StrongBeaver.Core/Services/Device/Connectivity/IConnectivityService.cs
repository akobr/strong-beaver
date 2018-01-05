using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Device.Connectivity
{
    public interface IConnectivityService : IService
    {
        event EventHandler<ConnectivityChangedEventArgs> ConnectivityChanged;

        event EventHandler<ConnectivityTypeChangedEventArgs> ConnectivityTypeChanged;

        bool IsConnected { get; }

        IEnumerable<ConnectionTypeEnum> GetConnectionTypes();

        Task<bool> IsReachableAsync(string host);

        /// <summary>
        /// This method will ping the host.
        /// </summary>
        /// <param name="host">The host name or IP address.</param>
        /// <param name="timeoutMiliseconds">Timeout for ping.</param>
        /// <returns>True if host is alive, otherwise false.</returns>
        Task<bool> IsAliveAsync(string host, int timeoutMiliseconds = 3500);
    }
}
