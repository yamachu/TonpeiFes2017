using System;
using TonpeiFes.MobileCore.Models.Consts;
namespace TonpeiFes.MobileCore.Models.DataObjects
{
    public interface IFavoritedPlanning
    {
        string Id { get; }
        PlanningTypeEnum PlanningType { get; }
    }
}
