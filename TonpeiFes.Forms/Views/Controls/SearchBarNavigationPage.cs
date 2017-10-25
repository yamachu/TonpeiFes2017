using System;

using Xamarin.Forms;

namespace TonpeiFes.Forms.Views.Controls
{
    public class SearchBarNavigationPage : ContentPage
    {
        public static readonly BindableProperty SearchTextProperty =
            BindableProperty.Create(nameof(SearchText), typeof(string), typeof(SearchBarNavigationPage), default(string),
             propertyChanged: (bindable, oldValue, newValue) =>
                                    ((SearchBarNavigationPage)bindable).SearchText = (string)newValue);

        public string SearchText
        {
            get { return (string)GetValue(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }
    }
}

