using System;

namespace StrongBeaver.Core.Services.Device.Geolocator
{
    public interface IListenerSettings
    {
        /// <summary>
        /// Gets or sets whether location updates should contains heading information (iOS only). Default: false.
        /// </summary>
        bool IncludeHeading { get; }

        /// <summary>
        /// Gets or sets whether background location updates should be allowed (>= iOS 9). Default: false.
        /// </summary>
        bool AllowBackgroundUpdates { get; }

        /// <summary>
        /// Gets or sets whether location updates should be paused automatically when the location is unlikely to change (>= iOS 6). Default: true.
        /// </summary>
        bool PauseLocationUpdatesAutomatically { get; }

        /// <summary>
        /// Gets or sets the activity type that should be used to determine when to automatically pause location updates (>= iOS 6). Default: ActivityType.Other.
        /// </summary>
        ActivityTypeEnum ActivityType { get; }

        /// <summary>
        /// Gets or sets whether the location manager should only listen for significant changes in location, rather than continuous listening (>= iOS 4). Default: false.
        /// </summary>
        bool ListenForSignificantChanges { get; }

        /// <summary>
        /// Gets or sets whether the location manager should defer location updates until an energy efficient time arrives, or distance and time criteria are met (>= iOS 6). Default: false.
        /// </summary>
        bool DeferLocationUpdates { get; }

        /// <summary>
        /// If deferring location updates, the minimum distance to travel before updates are delivered (>= iOS 6, UWP). Default: 500.
        /// </summary>
        double DeferralDistanceMetres { get; }

        /// <summary>
        /// If deferring location updates, the minimum time that should elapse before updates are delivered (>= iOS 6, UWP). Default: 5 minutes.
        /// </summary>
        TimeSpan DeferralTime { get; }
    }
}