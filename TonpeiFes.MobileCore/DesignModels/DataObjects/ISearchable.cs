﻿using System;
using System.Collections.Generic;

namespace TonpeiFes.MobileCore.DesignModels.DataObjects
{
    public interface ISearchable
    {
        List<string> Keywords { get; }
        string SearchableKeywords { get; }
        void UpdateSearchableKeywords();
    }
}
