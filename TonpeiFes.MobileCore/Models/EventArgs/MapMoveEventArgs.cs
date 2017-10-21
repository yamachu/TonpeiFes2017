using System;
using Prism.Events;

namespace TonpeiFes.MobileCore.Models.EventArgs
{
    public class MapMoveEventArgs
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public bool IsInitialPosition { get; private set; }

        public MapMoveEventArgs(double latitude, double longitude, bool initial = false)
        {
            Latitude = latitude;
            Longitude = longitude;
            IsInitialPosition = initial;
        }
    }

    public class MapMoveEvent : PubSubEvent<MapMoveEventArgs> { }
}
