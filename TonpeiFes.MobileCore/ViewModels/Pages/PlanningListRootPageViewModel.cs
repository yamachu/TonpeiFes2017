using System;
using System.Linq;
using Prism.Commands;
using Reactive.Bindings;
using System.Reactive.Linq;
using System.Windows.Input;
using TonpeiFes.MobileCore.Usecases;
using TonpeiFes.MobileCore.Models.DataObjects;

namespace TonpeiFes.MobileCore.ViewModels.Pages
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

        public ReadOnlyReactiveCollection<ISearchableListPlanning> Plannings { get; }

        private IFilterGroupingPlanning _planningUsecase;

        public PlanningListRootPageViewModel(IFilterGroupingPlanning planningUsecase)
        {
            _planningUsecase = planningUsecase;

            SelectedSegment.Subscribe(selectedSegment => 
            {
                _planningUsecase.UpdateFilterConditions(SearchQuery.Value, SelectedSegment.Value, FavStateObservable.Value);
            });

            SearchQuery.Throttle(TimeSpan.FromMilliseconds(400)).Subscribe(query =>
            {
                System.Diagnostics.Debug.WriteLine($"Query Update: {query}");
                _planningUsecase.UpdateFilterConditions(SearchQuery.Value, SelectedSegment.Value, FavStateObservable.Value);
            });

            FavButtonClickCommand = new DelegateCommand(() =>
            {
                FavStateObservable.Value = !FavStateObservable.Value;
                _planningUsecase.UpdateFilterConditions(SearchQuery.Value, SelectedSegment.Value, FavStateObservable.Value);
            });

            IconSource = FavStateObservable.Select((isFavActive) =>
            {
                return $@"ion-ios-heart{(isFavActive ? "" : "-outline")}";
            }).ToReadOnlyReactiveProperty("ion-ios-heart");

            Plannings = _planningUsecase.Plannings.ToReadOnlyReactiveCollection();
        }
    }
}
