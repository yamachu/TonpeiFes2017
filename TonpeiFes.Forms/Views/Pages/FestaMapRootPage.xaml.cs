using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Microsoft.Practices.Unity;
using Prism.Events;
using Reactive.Bindings;
using TonpeiFes.MobileCore.Models.EventArgs;
using TonpeiFes.MobileCore.ViewModels.Pages;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace TonpeiFes.Forms.Views.Pages
{
    public partial class FestaMapRootPage : ContentPage
    {
        private IEventAggregator _eventAggregator;

        private IDisposable _allDispose;

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

            var pinDisposable = (BindingContext as FestaMapRootPageViewModel)
                .Pins.ToCollectionChanged<Pin>()
                .Subscribe(pinChange =>
            {
                if (pinChange.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    map.Pins.Add(pinChange.Value);
                }
            });

            var polygonDisposable = (BindingContext as FestaMapRootPageViewModel)
                .Polygons.ToCollectionChanged<Polygon>()
                .Subscribe(polygonChange =>
            {
                if (polygonChange.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    map.Polygons.Add(polygonChange.Value);
                }
            });

            _allDispose = new CompositeDisposable(pinDisposable, polygonDisposable);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            _allDispose.Dispose();
        }
    }
}
