using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TonpeiFes.MobileCore.Services;
using TonpeiFes.MobileCore.Configurations;
using Reactive.Bindings;
using Prism.Navigation;
using Reactive.Bindings.Extensions;

namespace TonpeiFes.MobileCore.ViewModels.Pages
{
    public class OtherInformationsPageViewModel : ViewModelBase
    {
        public string Title { get; } = "アプリ情報";
        public ObservableCollection<Tuple<string, string>> Items { get; }
        public ICommand SelectedItemCommand { get; }
        public AsyncReactiveCommand CloseButtonClickCommand { get; }

        public OtherInformationsPageViewModel(INavigationService navigationService, IOpenWebPageService webService, IConstUrls constUrls)
        {
            Items = new ObservableCollection<Tuple<string, string>>()
            {
                new Tuple<string, string>("利用規約", constUrls.TermsOfUseUrl),
                new Tuple<string, string>("オープンソースライセンス", constUrls.LicenseUrl),
            };

            SelectedItemCommand = new DelegateCommand<Tuple<string, string>>(async (item) =>
            {
                await webService.OpenUri(item.Item2);
            });

            CloseButtonClickCommand = new AsyncReactiveCommand();
            CloseButtonClickCommand.Subscribe(async () =>
            {
                await navigationService.GoBackAsync(null, true);
            }).AddTo(this.Disposable);
        }
    }
}
