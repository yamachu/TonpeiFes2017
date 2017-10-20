using Xamarin.Forms;

namespace TonpeiFes.Forms.Views.Pages
{
    public partial class PlanningDetailPage : ContentPage
    {
        public PlanningDetailPage()
        {
            InitializeComponent();

            // ToolbarItemにするとスペースがすごい
            if (Device.RuntimePlatform == Device.iOS)
            {
                ToolbarItems.Clear();
            }
        }
    }
}

