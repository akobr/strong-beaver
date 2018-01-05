namespace StrongBeaver.Core.Services.Device.Geolocator
{
    public enum GeolocationErrorTypeEnum
    {
        /// <summary>
        /// The provider was unable to retrieve any position data.
        /// </summary>
        PositionUnavailable = 0,

        /// <summary>
        /// The app is not, or no longer, authorized to receive location data.
        /// </summary>
        Unauthorized
    }
}