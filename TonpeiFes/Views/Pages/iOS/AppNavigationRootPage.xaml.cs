using Xamarin.Forms;

namespace TonpeiFes.Views.Pages.iOS
{
    public partial class AppNavigationRootPage : Controls.MyIconTabbedPage
    {
        public AppNavigationRootPage()
        {
            InitializeComponent();
            TabChanged += (BindingContext as ViewModels.AppNavigationRootPageViewModel).SelectedPageChanged;
        }
    }
}

