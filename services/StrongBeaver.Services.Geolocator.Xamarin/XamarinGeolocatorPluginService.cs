using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using StrongBeaver.Core.Model.GeoLocation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StrongBeaver.Core.Services.Device.Geolocator
{
    public class XamarinGeolocatorPluginService : BaseService, IGeolocatorService
    {
        private readonly IGeolocator geolocator;

        public XamarinGeolocatorPluginService()
        {
            geolocator = CrossGeolocator.Current;
            RegisterEvents();
        }

        public event EventHandler<PositionErrorEventArgs> PositionError;

        public event EventHandler<PositionEventArgs> PositionChanged;

        public double DesiredAccuracy
        {
            get
            {
                if (!CrossGeolocator.IsSupported)
                {
                    return -1.0;
                }

                return geolocator.DesiredAccuracy;
            }
            set
            {
                if (!CrossGeolocator.IsSupported)
                {
                    return;
                }

                geolocator.DesiredAccuracy = value;
            }
        }

        public bool IsListening
        {
            get
            {
                if (!CrossGeolocator.IsSupported)
                {
                    return false;
                }

                return geolocator.IsListening;
            }
        }

        public bool SupportsHeading
        {
            get
            {
                if (!CrossGeolocator.IsSupported)
                {
                    return false;
                }

                return geolocator.SupportsHeading;
            }
        }

        public bool IsGeolocationAvailable
        {
            get
            {
                if (!CrossGeolocator.IsSupported)
                {
                    return false;
                }

                return geolocator.IsGeolocationAvailable;
            }
        }

        public bool IsGeolocationEnabled
        {
            get
            {
                if (!CrossGeolocator.IsSupported)
                {
                    return false;
                }

                return geolocator.IsGeolocationEnabled;
            }
        }

        public async Task<IPosition> GetPositionAsync()
        {
            if (!CrossGeolocator.IsSupported)
            {
                return Model.GeoLocation.Position.Empty;
            }

            return TranslatePosition(await geolocator.GetPositionAsync());
        }

        public async Task<IPosition> GetPositionAsync(TimeSpan timeout)
        {
            if (!CrossGeolocator.IsSupported)
            {
                return Model.GeoLocation.Position.Empty;
            }

            return TranslatePosition(await geolocator.GetPositionAsync(timeout));
        }

        public async Task<IPosition> GetPositionAsync(TimeSpan timeout, bool includeHeading, CancellationToken token)
        {
            if (!CrossGeolocator.IsSupported)
            {
                return Model.GeoLocation.Position.Empty;
            }

            return TranslatePosition(await geolocator.GetPositionAsync(timeout, token, includeHeading));
        }

        public Task<bool> StartListeningAsync(ListenerSettings listenerSettings)
        {
            if (!CrossGeolocator.IsSupported)
            {
                return Task.FromResult(false);
            }

            return geolocator.StartListeningAsync(listenerSettings.DeferralTime, listenerSettings.DeferralDistanceMetres, listenerSettings.IncludeHeading, TranslateListenerSettings(listenerSettings));
        }

        public Task<bool> StopListeningAsync()
        {
            if (!CrossGeolocator.IsSupported)
            {
                return Task.FromResult(false);
            }

            return geolocator.StopListeningAsync();
        }

        private void RegisterEvents()
        {
            if (!CrossGeolocator.IsSupported)
            {
                return;
            }

            geolocator.DesiredAccuracy = 20;
            geolocator.PositionChanged += HandleGeolocatorPositionChanged;
            geolocator.PositionError += HandleGeolocatorPositionError;
        }

        private void HandleGeolocatorPositionError(object sender, Plugin.Geolocator.Abstractions.PositionErrorEventArgs e)
        {
            if (PositionError == null)
            {
                return;
            }

            PositionError(this, new PositionErrorEventArgs((GeolocationErrorTypeEnum)(int)e.Error));
        }

        private void HandleGeolocatorPositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            if (PositionChanged == null)
            {
                return;
            }

            PositionChanged(this, new PositionEventArgs(TranslatePosition(e.Position)));
        }

        private IPosition TranslatePosition(Plugin.Geolocator.Abstractions.Position position)
        {
            if (position == null)
            {
                return null;
            }

            return new Model.GeoLocation.Position(position.Latitude, position.Longitude)
            {
                Altitude = position.Altitude,
                Accuracy = position.Accuracy,
                AltitudeAccuracy = position.AltitudeAccuracy,
                Speed = position.Speed,
                Heading = position.Heading,
                Timestamp = position.Timestamp,
            };
        }

        private Plugin.Geolocator.Abstractions.ListenerSettings TranslateListenerSettings(ListenerSettings listenerSettings)
        {
            if (listenerSettings == null)
            {
                return null;
            }

            return new Plugin.Geolocator.Abstractions.ListenerSettings()
            {
                AllowBackgroundUpdates = listenerSettings.AllowBackgroundUpdates,
                DeferLocationUpdates = listenerSettings.DeferLocationUpdates,
                DeferralDistanceMeters = listenerSettings.DeferralDistanceMetres,
                DeferralTime = listenerSettings.DeferralTime,
                ListenForSignificantChanges = listenerSettings.ListenForSignificantChanges,
                PauseLocationUpdatesAutomatically = listenerSettings.PauseLocationUpdatesAutomatically,
                ActivityType = (ActivityType)(int)listenerSettings.ActivityType,
            };
        }
    }
}
