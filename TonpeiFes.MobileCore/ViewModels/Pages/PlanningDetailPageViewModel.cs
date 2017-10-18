using System;
using Prism.Navigation;
using TonpeiFes.MobileCore.Models.Consts;

namespace TonpeiFes.MobileCore.ViewModels.Pages
{
    public class PlanningDetailPageViewModel : ViewModelBase
    {
        public static readonly string ParameterID = "ParameterID";
        public static readonly string ParameterPlanningType = "ParameterPlanningType";

        public PlanningDetailPageViewModel()
        {
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            // Get planning
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
