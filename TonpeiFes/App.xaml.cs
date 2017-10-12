using Prism.Unity;
using TonpeiFes.Views.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TonpeiFes
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        // To enable XAML Preview, need parameterless constructor
        public App() => this.InitializeComponent();

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("/AppNavigationRootPage/NavigationPage/HomePage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();

            Container.RegisterTypeForNavigationOnPlatform<AppNavigationRootPage, ViewModels.AppNavigationRootPageViewModel>(
                name:"AppNavigationRootPage",
                androidView:typeof(Views.Pages.Android.AppNavigationRootPage),
                iOSView:typeof(Views.Pages.iOS.AppNavigationRootPage));
            Container.RegisterTypeForNavigation<HomePage>();
            Container.RegisterTypeForNavigation<PlanningListRootPage>();
            Container.RegisterTypeForNavigation<StageEventListRootPage>();
            Container.RegisterTypeForNavigation<FestaMapRootPage>();
        }
    }
}
