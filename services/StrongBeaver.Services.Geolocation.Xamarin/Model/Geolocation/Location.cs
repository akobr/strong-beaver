using System;
using StrongBeaver.Core.Model.GeoLocation;
using Xamarin.Essentials;

namespace StrongBeaver.Model.Geolocation
{
    public class Location : ILocation
    {
        private readonly Xamarin.Essentials.Location wrappedLocation;

        internal Location(Xamarin.Essentials.Location location)
        {
            wrappedLocation = location;
        }

        public Location(double latitude, double longitude)
        {
            wrappedLocation = new Xamarin.Essentials.Location(latitude, longitude);
        }

        public double? Accuracy
        {
            get => wrappedLocation.Accuracy;
            set => wrappedLocation.Accuracy = value;
        }

        public double? Altitude
        {
            get => wrappedLocation.Altitude;
            set => wrappedLocation.Altitude = value;
        }

        public double Longitude
        {
            get => wrappedLocation.Longitude;
            set => wrappedLocation.Longitude = value;
        }

        public double Latitude
        {
            get => wrappedLocation.Latitude;
            set => wrappedLocation.Latitude = value;
        }

        public DateTimeOffset TimestampUtc
        {
            get => wrappedLocation.TimestampUtc;
            set => wrappedLocation.TimestampUtc = value;
        }

        public double? Course
        {
            get => wrappedLocation.Course;
            set => wrappedLocation.Course = value;
        }

        public double? Speed
        {
            get => wrappedLocation.Speed;
            set => wrappedLocation.Speed = value;
        }

        public double GetDistance(double latitude, double longitude)
        {
            return Xamarin.Essentials.Location.CalculateDistance(
                wrappedLocation,
                new Xamarin.Essentials.Location(latitude, longitude),
                DistanceUnits.Kilometers);
        }

        public double GetDistance(ILocation secondPoint)
        {
            return Xamarin.Essentials.Location.CalculateDistance(
                wrappedLocation,
                new Xamarin.Essentials.Location(secondPoint.Latitude, secondPoint.Longitude),
                DistanceUnits.Kilometers);
        }
    }
}
