using StrongBeaver.Core.Model.GeoLocation;
using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Services.Geolocation
{
    public interface IGeolocationMessage : IServiceAsyncMesssage<ILocation, IGeolocationService>
    {
        // no operation
    }
}