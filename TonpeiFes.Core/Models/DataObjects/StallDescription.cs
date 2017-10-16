using System;
namespace TonpeiFes.Core.Models.DataObjects
{
    public class StallDescription : Realms.RealmObject, IDescription
    {
        public string Title { get; set; }

        public string Detail { get; set; }
    }
}
