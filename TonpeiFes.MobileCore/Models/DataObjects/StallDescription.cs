using System;
namespace TonpeiFes.MobileCore.Models.DataObjects
{
    public class StallDescription : Realms.RealmObject, IDescription
    {
        public string Title { get; set; }

        public string Detail { get; set; }
    }
}
