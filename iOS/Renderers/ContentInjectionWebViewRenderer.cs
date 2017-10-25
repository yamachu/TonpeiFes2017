using System;
using TonpeiFes.Forms.Views.Controls;
using TonpeiFes.iOS.Renderers;
using WebKit;
using Foundation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentInjectionWebView), typeof(ContentInjectionWebViewRenderer))]
namespace TonpeiFes.iOS.Renderers
{
    public class ContentInjectionWebViewRenderer : ViewRenderer<ContentInjectionWebView, WKWebView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ContentInjectionWebView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                var webView = new WKWebView(NativeView.Frame, new WKWebViewConfiguration());
                webView.ScrollView.ScrollEnabled = true;
                SetNativeControl(webView);
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
            Control.LoadHtmlString(Element.MyContent ?? "表示内容はありません", null);
        }
    }
}
