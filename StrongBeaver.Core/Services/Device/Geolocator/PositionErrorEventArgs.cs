namespace StrongBeaver.Core.Services.Device.Geolocator
{
    public class PositionErrorEventArgs
    {
        public PositionErrorEventArgs(GeolocationErrorTypeEnum error)
        {
            Error = error;
        }

        public GeolocationErrorTypeEnum Error { get; private set; }
    }
}