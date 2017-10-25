using System;
using TonpeiFes.Droid.Renderers;
using TonpeiFes.Forms.Views.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ContentInjectionWebView), typeof(ContentInjectionWebViewRenderer))]
namespace TonpeiFes.Droid.Renderers
{
    public class ContentInjectionWebViewRenderer : ViewRenderer<ContentInjectionWebView, global::Android.Webkit.WebView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ContentInjectionWebView> e)
        {
            base.OnElementChanged(e);

            if (this.Control == null)
            {
                var webView = new Android.Webkit.WebView(this.Context);
                webView.Settings.BuiltInZoomControls = true;
                webView.Settings.DisplayZoomControls = true;
                this.SetNativeControl(webView);
            }
            if (e.NewElement != null)
            {
                UpdateContent();
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ContentInjectionWebView.MyContentProperty.PropertyName)
            {
                UpdateContent();
            }
        }

        private void UpdateContent()
        {
            if (Element == null) return;

            Control.LoadData(Element.MyContent, "text/html", "UTF-8");
        }
    }
}
