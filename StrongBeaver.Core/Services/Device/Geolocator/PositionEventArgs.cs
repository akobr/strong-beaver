using StrongBeaver.Core.Model.GeoLocation;
using System;

namespace StrongBeaver.Core.Services.Device.Geolocator
{
    public class PositionEventArgs
    {
        public PositionEventArgs(IPosition position)
        {
            Position = position ?? throw new ArgumentNullException(nameof(position));
        }

        public IPosition Position
        {
            get;
            private set;
        }
    }
}