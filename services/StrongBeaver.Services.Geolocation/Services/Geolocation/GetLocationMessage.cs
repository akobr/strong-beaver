using System;
using System.Threading;
using StrongBeaver.Core.Model.GeoLocation;
using StrongBeaver.Core.Services.Async;

namespace StrongBeaver.Services.Geolocation
{
    public class GetLocationMessage : ServiceAsyncMesssage<ILocation>, IGeolocationMessage, IGeolocationRequest
    {
        public GetLocationMessage()
        {
            CancellationSource = new CancellationTokenSource();
            DesiredAccuracy = GeolocationAccuracyEnum.Medium;
        }

        public TimeSpan Timeout { get; set; }

        public GeolocationAccuracyEnum DesiredAccuracy { get; set; }

        public CancellationTokenSource CancellationSource { get; }

        public void PerformMessage(IGeolocationService service)
        {
            OperationHolder = new TaskHolder<ILocation>(service.GetLocationAsync(this, CancellationSource.Token));
        }
    }
}
