using System.Collections.Generic;
using Xamarin.Forms;

namespace TonpeiFes.Forms.Views.Pages
{
    public partial class PlanningListRootPage : ContentPage
    {
        public PlanningListRootPage()
        {
            InitializeComponent();

            SearchBar.WidthRequest = (BindingContext as ViewModels.ViewModelBase).IsiOS ? 1 : -1;
            NavigationPage.SetHasNavigationBar(this, !(BindingContext as ViewModels.ViewModelBase).IsiOS);
        }
    }
}

