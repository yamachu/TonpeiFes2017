using System;

using Xamarin.Forms;

namespace TonpeiFes.Views.Controls
{
    public class MyIconTabbedPage : FormsPlugin.Iconize.IconTabbedPage
    {
        public event TabPageChangedHandler TabChanged;
        public delegate void TabPageChangedHandler(TabInfoEventArgs e);

        public MyIconTabbedPage()
        {
            CurrentPageChanged += (s, e) =>
            {
                try
                {
                    var pageIndex = this.Children?.IndexOf(this.CurrentPage) ?? 0;
                    TabChanged(new TabInfoEventArgs(pageIndex, this.CurrentPage?.Title ?? ""));
                }
                catch(Exception ex)
                {
                    // 初回のページ生成の時に例外が起こってしまうので，その対策
                }
            };
        }
    }
}

