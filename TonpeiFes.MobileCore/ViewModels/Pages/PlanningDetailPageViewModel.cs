using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using Prism.Commands;
using Prism.Navigation;
using Reactive.Bindings;
using TonpeiFes.Core.Models.Consts;
using TonpeiFes.Core.Models.DataObjects;
using TonpeiFes.MobileCore.Services;
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
        private INavigationService _navigationService;

        public AsyncReactiveCommand OpenMapCommand { get; }
        public ReadOnlyReactiveProperty<string> IconSource { get; }

        public ICommand OpenUriCommand { get; }
        private IOpenWebPageService _webPageService;

        public PlanningDetailPageViewModel(INavigationService navigationService, IShowPlanningDetail showDetail, IOpenWebPageService openWeb)
        {
            _showDetail = showDetail;
            _navigationService = navigationService;
            _webPageService = openWeb;

            IsFavorited = showDetail.IsFavorited;

            ToggleFavorited = new AsyncReactiveCommand();
            ToggleFavorited.Subscribe(async(_) =>
            {
                await _showDetail.TogglePlanningFavoritedState(!IsFavorited.Value);
            });

            IconSource = IsFavorited
                .Select((isFav) => $@"ion_ios_heart{(isFav ? "" : "_outline")}")
                .ToReadOnlyReactiveProperty($@"ion_ios_heart{(IsFavorited.Value ? "" : "_outline")}");

            OpenMapCommand = new AsyncReactiveCommand();
            OpenMapCommand.Subscribe(async (_) =>
            {
                await _navigationService.NavigateAsync("NavigationPage/FestaMapRootPage",
                                                       FestaMapRootPageViewModel.GetNavigationParameter(DetailModel.Value.Id, DetailModel.Value.PlanningType), true);
            });

            OpenUriCommand = new DelegateCommand<string>((uri) =>
            {
                _webPageService.OpenUri(uri);
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
