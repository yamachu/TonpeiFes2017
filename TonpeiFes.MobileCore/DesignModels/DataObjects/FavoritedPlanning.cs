using System;
using TonpeiFes.MobileCore.DesignModels.Consts;

namespace TonpeiFes.MobileCore.DesignModels.DataObjects
{
    public class FavoritedPlanning : IFavoritedPlanning
    {
        public string Id { get; set; }

        // ToDo: Validate PlannningTypeNumber 1~3 or not
        public int PlanningTypeNumber { get; set; }

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
