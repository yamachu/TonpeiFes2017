using System;
using Xamarin.Forms;

namespace TonpeiFes.Views.Controls
{
    // https://forums.xamarin.com/discussion/47896/how-to-draw-a-separator
    public class SeparatorControl : BoxView
    {
        public SeparatorControl()
        {
            VerticalOptions = LayoutOptions.FillAndExpand;
            HeightRequest = 1;
        }
    }
}
