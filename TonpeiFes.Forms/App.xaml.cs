using Prism.Mvvm;
using Prism.Unity;
using Microsoft.Practices.Unity;
using TonpeiFes.Forms.Views.Pages;
using TonpeiFes.MobileCore.Repositories;
using TonpeiFes.MobileCore.Services;
using TonpeiFes.MobileCore.Usecases;
using TonpeiFes.Core.Models.DataObjects;
#if LOCAL || DESIGN
using TonpeiFes.MobileCore.Repositories.Debug;
using TonpeiFes.MobileCore.Services.Debug;
#endif
#if DESIGN
using TonpeiFes.MobileCore.DesignViewModels.Pages;
#endif
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Events;
using System.Threading.Tasks;
using TonpeiFes.MobileCore.Models.EventArgs;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using TonpeiFes.Forms.Configurations;
using TonpeiFes.Forms.Service;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TonpeiFes.Forms
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
#if DEBUG
            MobileCenter.Start($"android={MobileCenterDebugAPI.MOBILE_CENTER_ANDROID_API_KEY};ios={MobileCenterDebugAPI.MOBILE_CENTER_IOS_API_KEY}",
                   typeof(Analytics), typeof(Crashes));
#else
            MobileCenter.Start($"android={MobileCenterAPI.MOBILE_CENTER_ANDROID_API_KEY};ios={MobileCenterAPI.MOBILE_CENTER_IOS_API_KEY}",
                   typeof(Analytics), typeof(Crashes));
#endif
        }

        // To enable XAML Preview, need parameterless constructor
        public App() => this.InitializeComponent();

        protected async override void OnInitialized()
        {
            InitializeComponent();

            var sent = await Container.Resolve<ILocalConfigService>().GetEnqueteSentAsync();
            if (sent)
            {
                await NavigationService.NavigateAsync("/AppNavigationRootPage/NavigationPage/HomePage");
            }
            else
            {
                await NavigationService.NavigateAsync("/NavigationPage/EnquetePage");
            }
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();

            Container.RegisterTypeForNavigationOnPlatform<AppNavigationRootPage, MobileCore.ViewModels.Pages.AppNavigationRootPageViewModel>(
                name: "AppNavigationRootPage",
                androidView: typeof(Views.Pages.Android.AppNavigationRootPage),
                iOSView: typeof(Views.Pages.iOS.AppNavigationRootPage));
            Container.RegisterTypeForNavigation<HomePage>();
            Container.RegisterTypeForNavigationOnPlatform<PlanningListRootPage, MobileCore.ViewModels.Pages.PlanningListRootPageViewModel>(
                name: "PlanningListRootPage",
                androidView: typeof(PlanningListRootPage),
                iOSView: typeof(Views.Pages.iOS.PlanningListRootPage));
            Container.RegisterTypeForNavigation<StageEventListRootPage>();
            Container.RegisterTypeForNavigation<FestaMapRootPage>();
            Container.RegisterTypeForNavigation<PlanningDetailPage>();
            Container.RegisterTypeForNavigation<RegionSpecificPlanningListPage, MobileCore.ViewModels.Pages.PlanningListRootPageViewModel>();
            Container.RegisterTypeForNavigation<EnquetePage>();
            Container.RegisterTypeForNavigation<VoteAnnouncePage>();

            Container.RegisterTypeForNavigation<DetailFloorPage>();
            Container.RegisterTypeForNavigation<ClosableInternalWebViewPage>();

            Container.RegisterType<IEventAggregator, EventAggregator>(new ContainerControlledLifetimeManager());

#if LOCAL
            Container.RegisterType<IRepository<Exhibition>, MockExhibitionRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<FavoritedPlanning>, MockFavoritedPlanningRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<StageEvent>, MockStageEventRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<Stall>, MockStallRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<MyGroupHeader>, MockMyGroupHeaderRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<MapRegion>, MockMapRegionRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<Announcement>, MockAnnouncementRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IDatabaseService, MockDatabaseService>(new ContainerControlledLifetimeManager());
#elif DESIGN
#else
            Container.RegisterType<IRepository<Exhibition>, ExhibitionRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<FavoritedPlanning>, FavoritedPlanningRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<StageEvent>, StageEventRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<Stall>, StallRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<MyGroupHeader>, MyGroupHeaderRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<MapRegion>, MapRegionRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<Announcement>, AnnouncementRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IDatabaseService, RealmDatabaseService>(new ContainerControlledLifetimeManager());
#endif

            Container.RegisterType<IFilterGroupingPlanning, FilterGroupingPlanning>();
            Container.RegisterType<IFilterGroupingStageEvent, FilterGroupingStageEvent>();
            Container.RegisterType<IShowPlanningDetail, ShowPlanningDetail>();
            Container.RegisterType<IShowFestaMap, ShowFestaMap>();
            Container.RegisterType<IShowAnnouncement, ShowAnnouncement>();

            Container.RegisterType<MobileCore.Configurations.IMapAssociated, MapAssociated>();
            Container.RegisterType<IOpenWebPageService, OpenWebPageService>();
            Container.RegisterType<IAnalyticsService, AnalyticsService>();
            Container.RegisterType<MobileCore.Configurations.IConstUrls, ConstUrls>();
            Container.RegisterType<ILocalConfigService, LocalConfigService>(new ContainerControlledLifetimeManager());

            Task.Run(async () =>
            {
                await MyInitializeModules();
            });
        }

        // https://github.com/amay077/XamarinFormsGachiSample2016Winter/blob/master/XamarinFormsGachiSample2016Winter/App.cs
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
#if !DESIGN
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(MobileCore.ViewModelTypeResolver.Resolve);
#else
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(MobileCore.DesignViewModelTypeResolver.Resolve);
#endif
        }

        private async Task MyInitializeModules()
        {
            Container.Resolve<IEventAggregator>()
                     .GetEvent<LocationPermissionRequestEvent>()
                     .Subscribe(async (_) =>
            {
                var granted = await CheckLocationPermissionAsync();
                Container.Resolve<IEventAggregator>()
                         .GetEvent<LocationPermissionRequestResultEvent>()
                         .Publish(new LocationPermissionRequestResultEventArgs(granted));
            });
        }

        private async Task<bool> CheckLocationPermissionAsync()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            if (status != PermissionStatus.Granted)
            {
                status = (await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location))[Permission.Location];
            }
            return status == PermissionStatus.Granted;
        }
    }
}
