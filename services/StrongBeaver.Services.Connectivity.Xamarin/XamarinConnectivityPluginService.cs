using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Device.Connectivity
{
    public class XamarinConnectivityPluginService : BaseService, IConnectivityService
    {
        private readonly IConnectivity connectivity;

        public XamarinConnectivityPluginService()
        {
            connectivity = CrossConnectivity.Current;
            RegisterEvents();
        }

        public event EventHandler<ConnectivityChangedEventArgs> ConnectivityChanged;

        public event EventHandler<ConnectivityTypeChangedEventArgs> ConnectivityTypeChanged;

        public bool IsConnected => CrossConnectivity.IsSupported && connectivity.IsConnected;

        protected override void OnDispose(bool disposing)
        {
            connectivity?.Dispose();
        }

        public IEnumerable<ConnectionTypeEnum> GetConnectionTypes()
        {
            if (!CrossConnectivity.IsSupported)
            {
                return Enumerable.Empty<ConnectionTypeEnum>();
            }

            return TranslateConnectionTypes(connectivity.ConnectionTypes);
        }

        public Task<bool> IsAliveAsync(string host, int timeoutMiliseconds = 3500)
        {
            if(!CrossConnectivity.IsSupported)
            {

            }

            return connectivity.IsReachable(host, timeoutMiliseconds);
        }

        public Task<bool> IsReachableAsync(string host)
        {
            return connectivity.IsRemoteReachable(host);
        }

        private void RegisterEvents()
        {
            if (!CrossConnectivity.IsSupported)
            {
                return;
            }

            connectivity.ConnectivityChanged += HandleConnectivityChanged;
            connectivity.ConnectivityTypeChanged += HandleConnectivityTypeChanged;
        }

        private void HandleConnectivityTypeChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityTypeChangedEventArgs e)
        {
            if (ConnectivityTypeChanged == null)
            {
                return;
            }

            ConnectivityTypeChanged(this, new ConnectivityTypeChangedEventArgs(e.IsConnected, TranslateConnectionTypes(e.ConnectionTypes)));
        }

        private void HandleConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            if (ConnectivityChanged == null)
            {
                return;
            }

            ConnectivityChanged(this, new ConnectivityChangedEventArgs(e.IsConnected));

            if (ConnectivityTypeChanged == null)
            {
                return;
            }

            if (!e.IsConnected)
            {
                ConnectivityTypeChanged(this, new ConnectivityTypeChangedEventArgs(false, new[] { ConnectionTypeEnum.NotConnected }));
            }
            else
            {
                ConnectivityTypeChanged(this, new ConnectivityTypeChangedEventArgs(true, GetConnectionTypes()));
            }
        }

        private IEnumerable<ConnectionTypeEnum> TranslateConnectionTypes(IEnumerable<ConnectionType> pluginTypes)
        {
            if (pluginTypes == null)
            {
                yield break;
            }

            foreach (ConnectionType conType in connectivity.ConnectionTypes)
            {
                switch (conType)
                {
                    case ConnectionType.Cellular:
                        yield return ConnectionTypeEnum.Cellular;
                        break;

                    case ConnectionType.WiFi:
                        yield return ConnectionTypeEnum.WiFi;
                        break;

                    case ConnectionType.Desktop:
                        yield return ConnectionTypeEnum.Desktop;
                        break;

                    case ConnectionType.Wimax:
                        yield return ConnectionTypeEnum.Wimax;
                        break;

                    case ConnectionType.Bluetooth:
                        yield return ConnectionTypeEnum.Bluetooth;
                        break;

                    default:
                        yield return ConnectionTypeEnum.Other;
                        break;
                }
            }
        }
    }
}
