using System;
namespace TonpeiFes.Core.Models.DataObjects
{
    public class ExhibitionDescription : Realms.RealmObject, IDescription
    {
        public string Title { get; set; }

        public string Detail { get; set; }
    }
}
