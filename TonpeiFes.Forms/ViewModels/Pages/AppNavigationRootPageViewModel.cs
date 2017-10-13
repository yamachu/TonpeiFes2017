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

namespace TonpeiFes.Forms.ViewModels
{
    public class AppNavigationRootPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        // Use only Android MasterDetail Page
        public List<MasterPageListItem> MasterPageItems { get; }
        public AsyncReactiveCommand<MasterPageListItem> SelectedItemCommand { get; }

        // Use only iOS TabbedPage
        public string iOSIconHome => "ion-ios-home";
        public string iOSIconList => "ion-ios-list";
        public string iOSIconStage => "ion-ios-calendar";
        public string iOSIconMap => "ion-ios-location";

        public ReactiveProperty<int> ReactiveCurrentTabIndex { get; } = new ReactiveProperty<int>();

        public AppNavigationRootPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            MasterPageItems = new List<MasterPageListItem>{
                new MasterPageListItem{ Title = "ホーム", Icon = iOSIconHome, PageName = nameof(Views.Pages.HomePage)},
                new MasterPageListItem{ Title = "模擬店／展示", Icon = iOSIconList, PageName = nameof(Views.Pages.PlanningListRootPage)},
                new MasterPageListItem{ Title = "ステージイベント", Icon = iOSIconStage, PageName = nameof(Views.Pages.StageEventListRootPage)},
                new MasterPageListItem{ Title = "マップ", Icon = iOSIconMap, PageName = nameof(Views.Pages.FestaMapRootPage)},
            };

            SelectedItemCommand = new AsyncReactiveCommand<MasterPageListItem>();
            SelectedItemCommand.Subscribe(async (item) =>
            {
                await PlatformDependPageNavigation(item.PageName);
            });

            /*
            ReactiveCurrentTabIndex.Subscribe((value) => {
                // Handle Current Tab Index
            });
            */
        }

        private async Task PlatformDependPageNavigation(string pageName) => _navigationService.NavigateAsync($@"{(Device.RuntimePlatform == Device.Android ? "NavigationPage/" : "")}{pageName}");
    }

    public class MasterPageListItem
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public string PageName { get; set; }
    }
}
