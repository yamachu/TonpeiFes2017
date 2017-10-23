using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using Reactive.Bindings;

namespace TonpeiFes.MobileCore.ViewModels.Pages
{
    public class DetailFloorPageViewModel : ViewModelBase
    {
        public ReactiveProperty<string> Title { get; } = new ReactiveProperty<string>();
        public AsyncReactiveCommand CloseButtonClickCommand { get; }
        private INavigationService _navigationService;

        public DetailFloorPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            CloseButtonClickCommand = new AsyncReactiveCommand();
            CloseButtonClickCommand.Subscribe(async () =>
            {
                await _navigationService.GoBackAsync(null, true);
            });
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            // ここで場所のパラメータをもらう
            // Titleの変更 => ~の詳細地図みたいな感じで
        }
    }
}
