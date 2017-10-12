using System;
using Xamarin.Forms;

namespace TonpeiFes.Views.Controls
{
    public class MyIconTabbedPage : OutlinedNonSelectedIconTabbedPage
    {
        public event TabPageChangedHandler TabChanged;
        public delegate void TabPageChangedHandler(TabInfoEventArgs e);

        public static readonly BindableProperty CurrentTabIndexProperty =
            BindableProperty.Create(nameof(CurrentTabIndex),
                                    typeof(int),
                                    typeof(MyIconTabbedPage),
                                    default(int),
                                    BindingMode.OneWayToSource,
                                    propertyChanged: (bindable, oldValue, newValue) =>
                                    ((MyIconTabbedPage)bindable).CurrentTabIndex = (int)newValue);

        public int CurrentTabIndex
        {
            get { return (int)GetValue(CurrentTabIndexProperty); }
            private set { SetValue(CurrentTabIndexProperty, value); }
        }

        public MyIconTabbedPage()
        {
            CurrentPageChanged += (s, e) =>
            {
                CurrentTabIndex = this.Children?.IndexOf(this.CurrentPage) ?? 0;
            };
        }
    }
}

