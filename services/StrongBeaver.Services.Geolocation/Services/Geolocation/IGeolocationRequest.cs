using System;

namespace StrongBeaver.Services.Geolocation
{
    public interface IGeolocationRequest
    {
        TimeSpan Timeout { get; }

        GeolocationAccuracyEnum DesiredAccuracy { get; }
    }
}