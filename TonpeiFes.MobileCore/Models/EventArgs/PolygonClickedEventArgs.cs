using System;
using Prism.Events;

namespace TonpeiFes.MobileCore.Models.EventArgs
{
    public class PolygonClickedEventArgs
    {
        public object Tag { get; private set; }

        public PolygonClickedEventArgs(object tag)
        {
            Tag = tag;
        }
    }

    public class PolygonClickedEvent : PubSubEvent<PolygonClickedEventArgs> { }
}
