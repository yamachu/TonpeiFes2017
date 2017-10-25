using System;
using Prism.Events;

namespace TonpeiFes.MobileCore.Models.EventArgs
{
    public class SwitchToClosablePageEventArgs
    {
        public string Name { get; private set; }

        public SwitchToClosablePageEventArgs(string pageName)
        {
            Name = pageName;
        }
    }

    public class SwitchToClosablePageEvent : PubSubEvent<SwitchToClosablePageEventArgs> { }
}
