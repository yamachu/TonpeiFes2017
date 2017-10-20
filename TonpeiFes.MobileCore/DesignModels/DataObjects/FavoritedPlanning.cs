using System;
using Realms;
using TonpeiFes.MobileCore.DesignModels.Consts;

namespace TonpeiFes.MobileCore.DesignModels.DataObjects
{
    public class FavoritedPlanning : Realms.RealmObject, IFavoritedPlanning
    {
        public string Id { get; set; }

        private int PlanningTypeNumber { get; set; }

        [Ignored]
        public PlanningTypeEnum PlanningType
        {
            get
            {
                return (PlanningTypeEnum)Enum.ToObject(typeof(PlanningTypeEnum), PlanningTypeNumber);
            }
            set
            {
                PlanningTypeNumber = (int)value;
            }
        }
    }
}
