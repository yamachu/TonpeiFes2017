using System;
using System.Threading.Tasks;
using Reactive.Bindings;
using TonpeiFes.Core.Models.Consts;
using TonpeiFes.Core.Models.DataObjects;

namespace TonpeiFes.MobileCore.Usecases
{
    public interface IShowPlanningDetail
    {
        ISearchableListPlanning GetPlanning();
        void Initialize(string id, PlanningTypeEnum planningType);
        ReadOnlyReactiveProperty<bool> IsFavorited { get; }
        Task TogglePlanningFavoritedState(bool isFavorited);
    }
}
