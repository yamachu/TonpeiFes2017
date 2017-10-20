﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TonpeiFes.Core.Models.DataObjects;
using TonpeiFes.MobileCore.Helpers;

namespace TonpeiFes.MobileCore.Usecases
{
    public interface IFilterGroupingPlanning
    {
        ReadOnlyObservableCollection<ObservableGroupCollection<string, ISearchableListPlanning>> Plannings { get; }
        Task UpdateFilterConditions(string query, int activeSegment, bool favorited);
    }
}
