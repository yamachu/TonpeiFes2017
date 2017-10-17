using Prism.Mvvm;
using Prism.Unity;
using Microsoft.Practices.Unity;
using TonpeiFes.Forms.Views.Pages;
using TonpeiFes.MobileCore.Repositories;
using TonpeiFes.MobileCore.Services;
using TonpeiFes.MobileCore.Usecases;
using TonpeiFes.MobileCore.Models.DataObjects;
#if LOCAL
using TonpeiFes.MobileCore.Repositories.Debug;
using TonpeiFes.MobileCore.Services.Debug;
#endif
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
                name: "AppNavigationRootPage",
                androidView: typeof(Views.Pages.Android.AppNavigationRootPage),
                iOSView: typeof(Views.Pages.iOS.AppNavigationRootPage));
            Container.RegisterTypeForNavigation<HomePage>();
            Container.RegisterTypeForNavigation<PlanningListRootPage>();
            Container.RegisterTypeForNavigation<StageEventListRootPage>();
            Container.RegisterTypeForNavigation<FestaMapRootPage>();

#if LOCAL
            Container.RegisterType<IRepository<EventDate>, MockEventDateRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<Exhibition>, MockExhibitionRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<FavoritedPlanning>, MockFavoritedPlanningRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<StageEvent>, MockStageEventRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<Stall>, MockStallRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IDatabaseService, MockDatabaseService>(new ContainerControlledLifetimeManager());
#else
            Container.RegisterType<IRepository<EventDate>, EventDateRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<Exhibition>, ExhibitionRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<FavoritedPlanning>, FavoritedPlanningRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<StageEvent>, StageEventRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<Stall>, StallRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IDatabaseService, RealmDatabaseService>(new ContainerControlledLifetimeManager());
#endif
        }

        // https://github.com/amay077/XamarinFormsGachiSample2016Winter/blob/master/XamarinFormsGachiSample2016Winter/App.cs
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(MobileCore.ViewModelTypeResolver.Resolve);
        }
    }
}
