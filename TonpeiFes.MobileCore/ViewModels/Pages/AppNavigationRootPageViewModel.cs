using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prism.Events;
using Prism.Navigation;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using TonpeiFes.MobileCore.Extensions;
using TonpeiFes.MobileCore.Models.EventArgs;

namespace TonpeiFes.MobileCore.ViewModels.Pages
{
    public class AppNavigationRootPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;

        // Use only Android MasterDetail Page
        public List<MasterPageListItem> MasterPageItems { get; }
        public AsyncReactiveCommand<MasterPageListItem> SelectedItemCommand { get; }

        // Use only iOS TabbedPage
        public string iOSIconHome => "ion-ios-home";
        public string iOSIconList => "ion-ios-list";
        public string iOSIconStage => "ion-ios-calendar";
        public string iOSIconMap => "ion-ios-location";

        public ReactiveProperty<int> ReactiveCurrentTabIndex { get; } = new ReactiveProperty<int>();

        public AppNavigationRootPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;

            MasterPageItems = new List<MasterPageListItem>{
                new MasterPageListItem{ Title = "ホーム", Icon = iOSIconHome, PageName = nameof(Pages.HomePageViewModel).GetViewNameFromRule() },
                new MasterPageListItem{ Title = "模擬店／展示", Icon = iOSIconList, PageName = nameof(Pages.PlanningListRootPageViewModel).GetViewNameFromRule() },
                new MasterPageListItem{ Title = "ステージイベント", Icon = iOSIconStage, PageName = nameof(Pages.StageEventListRootPageViewModel).GetViewNameFromRule() },
                new MasterPageListItem{ Title = "マップ", Icon = iOSIconMap, PageName = nameof(Pages.FestaMapRootPageViewModel).GetViewNameFromRule() },
            };

            SelectedItemCommand = new AsyncReactiveCommand<MasterPageListItem>();
            SelectedItemCommand.Subscribe(async (item) =>
            {
                await NavigationPageWrappedNavigation(item.PageName);
            }).AddTo(this.Disposable);

            ReactiveCurrentTabIndex.Subscribe((value) =>
            {
                // Mapが選択された時にデフォルトの場所に移動
                if (value == 3)
                {
                    _eventAggregator.GetEvent<TabbedPageOpendEvent>().Publish(new TabbedPageOpendEventArgs(nameof(Pages.FestaMapRootPageViewModel).GetViewNameFromRule()));
                }
            }).AddTo(this.Disposable);

            ReactiveCurrentTabIndex.AddTo(this.Disposable);
        }

        private Task NavigationPageWrappedNavigation(string pageName)
        {
            return _navigationService.NavigateAsync($@"NavigationPage/{pageName}");
        }
    }

    public class MasterPageListItem
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public string PageName { get; set; }
    }
}
