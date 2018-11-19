using StrongBeaver.Core.Model.GeoLocation;
using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Services.Geolocation
{
    public class GetLastKnownLocationMessage : ServiceAsyncMesssage<ILocation>, IGeolocationMessage
    {
        public void PerformMessage(IGeolocationService service)
        {
            OperationHolder = new TaskHolder<ILocation>(service.GetLastKnownLocationAsync());
        }
    }
}
