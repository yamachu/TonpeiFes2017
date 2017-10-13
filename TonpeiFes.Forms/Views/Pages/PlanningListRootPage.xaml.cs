using System.Collections.Generic;
using Xamarin.Forms;

namespace TonpeiFes.Forms.Views.Pages
{
    public partial class PlanningListRootPage : ContentPage
    {
        public PlanningListRootPage()
        {
            InitializeComponent();

            iOSFavIcon.IsVisible = Device.RuntimePlatform == Device.iOS;
            SearchBar.WidthRequest = Device.RuntimePlatform == Device.iOS ? 1 : -1;
            NavigationPage.SetHasNavigationBar(this, Device.RuntimePlatform == Device.Android);
        }
    }
}

