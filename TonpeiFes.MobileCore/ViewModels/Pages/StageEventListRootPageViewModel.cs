using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using Reactive.Bindings;
using TonpeiFes.Core.Models.DataObjects;
using TonpeiFes.MobileCore.Extensions;
using TonpeiFes.MobileCore.Helpers;
using TonpeiFes.MobileCore.Usecases;

namespace TonpeiFes.MobileCore.ViewModels.Pages
{
    public class StageEventListRootPageViewModel : ViewModelBase
    {
        private readonly static string ParameterPlaceId = "ParameterPlaceId";
        private readonly static string ParameterPlaceName = "ParameterPlaceName";

        public ReactiveProperty<string> Title { get; } = new ReactiveProperty<string>("ステージイベント");
        public string Day1Segment => "11/3";
        public string Day2Segment => "11/4";
        public string Day3Segment => "11/5";
        private string PlaceId = null;

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
                _eventUsecase.UpdateFilterConditions(SelectedSegment.Value, FavStateObservable.Value, PlaceId);
            });

            FavButtonClickCommand = new DelegateCommand(() =>
            {
            FavStateObservable.Value = !FavStateObservable.Value;
            _eventUsecase.UpdateFilterConditions(SelectedSegment.Value, FavStateObservable.Value, PlaceId);
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

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if (parameters == null || !parameters.ContainsKey(ParameterPlaceId)) return;

            Title.Value = $"{parameters[ParameterPlaceName]}のイベント";
            PlaceId = parameters[ParameterPlaceId] as string;

            _eventUsecase.UpdateFilterConditions(SelectedSegment.Value, FavStateObservable.Value, PlaceId);
        }

        public static NavigationParameters GetNavigationParameter(string id, string name)
        {
            return new NavigationParameters
            {
                { ParameterPlaceId, id },
                { ParameterPlaceName, name }
            };
        }
    }
}
