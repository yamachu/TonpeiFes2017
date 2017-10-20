using System;
using TonpeiFes.Core.Models.Consts;
namespace TonpeiFes.Core.Models.DataObjects
{
    public interface IFavoritedPlanning
    {
        string Id { get; }
        PlanningTypeEnum PlanningType { get; }
    }
}
