using System;

namespace StrongBeaver.Core.Model.GeoLocation
{
    public class Position : IPosition
    {
        // TODO: [P2] Refactor this !
        private const double EARTH_RADIUS = 6371007.2;

        public Position()
        {
            // No operation
        }

        public static Position Empty => new Position(double.NaN, double.NaN);

        public Position(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public Position(IPosition position)
        {
            if (position == null)
            {
                throw new ArgumentNullException(nameof(position));
            }

            Timestamp = position.Timestamp;
            Latitude = position.Latitude;
            Longitude = position.Longitude;
            Altitude = position.Altitude;
            AltitudeAccuracy = position.AltitudeAccuracy;
            Accuracy = position.Accuracy;
            Heading = position.Heading;
            Speed = position.Speed;
        }


        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Altitude { get; set; }

        public double AltitudeAccuracy { get; set; }

        public double Accuracy { get; set; }

        public double Speed { get; set; }

        public double Heading { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public double GetDistance(double latitude, double longitude)
        {
            return GetDistance(new Position(latitude, longitude));
        }

        public double GetDistance(IPosition secondPoint)
        {
            return GetJavaScriptDistance(secondPoint);
        }

        public bool IsEmpty()
        {
            return IsEmpty(this);
        }

        public static bool IsEmpty(IPosition position)
        {
            return double.IsNaN(position.Latitude) && double.IsNaN(position.Longitude);
        }

        private static IPosition ToRadians(IPosition point)
        {
            return new Position(ToRadians(point.Latitude), ToRadians(point.Longitude));
        }

        private static double ToRadians(double value)
        {
            return value * Math.PI / 180;
        }

        private double GetDistanceSphere(IPosition secondPoint)
        {
            IPosition aRad = ToRadians(this);
            IPosition bRad = ToRadians(secondPoint);

            double dLatitude = bRad.Latitude - aRad.Latitude;
            double dLongitude = bRad.Longitude - aRad.Longitude;

            double s = Math.Sin(dLatitude * 0.5) * Math.Sin(dLatitude * 0.5) +
                       Math.Sin(dLongitude * 0.5) * Math.Sin(dLongitude * 0.5) * Math.Cos(aRad.Latitude) * Math.Cos(bRad.Latitude);
            double c = 2 * Math.Atan2(Math.Sqrt(s), Math.Sqrt(1 - s));
            return EARTH_RADIUS * c;
        }

        private double GetDistanceSphereSimple(IPosition secondPoint)
        {
            IPosition aRad = ToRadians(this);
            IPosition bRad = ToRadians(secondPoint);

            return Math.Acos(Math.Sin(aRad.Latitude) * Math.Sin(bRad.Latitude) +
                             Math.Cos(aRad.Latitude) * Math.Cos(bRad.Latitude) *
                             Math.Cos(bRad.Longitude - aRad.Longitude)) * EARTH_RADIUS;
        }

        private double GetEquirectangularDistance(IPosition secondPoint)
        {
            IPosition aRad = ToRadians(this);
            IPosition bRad = ToRadians(secondPoint);

            double x = (bRad.Longitude - aRad.Longitude) * Math.Cos((aRad.Latitude + bRad.Latitude) * 0.5);
            double y = (aRad.Latitude - bRad.Latitude);
            return Math.Sqrt(x* x + y* y) * EARTH_RADIUS;
        }

        private double GetJavaScriptDistance(IPosition secondPoint)
        {
            double rlat1 = Math.PI * Latitude / 180;
            double rlat2 = Math.PI * secondPoint.Latitude / 180;
            double theta = Longitude - secondPoint.Longitude;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            return (dist*180/Math.PI)*60*1.1515*1609.344;
        }
    }
}