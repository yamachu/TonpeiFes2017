using System;
using Prism.Events;

namespace TonpeiFes.MobileCore.Models.EventArgs
{
    public class LocationPermissionRequestEventArgs
    {
        public LocationPermissionRequestEventArgs()
        {
        }
    }

    public class LocationPermissionRequestEvent : PubSubEvent<LocationPermissionRequestEventArgs> {}
}
