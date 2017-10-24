using System;
using Prism.Events;

namespace TonpeiFes.MobileCore.Models.EventArgs
{
    public class LocationPermissionRequestResultEventArgs
    {
        public bool Granted { get; private set; }

        public LocationPermissionRequestResultEventArgs(bool granted)
        {
            Granted = granted;
        }
    }

    public class LocationPermissionRequestResultEvent : PubSubEvent<LocationPermissionRequestResultEventArgs> { }
}
