using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Reactive.Bindings;
using Xamarin.Forms.GoogleMaps;

namespace TonpeiFes.MobileCore.Usecases
{
    public interface IShowFestaMap
    {
        ReadOnlyObservableCollection<Pin> Pins { get; }
        ReadOnlyObservableCollection<Polygon> Polygons { get; }
        ReadOnlyReactiveProperty<Pin> SpecifiedPin { get; }
        ReadOnlyReactiveProperty<Polygon> SpecifiedPolygon { get; }

        Task InitializeAllMapObjects();
        Task GetSingleMapObject(string id);
    }
}
