using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TonpeiFes.Core.Models.DataObjects;
using TonpeiFes.MobileCore.Helpers;

namespace TonpeiFes.MobileCore.Usecases
{
    public interface IFilterGroupingStageEvent
    {
        ReadOnlyObservableCollection<ObservableGroupCollection<string, ISearchableListPlanning>> Plannings { get; }
        Task UpdateFilterConditions(int activeSegment, bool favorited, string placeId);
    }
}
