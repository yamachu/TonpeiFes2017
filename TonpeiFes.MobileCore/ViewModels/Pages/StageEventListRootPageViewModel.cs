using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using Reactive.Bindings;
using TonpeiFes.MobileCore.Extensions;
using TonpeiFes.MobileCore.Helpers;
using TonpeiFes.MobileCore.Models.DataObjects;
using TonpeiFes.MobileCore.Usecases;

namespace TonpeiFes.MobileCore.ViewModels.Pages
{
    public class StageEventListRootPageViewModel : ViewModelBase
    {
        public string Title => "ステージイベント";
        public string Day1Segment => "11/3";
        public string Day2Segment => "11/4";
        public string Day3Segment => "11/5";

        public ReactiveProperty<int> SelectedSegment { get; } = new ReactiveProperty<int>(0);
        public ReadOnlyReactiveProperty<string> IconSource { get; }
        public ICommand FavButtonClickCommand { get; }
        public ReadOnlyReactiveCollection<ObservableGroupCollection<string, ISearchableListPlanning>> Plannings { get; }

        public AsyncReactiveCommand<IPlanning> SelectedItemCommand { get; }

        private ReactiveProperty<bool> FavStateObservable = new ReactiveProperty<bool>(false);
        private IFilterGroupingStageEvent _eventUsecase;

        public StageEventListRootPageViewModel(INavigationService navigationService, IFilterGroupingStageEvent eventUsecase)
        {
            _eventUsecase = eventUsecase;

            SelectedSegment.Subscribe(selectedSegment =>
            {
                _eventUsecase.UpdateFilterConditions(SelectedSegment.Value, FavStateObservable.Value);
            });

            FavButtonClickCommand = new DelegateCommand(() =>
            {
                FavStateObservable.Value = !FavStateObservable.Value;
                _eventUsecase.UpdateFilterConditions(SelectedSegment.Value, FavStateObservable.Value);
            });

            IconSource = FavStateObservable.Select((isFavActive) =>
            {
                return $@"ion_ios_heart{(isFavActive ? "" : "_outline")}";
            }).ToReadOnlyReactiveProperty("ion_ios_heart");

            Plannings = _eventUsecase.Plannings.ToReadOnlyReactiveCollection();

            SelectedItemCommand = new AsyncReactiveCommand<IPlanning>();
            SelectedItemCommand.Subscribe(async (item) =>
            {
                await navigationService.NavigateAsync(
                    nameof(PlanningDetailPageViewModel).GetViewNameFromRule(),
                    PlanningDetailPageViewModel.GetNavigationParameter(item.Id, item.PlanningType));
            });
        }
    }
}
