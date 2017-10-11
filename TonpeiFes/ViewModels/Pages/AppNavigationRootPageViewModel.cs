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
        private const string iOSIconOutline = "-outline";
        private const string iOSIconHome = "ion-ios-home";
        private const string iOSIconList = "ion-ios-list";
        private const string iOSIconStage = "ion-ios-calendar";
        private const string iOSIconMap = "ion-ios-location";

        // Use only Android MasterDetail Page
        public ICommand NavigationHomeCommand { get; }
        public ICommand NavigationPlanningRootCommand { get; }
        public ICommand NavigationStageEventRootCommand { get; }
        public ICommand NavigationMapRootCommand { get; }

        // Use only iOS TabbedPage
        public ReactiveProperty<string> ReactiveiOSIconHome { get; } = new ReactiveProperty<string>($"{iOSIconHome}{iOSIconOutline}");
        public ReactiveProperty<string> ReactiveiOSIconList { get; }= new ReactiveProperty<string>($"{iOSIconList}{iOSIconOutline}");
        public ReactiveProperty<string> ReactiveiOSIconStage { get; }= new ReactiveProperty<string>($"{iOSIconStage}{iOSIconOutline}");
        public ReactiveProperty<string> ReactiveiOSIconMap { get; }= new ReactiveProperty<string>($"{iOSIconMap}{iOSIconOutline}");
        public void SelectedPageChanged(Views.Controls.TabInfoEventArgs args)
        {
            // Iconのソースを変えてもだめみたい
            /*
            ReactiveiOSIconHome.Value = $@"{iOSIconHome}{(args.PageIndex != 0 ? iOSIconOutline : "")}";
            ReactiveiOSIconList.Value = $@"{iOSIconList}{(args.PageIndex != 1 ? iOSIconOutline : "")}";
            ReactiveiOSIconStage.Value = $@"{iOSIconStage}{(args.PageIndex != 2 ? iOSIconOutline : "")}";
            ReactiveiOSIconMap.Value = $@"{iOSIconMap}{(args.PageIndex != 3 ? iOSIconOutline : "")}";
            */
        }

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
        }

        private async Task PlatformDependPageNavigation(string pageName) => _navigationService.NavigateAsync($@"{(Device.RuntimePlatform == Device.Android ? "NavigationPage/" : "")}{pageName}");
    }
}
