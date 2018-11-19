using System;

namespace StrongBeaver.Core.Model.GeoLocation
{
    public interface ILocation
    {
        /// <summary>
        /// Gets the accuracy (in meters) of the location.
        /// The potential position error radius in meters.
        /// </summary>
        double? Accuracy { get; }

        /// <summary>
        /// Gets or sets the altitude in metres relative to sea level.
        /// </summary>
        double? Altitude { get; }

        /// <summary>
        /// Gets or sets the longitude of location.
        /// </summary>
        double Longitude { get; }

        /// <summary>
        /// Gets or sets the latitude of location.
        /// </summary>
        double Latitude { get; }

        /// <summary>
        /// Gets or sets the timestamp of the location.
        /// </summary>
        DateTimeOffset TimestampUtc { get; }

        /// <summary>
        /// Gest or sets degrees relative to true north.
        /// </summary>
        double? Course { get; }


        /// <summary>
        /// Gets or sets the speed in meters per second.
        /// </summary>
        double? Speed { get; }

        /// <summary>
        /// Calculates distance between two locations in metres.
        /// </summary>
        /// <param name="latitude">Latitude of the second location.</param>
        /// <param name="longitude">Longitude of the second location.</param>
        /// <returns>The distance in metres.</returns>
        double GetDistance(double latitude, double longitude);

        /// <summary>
        /// Calculates distance between two locations in metres.
        /// </summary>
        /// <param name="secondPoint">The second location.</param>
        /// <returns>The distance in metres.</returns>
        double GetDistance(ILocation secondPoint);
    }
}