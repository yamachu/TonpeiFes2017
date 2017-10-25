using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TonpeiFes.Core.Models.Consts;
using TonpeiFes.Core.Models.DataObjects;
using Xamarin.Forms.GoogleMaps;

namespace TonpeiFes.MobileCore.Usecases
{
    public interface IShowFestaMap
    {
        ReadOnlyObservableCollection<Pin> Pins { get; }
        ReadOnlyObservableCollection<Polygon> Polygons { get; }

        Task InitializeAllMapObjects();
        IPlanning GetSingleMapObject(string id, PlanningTypeEnum type);
    }
}
