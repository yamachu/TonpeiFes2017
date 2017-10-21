using Microsoft.Practices.Unity;
using Prism.Events;
using TonpeiFes.MobileCore.Models.EventArgs;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace TonpeiFes.Forms.Views.Pages
{
    public partial class FestaMapRootPage : ContentPage
    {
        private IEventAggregator _eventAggregator;

        public FestaMapRootPage()
        {
            InitializeComponent();

            // つらい
            var container = (Application.Current as App).Container;

            _eventAggregator = container.Resolve<IEventAggregator>();

            _eventAggregator.GetEvent<MapMoveEvent>().Subscribe((args) =>
            {
                map.MoveToRegion(
                    MapSpan.FromCenterAndRadius(new Position(
                        args.IsInitialPosition ? Configurations.ConstParameters.MAP_CENTER_X : args.Longitude,
                        args.IsInitialPosition ? Configurations.ConstParameters.MAP_CENTER_Y : args.Latitude),
                    Distance.FromMeters(100)), false);
            });
        }
    }
}

