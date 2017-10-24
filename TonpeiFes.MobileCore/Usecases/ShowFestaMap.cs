using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Events;
using Reactive.Bindings;
using TonpeiFes.Core.Models.DataObjects;
using TonpeiFes.MobileCore.Models.EventArgs;
using TonpeiFes.MobileCore.Repositories;
using Xamarin.Forms.GoogleMaps;

namespace TonpeiFes.MobileCore.Usecases
{
    public class ShowFestaMap : IShowFestaMap
    {
        private readonly ObservableCollection<Pin> _pins = new ObservableCollection<Pin>();
        public ReadOnlyObservableCollection<Pin> Pins { get; }

        private readonly ObservableCollection<Polygon> _polygons = new ObservableCollection<Polygon>();
        public ReadOnlyObservableCollection<Polygon> Polygons { get; }

        private ReactiveProperty<Pin> _specifiedPin = new ReactiveProperty<Pin>();
        public ReadOnlyReactiveProperty<Pin> SpecifiedPin { get; }

        private ReactiveProperty<Polygon> _specifiedPolygon = new ReactiveProperty<Polygon>();
        public ReadOnlyReactiveProperty<Polygon> SpecifiedPolygon { get; }

        private IRepository<MapRegion> _mapRepository;
        private IEventAggregator _eventAggregator;

        public ShowFestaMap(IRepository<MapRegion> mapRep, IEventAggregator eventAggregator)
        {
            _mapRepository = mapRep;
            _eventAggregator = eventAggregator;

            Pins = new ReadOnlyObservableCollection<Pin>(_pins);
            Polygons = new ReadOnlyObservableCollection<Polygon>(_polygons);
            SpecifiedPin = _specifiedPin.ToReadOnlyReactiveProperty();
            SpecifiedPolygon = _specifiedPolygon.ToReadOnlyReactiveProperty();
        }

        public Task GetSingleMapObject(string id)
        {
            throw new NotImplementedException();
        }

        public async Task InitializeAllMapObjects()
        {
            var plannedRegion = _mapRepository.GetAll().Where(item => item.ContainsPlannings);

            foreach (var region in plannedRegion)
            {
                var pinPoint = region.PinPoint;
                var parentPin = new Pin
                {
                    Type = PinType.Generic,
                    Label = region.Name,
                    Address = "タップして展示一覧を確認",
                    Position = new Position(pinPoint.Langitude, pinPoint.Longitude),
                    Tag = region
                };

                _pins.Add(parentPin);

                var parentPolygon = GetPolygon(region);
                SetAssociationWithPin(parentPolygon, parentPin);
                _polygons.Add(parentPolygon);

                foreach (var childRegion in region.ChildMapRegion)
                {
                    var childPolygon = GetPolygon(childRegion);
                    SetAssociationWithPin(childPolygon, parentPin);
                    _polygons.Add(childPolygon);
                }
            }
        }

        private Polygon GetPolygon(MapRegion region)
        {
            var polygon = new Polygon();
            foreach (var point in region.Points)
            {
                polygon.Positions.Add(new Position(point.Langitude, point.Longitude));
            }

            return polygon;
        }
        private void SetAssociationWithPin(Polygon polygon, Pin pin)
        {
            polygon.Tag = pin;
            polygon.IsClickable = true;
            polygon.Clicked += (sender, e) =>
            {
                _eventAggregator.GetEvent<PolygonClickedEvent>()
                                .Publish(new PolygonClickedEventArgs((sender as Polygon).Tag));
            };
        }
    }
}
