using System;
namespace TonpeiFes.Views.Controls
{
    public class TabInfoEventArgs : EventArgs
    {
        public int PageIndex { get; private set; }
        public string PageTitle { get; private set; }

        public TabInfoEventArgs(int PageIndex, string PageTitle)
        {
            this.PageIndex = PageIndex;
            this.PageTitle = PageTitle;
        }
    }
}
