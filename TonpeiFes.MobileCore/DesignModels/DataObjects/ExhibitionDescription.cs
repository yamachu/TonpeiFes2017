﻿using System;
namespace TonpeiFes.MobileCore.DesignModels.DataObjects
{
    public class ExhibitionDescription : Realms.RealmObject, IDescription
    {
        public string Title { get; set; }

        public string Detail { get; set; }
    }
}
