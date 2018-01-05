using System;

namespace StrongBeaver.Core.Services.Device.Geolocator
{
    public class ListenerSettings : IListenerSettings
    {
        public ListenerSettings()
        {
            // No operation
        }

        public ListenerSettings(double deferralDistanceMetres, TimeSpan deferralTime)
        {
            DeferralDistanceMetres = deferralDistanceMetres;
            DeferralTime = deferralTime;
        }

        public bool IncludeHeading { get; set; } = false;

		public bool AllowBackgroundUpdates { get; set; } = false;

        public bool PauseLocationUpdatesAutomatically { get; set; } = true;

        public ActivityTypeEnum ActivityType { get; set; } = ActivityTypeEnum.Other;

        public bool ListenForSignificantChanges { get; set; } = false;

        public bool DeferLocationUpdates { get; set; } = false;

        public double DeferralDistanceMetres { get; set; } = 500;

        public TimeSpan DeferralTime { get; set; } = TimeSpan.FromMinutes(5);
    }
}