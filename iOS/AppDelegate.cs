using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Microsoft.Practices.Unity;
using Prism.Unity;
using UIKit;
using SegmentedControl.FormsPlugin.iOS;

namespace TonpeiFes.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.IoniconsModule());

            SegmentedControlRenderer.Init();
            FFImageLoading.Forms.Touch.CachedImageRenderer.Init();
            FormsPlugin.Iconize.iOS.IconControls.Init();

            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {

        }
    }
}
