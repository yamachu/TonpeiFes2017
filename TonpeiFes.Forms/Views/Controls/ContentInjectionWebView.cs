using System;
using Xamarin.Forms;

namespace TonpeiFes.Forms.Views.Controls
{
    public class ContentInjectionWebView : View
    {
        public static readonly BindableProperty MyContentProperty =
            BindableProperty.Create(nameof(MyContent), typeof(string), typeof(ContentInjectionWebView), default(string),
             propertyChanged: (bindable, oldValue, newValue) =>
                                    ((ContentInjectionWebView)bindable).MyContent = (string)newValue);

        public string MyContent
        {
            get { return (string)GetValue(MyContentProperty); }
            set { SetValue(MyContentProperty, value); }
        }
    }
}
