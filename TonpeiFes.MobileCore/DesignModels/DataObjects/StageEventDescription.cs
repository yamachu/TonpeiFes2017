using System;
namespace TonpeiFes.MobileCore.DesignModels.DataObjects
{
    public class StageEventDescription : Realms.RealmObject, IDescription
    {
        public string Title { get; set; }

        public string Detail { get; set; }
    }
}
