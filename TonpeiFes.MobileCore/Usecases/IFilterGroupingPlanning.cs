using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TonpeiFes.Core.Models.Consts;
using TonpeiFes.Core.Models.DataObjects;
using TonpeiFes.MobileCore.Helpers;

namespace TonpeiFes.MobileCore.Usecases
{
    public interface IFilterGroupingPlanning
    {
        ReadOnlyObservableCollection<ObservableGroupCollection<MyGroupHeader, ISearchableListPlanning>> Plannings { get; }
        Task UpdateFilterConditions(string query, PlanningTypeEnum type, bool favorited, string placeId);
    }
}
