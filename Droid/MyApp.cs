using System;
using Android.App;
using Android.Runtime;

namespace TonpeiFes.Droid
{
    [Application]
    [MetaData("com.google.android.maps.v2.API_KEY",
              Value = TonpeiFes.Forms.Configurations.GoogleMapAPI.GOOGLE_MAPS_ANDROID_API_KEY)]
    public class MyApp : Application
    {
        public MyApp(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
    }
}
