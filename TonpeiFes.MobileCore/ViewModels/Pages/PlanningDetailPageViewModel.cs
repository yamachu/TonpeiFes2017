using System;
using Prism.Navigation;
using Reactive.Bindings;
using TonpeiFes.Core.Models.Consts;
using TonpeiFes.Core.Models.DataObjects;
using TonpeiFes.MobileCore.Usecases;

namespace TonpeiFes.MobileCore.ViewModels.Pages
{
    public class PlanningDetailPageViewModel : ViewModelBase
    {
        public static readonly string ParameterID = "ParameterID";
        public static readonly string ParameterPlanningType = "ParameterPlanningType";

        public ReadOnlyReactiveProperty<bool> IsFavorited { get; }
        public AsyncReactiveCommand ToggleFavorited { get; }
        public ReactiveProperty<IPlanning> DetailModel { get; } = new ReactiveProperty<IPlanning>();
        private IShowPlanningDetail _showDetail;

        public PlanningDetailPageViewModel(IShowPlanningDetail showDetail)
        {
            _showDetail = showDetail;

            IsFavorited = showDetail.IsFavorited;

            ToggleFavorited = new AsyncReactiveCommand();
            ToggleFavorited.Subscribe(async(_) =>
            {
                await _showDetail.TogglePlanningFavoritedState(!IsFavorited.Value);
            });
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            _showDetail.Initialize(parameters[ParameterID] as string, (PlanningTypeEnum)parameters[ParameterPlanningType]);
            DetailModel.Value = _showDetail.GetPlanning();
        }

        public static NavigationParameters GetNavigationParameter(string id, PlanningTypeEnum planningType)
        {
            return new NavigationParameters
            {
                { ParameterID, id },
                { ParameterPlanningType, planningType },
            };
        }
    }
}
