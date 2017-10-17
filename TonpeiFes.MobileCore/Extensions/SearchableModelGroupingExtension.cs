using System;
using System.Collections.Generic;
using System.Linq;
using TonpeiFes.MobileCore.Helpers;
using TonpeiFes.MobileCore.Models.DataObjects;

namespace TonpeiFes.MobileCore.Extensions
{
    public static class SearchableModelGroupingExtension
    {
        public static IEnumerable<ObservableGroupCollection<string, ISearchableListPlanning>> GroupingPlannings(this IEnumerable<ISearchableListPlanning> list)
        {
            return list.GroupBy(item => item.GroupHeader)
                       .Select(g => new ObservableGroupCollection<string, ISearchableListPlanning>(g))
                       .OrderBy(g => g.Key);
        }
    }
}
