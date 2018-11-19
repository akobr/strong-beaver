using System.Threading;
using System.Threading.Tasks;
using StrongBeaver.Core.Model.GeoLocation;
using StrongBeaver.Core.Services;

namespace StrongBeaver.Services.Geolocation
{
    public interface IGeolocationService : IMessageBusService<IGeolocationMessage>
    {
        Task<ILocation> GetLastKnownLocationAsync();

        Task<ILocation> GetLocationAsync();

        Task<ILocation> GetLocationAsync(IGeolocationRequest request);

        Task<ILocation> GetLocationAsync(IGeolocationRequest request, CancellationToken cancelToken);
    }
}