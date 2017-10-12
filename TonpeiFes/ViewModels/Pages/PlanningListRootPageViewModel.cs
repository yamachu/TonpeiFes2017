using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Reactive.Bindings;
using System.Reactive.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace TonpeiFes.ViewModels.Pages
{
    public class PlanningListRootPageViewModel : ViewModelBase
    {
        // ToDo: 文字列リソースはどこかにまとめる
        public string Title => "模擬店/企画";
        public string OutdoorSegment => "屋外";
        public string IndoorSegment => "屋内";

        public ReactiveProperty<int> SelectedSegment { get; } = new ReactiveProperty<int>(0);
        public ReactiveProperty<string> SearchQuery { get; } = new ReactiveProperty<string>();

        public ReadOnlyReactiveProperty<string> IconSource { get; }
        public ICommand FavButtonClickCommand { get; }
        private ReactiveProperty<bool> FavStateObservable = new ReactiveProperty<bool>(false);
        public bool IsAndroid => Device.RuntimePlatform == Device.Android;

        public PlanningListRootPageViewModel()
        {
            SelectedSegment.Subscribe(selectedSegment => 
            {
                if (selectedSegment == 0) {}
                else {}
            });

            SearchQuery.Throttle(TimeSpan.FromMilliseconds(400)).Subscribe(query =>
            {
                System.Diagnostics.Debug.WriteLine($"Query Update: {query}");
            });

            FavButtonClickCommand = new DelegateCommand(() =>
            {
                FavStateObservable.Value = !FavStateObservable.Value;
            });

            IconSource = FavStateObservable.Select((isFavActive) =>
            {
                return $@"ion-ios-heart{(isFavActive ? "" : "-outline")}";
            }).ToReadOnlyReactiveProperty("ion-ios-heart");
        }
    }
}
