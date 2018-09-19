using System.Threading;
using System.Threading.Tasks;
using StrongBeaver.Core.Model.GeoLocation;
using Xamarin.Essentials;

using Location = StrongBeaver.Model.Geolocation.Location;

namespace StrongBeaver.Services.Geolocation
{
    public class XamarinGeolocationService : IGeolocationService
    {
        public async Task<ILocation> GetLastKnownLocationAsync()
        {
            return new Location(await Xamarin.Essentials.Geolocation.GetLastKnownLocationAsync());
        }

        public async Task<ILocation> GetLocationAsync()
        {
            return new Location(await Xamarin.Essentials.Geolocation.GetLocationAsync());
        }

        public async Task<ILocation> GetLocationAsync(IGeolocationRequest request)
        {
            return new Location(await Xamarin.Essentials.Geolocation.GetLocationAsync(BuildRequest(request)));
        }

        public async Task<ILocation> GetLocationAsync(IGeolocationRequest request, CancellationToken cancelToken)
        {
            return new Location(await Xamarin.Essentials.Geolocation.GetLocationAsync(BuildRequest(request), cancelToken));
        }

        private static GeolocationRequest BuildRequest(IGeolocationRequest request)
        {
            return new GeolocationRequest((GeolocationAccuracy)request.DesiredAccuracy, request.Timeout);
        }

        public void ProcessMessage(IGeolocationMessage message)
        {
            message.PerformMessage(this);
        }
    }
}
