using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using TonpeiFes.Core.Models.Consts;
using TonpeiFes.Core.Models.DataObjects;
using TonpeiFes.MobileCore.Configurations;
using TonpeiFes.MobileCore.Extensions;
using TonpeiFes.MobileCore.Models.EventArgs;
using TonpeiFes.MobileCore.Usecases;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Bindings;

namespace TonpeiFes.MobileCore.ViewModels.Pages
{
    public class FestaMapRootPageViewModel : ViewModelBase
    {
        private static readonly string ParameterID = "ParameterID";
        private static readonly string ParameterPlanningType = "ParameterPlanningType";

        public ReactiveProperty<string> Title { get; } = new ReactiveProperty<string>("会場全体図");
        public ObservableCollection<Pin> Pins { get; set; }
        public ObservableCollection<Polygon> Polygons { get; set; }

        public ICommand InfoWindowClickedCommand { get; }
        public ReactiveProperty<Pin> SelectedPin { get; } = new ReactiveProperty<Pin>();
        public ReactiveProperty<bool> IsShowingUser { get; } = new ReactiveProperty<bool>(false);

        private IEventAggregator _eventAggregator;
        private INavigationService _navigationService;
        private IShowFestaMap showFestaUsecase;
        private IMapAssociated _mapParams;

        public bool IsGlobalMap { get; private set; } = true;
        private bool IsInitialized = false;

        public MoveToRegionRequest MoveToRegionRequest { get; } = new MoveToRegionRequest();

        public FestaMapRootPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IShowFestaMap showFesta, IMapAssociated mapParam)
        {
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;
            showFestaUsecase = showFesta;
            _mapParams = mapParam;

            InfoWindowClickedCommand = new DelegateCommand<InfoWindowClickedEventArgs>(async (pin) =>
            {
                var region = pin.Pin.Tag as MapRegion;
                if (region == null) return;
                if (IsGlobalMap)
                {
                    if (region.MapObjectType.HasFlag(MapObjectEnum.STAGE))
                    {
                       await _navigationService.NavigateAsync(
                            nameof(StageEventListRootPageViewModel).GetViewNameFromRule(),
                            StageEventListRootPageViewModel.GetNavigationParameter(region.Id, region.Name));
                    }
                    else
                    {
                        var type = region.MapObjectType.HasFlag(MapObjectEnum.EXHIBITION) ? PlanningTypeEnum.EXHIBITION : PlanningTypeEnum.STALL;
                        await _navigationService.NavigateAsync(
                            "RegionSpecificPlanningListPage",
                            PlanningListRootPageViewModel.GetNavigationParameter(region.Id, region.Name, type));
                    }
                }
            });

            _eventAggregator.GetEvent<PolygonClickedEvent>().Subscribe((pin) =>
            {
                SelectedPin.Value = pin.Tag as Pin;
                MoveToRegionRequest.MoveToRegion(
                        MapSpan.FromCenterAndRadius(SelectedPin.Value.Position, Distance.FromMeters(100)));
            }).AddTo(this.Disposable);

            showFesta.Pins.ToCollectionChanged<Pin>()
                     .Subscribe(change =>
            {
                switch (change.Action)
                {
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                        Pins?.Add(change.Value);
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                        Pins?.Remove(change.Value);
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                        Pins?.Clear();
                        break;
                }
            }).AddTo(this.Disposable);

            showFesta.Polygons.ToCollectionChanged<Polygon>()
                     .Subscribe(change =>
            {
                switch (change.Action)
                {
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                        Polygons?.Add(change.Value);
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                        Polygons?.Remove(change.Value);
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                        Polygons?.Clear();
                        break;
                }
            }).AddTo(this.Disposable);

            // For iOS
            _eventAggregator.GetEvent<TabbedPageOpendEvent>().Subscribe((ev) =>
            {
                if (ev.Name != this.GetType().Name.Replace("ViewModel", "")) return;

                IsShowingUser.Value = true;
                if (IsGlobalMap)
                {
                    MoveToRegionRequest.MoveToRegion(
                        MapSpan.FromCenterAndRadius(
                            new Position(_mapParams.MapCenterLangitude, _mapParams.MapCenterLongitude),
                            Distance.FromMeters(100)));

                    if (!IsInitialized)
                    {
                        showFestaUsecase.InitializeAllMapObjects();
                        IsInitialized = true;
                    }
                }
            }).AddTo(this.Disposable);

            _eventAggregator.GetEvent<LocationPermissionRequestResultEvent>()
                            .Subscribe((ev) =>
            {
                IsShowingUser.Value = ev.Granted;
            });

            Title.AddTo(this.Disposable);
            SelectedPin.AddTo(this.Disposable);
            IsShowingUser.AddTo(this.Disposable);
        }

        // iOS: called with parameter only
        // Android: always called
        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            // For Android
            if (parameters == null || !parameters.ContainsKey(ParameterID))
            {
                MoveToRegionRequest.MoveToRegion(
                        MapSpan.FromCenterAndRadius(
                            new Position(_mapParams.MapCenterLangitude, _mapParams.MapCenterLongitude),
                            Distance.FromMeters(100)));
                showFestaUsecase.InitializeAllMapObjects();
                _eventAggregator.GetEvent<LocationPermissionRequestEvent>().Publish(new LocationPermissionRequestEventArgs());
            }
            else
            {
                IsGlobalMap = false;
                _eventAggregator.GetEvent<SwitchToClosablePageEvent>().Publish(new SwitchToClosablePageEventArgs(nameof(FestaMapRootPageViewModel).GetViewNameFromRule()));
                var planning = showFestaUsecase.GetSingleMapObject(parameters[ParameterID] as string,
                                                                   (PlanningTypeEnum)parameters[ParameterPlanningType]);
                if (planning == null) return;
                Title.Value = $"{planning.Title}の場所";
                if (Pins.Count == 0) return;
                SelectedPin.Value = Pins[0];
                MoveToRegionRequest.MoveToRegion(
                        MapSpan.FromCenterAndRadius(Pins[0].Position, Distance.FromMeters(100)));
            }
        }

        public static NavigationParameters GetNavigationParameter(string id, PlanningTypeEnum planType)
        {
            return new NavigationParameters
            {
                { ParameterID, id },
                { ParameterPlanningType, planType},
            };
        }
    }
}
