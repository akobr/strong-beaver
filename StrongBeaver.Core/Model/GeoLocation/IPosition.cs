using System;

namespace StrongBeaver.Core.Model.GeoLocation
{
    public interface IPosition
    {
        double Latitude { get; set; }

        double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the altitude in metres relative to sea level.
        /// </summary>
        double Altitude { get; set; }

        /// <summary>
        /// Gets or sets the potential altitude error range in meters.
        /// </summary>
        double AltitudeAccuracy { get; set; }

        /// <summary>
        /// Gets or sets the potential position error radius in meters.
        /// </summary>
        double Accuracy { get; set; }

        /// <summary>
        /// Gets or sets the heading in degrees relative to true North.
        /// </summary>
        double Heading { get; set; }

        /// <summary>
        /// Gets or sets the speed in meters per second.
        /// </summary>
        double Speed { get; set; }

        DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Returns distance in metres which is quick but with poor accuracy.
        /// </summary>
        /// <param name="latitude">Latitude of second geolocation point.</param>
        /// <param name="longitude">Longitude of second geolocation point.</param>
        /// <returns>Distance in metres.</returns>
        double GetDistance(double latitude, double longitude);

        /// <summary>
        /// Returns distance in metres which is quick but with poor accuracy.
        /// </summary>
        /// <param name="secondPoint">Second geolocation point.</param>
        /// <returns>Distance in metres.</returns>
        double GetDistance(IPosition secondPoint);
    }
}