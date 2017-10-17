﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TonpeiFes.MobileCore.Models.DataObjects;

namespace TonpeiFes.MobileCore.Usecases
{
    public interface IFilterGroupingPlanning
    {
        ReadOnlyObservableCollection<ISearchableListPlanning> Plannings { get; }
        Task UpdateFilterConditions(string query, int activeSegment, bool favorited);
    }
}
