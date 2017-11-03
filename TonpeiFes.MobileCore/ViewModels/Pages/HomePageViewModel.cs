using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Reactive.Bindings;
using TonpeiFes.Core.Models.DataObjects;
using TonpeiFes.MobileCore.Usecases;
using Prism.Navigation;
using TonpeiFes.MobileCore.Extensions;
using TonpeiFes.MobileCore.Services;
using System.Windows.Input;
using Reactive.Bindings.Extensions;
using TonpeiFes.MobileCore.Models.EventArgs;
using Prism.Events;

namespace TonpeiFes.MobileCore.ViewModels.Pages
{
    public class HomePageViewModel : ViewModelBase
    {
        public string Title => "東北大学祭 2017";

        public ReadOnlyReactiveCollection<Announcement> Announcements { get; }
        public ICommand SelectedItemCommand { get; }
        public ICommand OpenInfoPageCommand { get; }

        private IShowAnnouncement _showAnnouce;
        private INavigationService _navigationService;
        private IOpenWebPageService _webService;

        public HomePageViewModel(INavigationService navigationService, IOpenWebPageService webService, IShowAnnouncement showAnnounce, IEventAggregator _eventAggregator)
        {
            _showAnnouce = showAnnounce;
            _navigationService = navigationService;
            _webService = webService;

            Announcements = _showAnnouce.Announcements.ToReadOnlyReactiveCollection().AddTo(this.Disposable);

            SelectedItemCommand = new DelegateCommand<Announcement>(async (item) =>
            {
                if (item.HasContents)
                {
                    await _navigationService.NavigateAsync(
                    $"NavigationPage/{nameof(ClosableInternalWebViewPageViewModel).GetViewNameFromRule()}",
                        ClosableInternalWebViewPageViewModel.GetNavigationParameter(item.Id), true);
                }
                else if (item.IsOutWebPage)
                {
                    await _webService.OpenUri(item.Uri);
                }
            });

            OpenInfoPageCommand = new DelegateCommand(async () =>
            {
                await _navigationService.NavigateAsync("NavigationPage/OtherInformationsPage", null, true);
            });

            _eventAggregator.GetEvent<TabbedPageOpendEvent>().Subscribe((ev) =>
            {
                if (ev.Name != this.GetType().Name.Replace("ViewModel", "")) return;

                _showAnnouce.InitializeAnnouncements();
            }).AddTo(this.Disposable);
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            // iOSでEventAggregatorとこっちも呼ばれるけどめんどくさいので放置
            _showAnnouce.InitializeAnnouncements();
        }
    }
}
