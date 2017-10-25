using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Events;
using Reactive.Bindings;
using TonpeiFes.Core.Models.Consts;
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

        private IRepository<MapRegion> _mapRepository;
        private IRepository<Exhibition> _exhiitionRepository;
        private IRepository<StageEvent> _stageRepository;
        private IRepository<Stall> _stallRepository;
        private IEventAggregator _eventAggregator;

        public ShowFestaMap(IRepository<MapRegion> mapRep,
                            IRepository<Exhibition> exhibitionRep,
                            IRepository<StageEvent> stageEventRep,
                            IRepository<Stall> stallRep,
                            IEventAggregator eventAggregator)
        {
            _mapRepository = mapRep;
            _exhiitionRepository = exhibitionRep;
            _stageRepository = stageEventRep;
            _stallRepository = stallRep;
            _eventAggregator = eventAggregator;

            Pins = new ReadOnlyObservableCollection<Pin>(_pins);
            Polygons = new ReadOnlyObservableCollection<Polygon>(_polygons);
        }

        public IPlanning GetSingleMapObject(string id, PlanningTypeEnum type)
        {
            IPlanning tmp = null;
            switch (type)
            {
                case PlanningTypeEnum.EXHIBITION:
                    tmp = (dynamic)_exhiitionRepository.GetOne(id);
                    break;
                case PlanningTypeEnum.STAGE:
                    tmp = (dynamic)_stageRepository.GetOne(id);
                    break;
                case PlanningTypeEnum.STALL:
                    tmp = (dynamic)_stallRepository.GetOne(id);
                    break;
            }

            var region = _mapRepository.GetOne(tmp.MappedRegion.Id);
            var pinPoint = region.PinPoint;
            var pin = new Pin
            {
                Type = PinType.Generic,
                Label = tmp.Title,
                Address = tmp.LocationDetail,
                Position = new Position(pinPoint.Langitude, pinPoint.Longitude)
            };

            _pins.Add(pin);
            var polygon = GetPolygon(region);
            if (polygon != null)
            {
                _polygons.Add(polygon);
            }

            foreach (var childRegion in region.ChildMapRegion)
            {
                var childPolygon = GetPolygon(childRegion);
                if (childPolygon != null)
                {
                    _polygons.Add(childPolygon);
                }
            }

            return tmp;
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
                if (parentPolygon != null)
                {
                    SetAssociationWithPin(parentPolygon, parentPin);
                    _polygons.Add(parentPolygon);
                }

                foreach (var childRegion in region.ChildMapRegion)
                {
                    var childPolygon = GetPolygon(childRegion);
                    if (childPolygon != null)
                    {
                        SetAssociationWithPin(childPolygon, parentPin);
                        _polygons.Add(childPolygon);
                    }
                }
            }
        }

        private Polygon GetPolygon(MapRegion region)
        {
            var polygon = new Polygon();
            if (region.Points.Count < 3) return null;
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
