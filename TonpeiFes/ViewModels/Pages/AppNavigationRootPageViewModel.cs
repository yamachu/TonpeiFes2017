using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using Reactive.Bindings;

namespace TonpeiFes.ViewModels
{
    public class AppNavigationRootPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        // Use only Android MasterDetail Page
        public ICommand NavigationHomeCommand { get; }
        public ICommand NavigationPlanningRootCommand { get; }
        public ICommand NavigationStageEventRootCommand { get; }
        public ICommand NavigationMapRootCommand { get; }

        // Use only iOS TabbedPage
        public string iOSIconHome => "ion-ios-home";
        public string iOSIconList => "ion-ios-list";
        public string iOSIconStage => "ion-ios-calendar";
        public string iOSIconMap => "ion-ios-location";

        public ReactiveProperty<int> ReactiveCurrentTabIndex { get; } = new ReactiveProperty<int>();

        public AppNavigationRootPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            NavigationHomeCommand = new DelegateCommand(() =>
            {
                PlatformDependPageNavigation("HomePage");
            });

            NavigationPlanningRootCommand = new DelegateCommand(() =>
            {
                PlatformDependPageNavigation("PlanningListRootPage");
            });

            NavigationStageEventRootCommand = new DelegateCommand(() =>
            {
                PlatformDependPageNavigation("StageEventListRootPage");
            });

            NavigationMapRootCommand = new DelegateCommand(() =>
            {
                PlatformDependPageNavigation("FestaMapRootPage");
            });

            /*
            ReactiveCurrentTabIndex.Subscribe((value) => {
                // Handle Current Tab Index
            });
            */
        }

        private async Task PlatformDependPageNavigation(string pageName) => _navigationService.NavigateAsync($@"{(Device.RuntimePlatform == Device.Android ? "NavigationPage/" : "")}{pageName}");
    }
}
