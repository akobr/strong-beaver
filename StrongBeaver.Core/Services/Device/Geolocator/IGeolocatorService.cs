using StrongBeaver.Core.Model.GeoLocation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Device.Geolocator
{
    public interface IGeolocatorService : IService
    {
        event EventHandler<PositionErrorEventArgs> PositionError;

        event EventHandler<PositionEventArgs> PositionChanged;

        double DesiredAccuracy { get; set; }

        bool IsListening { get; }

        bool SupportsHeading { get; }

        bool IsGeolocationAvailable { get; }

        bool IsGeolocationEnabled { get; }

        Task<IPosition> GetPositionAsync();

        Task<IPosition> GetPositionAsync(TimeSpan timeout);

        Task<IPosition> GetPositionAsync(TimeSpan timeout, bool includeHeading, CancellationToken token);

        Task<bool> StartListeningAsync(ListenerSettings listenerSettings);

        Task<bool> StopListeningAsync();
    }
}