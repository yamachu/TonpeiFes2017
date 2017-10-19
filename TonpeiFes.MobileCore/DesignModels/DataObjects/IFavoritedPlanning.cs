using System;
using TonpeiFes.MobileCore.DesignModels.Consts;
namespace TonpeiFes.MobileCore.DesignModels.DataObjects
{
    public interface IFavoritedPlanning
    {
        string Id { get; }
        PlanningTypeEnum PlanningType { get; }
    }
}
