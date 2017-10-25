﻿using Prism.Mvvm;
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
            Container.RegisterTypeForNavigationOnPlatform<PlanningListRootPage, MobileCore.ViewModels.Pages.PlanningListRootPageViewModel>(
                name: "PlanningListRootPage",
                androidView: typeof(PlanningListRootPage),
                iOSView: typeof(Views.Pages.iOS.PlanningListRootPage));
            Container.RegisterTypeForNavigation<StageEventListRootPage>();
            Container.RegisterTypeForNavigation<FestaMapRootPage>();
            Container.RegisterTypeForNavigation<PlanningDetailPage>();

            Container.RegisterTypeForNavigation<DetailFloorPage>();

            Container.RegisterType<IEventAggregator, EventAggregator>(new ContainerControlledLifetimeManager());

#if LOCAL
            Container.RegisterType<IRepository<Exhibition>, MockExhibitionRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<FavoritedPlanning>, MockFavoritedPlanningRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<StageEvent>, MockStageEventRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<Stall>, MockStallRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<MyGroupHeader>, MockMyGroupHeaderRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<MapRegion>, MockMapRegionRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IDatabaseService, MockDatabaseService>(new ContainerControlledLifetimeManager());
#elif DESIGN
#else
            Container.RegisterType<IRepository<Exhibition>, ExhibitionRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<FavoritedPlanning>, FavoritedPlanningRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<StageEvent>, StageEventRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<Stall>, StallRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<MyGroupHeader>, MyGroupHeaderRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IRepository<MapRegion>, MapRegionRepository>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IDatabaseService, RealmDatabaseService>(new ContainerControlledLifetimeManager());
#endif

            Container.RegisterType<IFilterGroupingPlanning, FilterGroupingPlanning>();
            Container.RegisterType<IFilterGroupingStageEvent, FilterGroupingStageEvent>();
            Container.RegisterType<IShowPlanningDetail, ShowPlanningDetail>();
            Container.RegisterType<IShowFestaMap, ShowFestaMap>();

            Container.RegisterInstance<MobileCore.Configurations.IMapAssociated>(new MapAssociated());

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
