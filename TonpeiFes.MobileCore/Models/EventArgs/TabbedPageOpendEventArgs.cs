using System;
using Prism.Events;

namespace TonpeiFes.MobileCore.Models.EventArgs
{
    public class TabbedPageOpendEventArgs
    {
        public string Name { get; private set; }

        public TabbedPageOpendEventArgs(string pageName)
        {
            Name = pageName;
        }
    }

    public class TabbedPageOpendEvent : PubSubEvent<TabbedPageOpendEventArgs> { }
}
