using Prism.Mvvm;
using Prism.Unity;
using TonpeiFes.Forms.Views.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TonpeiFes.Forms
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        // To enable XAML Preview, need parameterless constructor
        public App() => this.InitializeComponent();

        protected async override void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("/AppNavigationRootPage/NavigationPage/HomePage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();

            Container.RegisterTypeForNavigationOnPlatform<AppNavigationRootPage, MobileCore.ViewModels.Pages.AppNavigationRootPageViewModel>(
                name:"AppNavigationRootPage",
                androidView:typeof(Views.Pages.Android.AppNavigationRootPage),
                iOSView:typeof(Views.Pages.iOS.AppNavigationRootPage));
            Container.RegisterTypeForNavigation<HomePage>();
            Container.RegisterTypeForNavigation<PlanningListRootPage>();
            Container.RegisterTypeForNavigation<StageEventListRootPage>();
            Container.RegisterTypeForNavigation<FestaMapRootPage>();
        }

        // https://github.com/amay077/XamarinFormsGachiSample2016Winter/blob/master/XamarinFormsGachiSample2016Winter/App.cs
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(MobileCore.ViewModelTypeResolver.Resolve);
        }
    }
}
