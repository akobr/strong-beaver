using System;
using System.Collections.Generic;
using StrongBeaver.Core.Services;
using Xamarin.Essentials;

namespace StrongBeaver.Services.Connectivity
{
    public class XamarinConnectivityService : BaseDisposableService, IConnectivityService
    {
        public XamarinConnectivityService()
        {
            Xamarin.Essentials.Connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        private void OnConnectivityChanged(object sender, Xamarin.Essentials.ConnectivityChangedEventArgs e)
        {
            ConnectivityChanged?.Invoke(this, new ConnectivityChangedEventArgs((NetworkAccessEnum)e.NetworkAccess, GetProfiles(e.Profiles)));
        }

        public event EventHandler<ConnectivityChangedEventArgs> ConnectivityChanged;

        public bool IsInternetAccessible => Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet;

        public NetworkAccessEnum NetworkAccess => (NetworkAccessEnum)Xamarin.Essentials.Connectivity.NetworkAccess;

        public IEnumerable<NetworkProfileEnum> Profiles => GetProfiles(Xamarin.Essentials.Connectivity.Profiles);

        protected override void OnDispose(bool disposing)
        {
            Xamarin.Essentials.Connectivity.ConnectivityChanged -= OnConnectivityChanged;
            base.OnDispose(disposing);
        }

        private static IEnumerable<NetworkProfileEnum> GetProfiles(IEnumerable<ConnectionProfile> profiles)
        {
            foreach (ConnectionProfile profile in profiles)
            {
                yield return (NetworkProfileEnum)profile;
            }
        }
    }
}
