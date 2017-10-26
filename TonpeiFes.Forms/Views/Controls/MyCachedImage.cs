using System;
using FFImageLoading.Forms;
using Xamarin.Forms;

namespace TonpeiFes.Forms.Views.Controls
{
    public class MyCachedImage : CachedImage
    {
        public MyCachedImage()
        {
            RetryCount = 3;
            RetryDelay = 1000;
            CacheDuration = TimeSpan.FromDays(30);
            LoadingPlaceholder = ImageSource.FromFile("loading.png");
            ErrorPlaceholder = ImageSource.FromFile("loading.png");
        }
    }
}
