using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Reactive.Bindings;
using TonpeiFes.MobileCore.Models.EventArgs;
using TonpeiFes.MobileCore.Usecases;
using Xamarin.Forms.GoogleMaps;

namespace TonpeiFes.MobileCore.ViewModels.Pages
{
    public class FestaMapRootPageViewModel : ViewModelBase
    {
        private readonly static string NavigationKey = "NavigationKey";

        public ReactiveProperty<string> Title { get; } = new ReactiveProperty<string>("会場全体図");
        public ReadOnlyObservableCollection<Pin> Pins { get; }
        public ReadOnlyObservableCollection<Polygon> Polygons { get; }

        public ICommand InfoWindowClickedCommand { get; }
        public ReactiveProperty<Pin> SelectedPin { get; } = new ReactiveProperty<Pin>();
        public ReactiveProperty<bool> IsShowingUser { get; } = new ReactiveProperty<bool>(false);

        private IEventAggregator _eventAggregator;
        private INavigationService _navigationService;
        private IShowFestaMap showFestaUsecase;

        private bool IsGlobalMap = true;
        private bool IsInitialized = false;

        public FestaMapRootPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IShowFestaMap showFesta)
        {
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;
            showFestaUsecase = showFesta;

            InfoWindowClickedCommand = new DelegateCommand<Pin>((pin) =>
            {
                if (IsGlobalMap)
                {
                    //_navigationService.NavigateAsync();
                }
            });

            _eventAggregator.GetEvent<PolygonClickedEvent>().Subscribe((pin) =>
            {
                SelectedPin.Value = pin.Tag as Pin;
            });

            Pins = showFesta.Pins.ToReadOnlyReactiveCollection();
            Polygons = showFesta.Polygons.ToReadOnlyReactiveCollection();

            // For iOS
            _eventAggregator.GetEvent<TabbedPageOpendEvent>().Subscribe((ev) =>
            {
                if (ev.Name != this.GetType().Name.Replace("ViewModel", "")) return;

                IsShowingUser.Value = true;
                if (IsGlobalMap)
                {
                    _eventAggregator.GetEvent<MapMoveEvent>().Publish(new MapMoveEventArgs(0, 0, true));
                    if (!IsInitialized)
                    {
                        showFestaUsecase.InitializeAllMapObjects();
                        IsInitialized = true;
                    }
                }
            });

            _eventAggregator.GetEvent<LocationPermissionRequestResultEvent>()
                            .Subscribe((ev) =>
            {
                IsShowingUser.Value = ev.Granted;
            });

        }

        // iOS: called with parameter only
        // Android: always called
        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            // For Android
            if (parameters == null || !parameters.ContainsKey(NavigationKey))
            {
                _eventAggregator.GetEvent<MapMoveEvent>().Publish(new MapMoveEventArgs(0, 0, true));
                showFestaUsecase.InitializeAllMapObjects();
                _eventAggregator.GetEvent<LocationPermissionRequestEvent>().Publish(new LocationPermissionRequestEventArgs());
            }
        }
    }
}
