using System;
using Realms;
using TonpeiFes.Core.Models.Consts;

namespace TonpeiFes.Core.Models.DataObjects
{
    public class FavoritedPlanning : Realms.RealmObject, IFavoritedPlanning
    {
        [PrimaryKey]
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
