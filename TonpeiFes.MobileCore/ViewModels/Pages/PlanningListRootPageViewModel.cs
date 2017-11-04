using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using TonpeiFes.Core.Models.Consts;
using TonpeiFes.Core.Models.DataObjects;
using TonpeiFes.MobileCore.Extensions;
using TonpeiFes.MobileCore.Helpers;
using TonpeiFes.MobileCore.Models.EventArgs;
using TonpeiFes.MobileCore.Usecases;

namespace TonpeiFes.MobileCore.ViewModels.Pages
{
    public class PlanningListRootPageViewModel : ViewModelBase
    {
        private readonly static string ParameterPlaceId = "ParameterPlaceId";
        private readonly static string ParameterPlaceName = "ParameterPlaceName";
        private readonly static string ParameterType = "ParameterType";

        public ReactiveProperty<string> Title { get; } = new ReactiveProperty<string>("模擬店/企画");
        public string OutdoorSegment => "屋外";
        public string IndoorSegment => "屋内";
        private string PlaceId = null;
        private ReactiveProperty<PlanningTypeEnum> PlanningType;

        public ReactiveProperty<int> SelectedSegment { get; } = new ReactiveProperty<int>(0);
        public ReactiveProperty<string> SearchQuery { get; } = new ReactiveProperty<string>();

        public ReadOnlyReactiveProperty<string> IconSource { get; }
        public ICommand FavButtonClickCommand { get; }
        private ReactiveProperty<bool> FavStateObservable = new ReactiveProperty<bool>(false);

        public ReadOnlyReactiveCollection<ObservableGroupCollection<MyGroupHeader, ISearchableListPlanning>> Plannings { get; }

        public AsyncReactiveCommand<IPlanning> SelectedItemCommand { get; }
        public AsyncReactiveCommand<string> OpenPlceDetailCommand { get; }

        private IFilterGroupingPlanning _planningUsecase;
        private bool IsDirty = false;

        public PlanningListRootPageViewModel(INavigationService navigationService, IFilterGroupingPlanning planningUsecase, IEventAggregator eventAggregator)
        {
            _planningUsecase = planningUsecase;

            Title.AddTo(this.Disposable);
            SelectedSegment.AddTo(this.Disposable);
            SearchQuery.AddTo(this.Disposable);
            FavStateObservable.AddTo(this.Disposable);

            PlanningType = SelectedSegment.Select((index) => index.SegmentedControlIndexToPlanningTypeEnum())
                                          .ToReactiveProperty()
                                          .AddTo(this.Disposable);

            SelectedSegment.Subscribe(selectedSegment => 
            {
                if (IsDirty)
                    _planningUsecase.UpdateFilterConditions(SearchQuery.Value, PlanningType.Value, FavStateObservable.Value, PlaceId);
            }).AddTo(this.Disposable);

            SearchQuery.Throttle(TimeSpan.FromMilliseconds(400)).ObserveOnUIDispatcher().Subscribe(query =>
            {
                if (IsDirty)
                    _planningUsecase.UpdateFilterConditions(SearchQuery.Value, PlanningType.Value, FavStateObservable.Value, PlaceId);
            }).AddTo(this.Disposable);

            FavButtonClickCommand = new DelegateCommand(() =>
            {
                FavStateObservable.Value = !FavStateObservable.Value;
                _planningUsecase.UpdateFilterConditions(SearchQuery.Value, PlanningType.Value, FavStateObservable.Value, PlaceId);
            });

            IconSource = FavStateObservable.Select((isFavActive) =>
            {
                return $@"ion_ios_heart{(isFavActive ? "" : "_outline")}";
            }).ToReadOnlyReactiveProperty("ion_ios_heart").AddTo(this.Disposable);

            Plannings = _planningUsecase.Plannings.ToReadOnlyReactiveCollection().AddTo(this.Disposable);

            SelectedItemCommand = new AsyncReactiveCommand<IPlanning>();
            SelectedItemCommand.Subscribe(async (item) =>
            {
                await navigationService.NavigateAsync(
                    nameof(PlanningDetailPageViewModel).GetViewNameFromRule(),
                    PlanningDetailPageViewModel.GetNavigationParameter(item.Id, item.PlanningType));
            }).AddTo(this.Disposable);

            OpenPlceDetailCommand = new AsyncReactiveCommand<string>();
            OpenPlceDetailCommand.Subscribe(async (placeName) =>
            {
                await navigationService.NavigateAsync("NavigationPage/DetailFloorPage", DetailFloorPageViewModel.GetNavigationParameter(placeName), true);
            }).AddTo(this.Disposable);

            eventAggregator.GetEvent<TabbedPageOpendEvent>().Subscribe((ev) =>
            {
                if (ev.Name != this.GetType().Name.Replace("ViewModel", "")) return;
                if (!IsDirty)
                {
                    IsDirty = true;
                    _planningUsecase.UpdateFilterConditions(SearchQuery.Value, PlanningType.Value, FavStateObservable.Value, PlaceId);
                }
            }).AddTo(this.Disposable);
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if (parameters == null || !parameters.ContainsKey(ParameterPlaceId) && !IsDirty)
            {
                IsDirty = true;
                _planningUsecase.UpdateFilterConditions(SearchQuery.Value, PlanningType.Value, FavStateObservable.Value, PlaceId);
                return;
            }

            if (IsDirty) return;

            Title.Value = $@"{parameters[ParameterPlaceName]}の{((PlanningTypeEnum)parameters[ParameterType] == PlanningTypeEnum.EXHIBITION ? "屋内" : "屋外")}企画";
            PlaceId = parameters[ParameterPlaceId] as string;
            PlanningType.Value = (PlanningTypeEnum)parameters[ParameterType];

            _planningUsecase.UpdateFilterConditions(SearchQuery.Value, PlanningType.Value, FavStateObservable.Value, PlaceId);
        }

        public static NavigationParameters GetNavigationParameter(string id, string name, PlanningTypeEnum type)
        {
            return new NavigationParameters
            {
                { ParameterPlaceId, id },
                { ParameterPlaceName, name },
                { ParameterType, type }
            };
        }
    }

    internal static class SegmentedControlIndexExtensions
    {
        public static PlanningTypeEnum SegmentedControlIndexToPlanningTypeEnum(this int index)
        {
            return index == 0 ? PlanningTypeEnum.STALL : PlanningTypeEnum.EXHIBITION;
        }
    }
}
