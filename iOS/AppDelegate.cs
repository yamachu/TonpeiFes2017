using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Microsoft.Practices.Unity;
using Prism.Unity;
using UIKit;
using SegmentedControl.FormsPlugin.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using TonpeiFes.Forms;

namespace TonpeiFes.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        private App _application;
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            app.StatusBarStyle = UIStatusBarStyle.LightContent;

            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.IoniconsModule());

            SegmentedControlRenderer.Init();
            FFImageLoading.Forms.Touch.CachedImageRenderer.Init();
            FormsPlugin.Iconize.iOS.IconControls.Init();

            Xamarin.FormsGoogleMaps.Init(Forms.Configurations.GoogleMapAPI.GOOGLE_MAPS_IOS_API_KEY);

            _application = new App(new iOSInitializer());
            LoadApplication(_application);

            UITabBar.Appearance.SelectedImageTintColor = ((Color)App.Current.Resources["Primary"]).ToUIColor();

            // NavigationBarのボーダーラインを消す
            UINavigationBar.Appearance.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            UINavigationBar.Appearance.ShadowImage = new UIImage();

            return base.FinishedLaunching(app, options);
        }

        public override void WillEnterForeground(UIApplication app)
        {
            base.WillEnterForeground(app);
            _application.iOSOnResume();
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {

        }
    }
}
