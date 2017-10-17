using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TonpeiFes.MobileCore.Helpers;
using TonpeiFes.MobileCore.Models.DataObjects;

namespace TonpeiFes.MobileCore.Usecases
{
    public interface IFilterGroupingStageEvent
    {
        ReadOnlyObservableCollection<ObservableGroupCollection<string, ISearchableListPlanning>> Plannings { get; }
        Task UpdateFilterConditions(int activeSegment, bool favorited);
    }
}
