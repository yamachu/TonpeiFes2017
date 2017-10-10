using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TonpeiFes.ViewModels
{
    public class AppNavigationRootPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        // Use only Android MasterDetail Page
        public ICommand NavigationHomeCommand { get; }
        public ICommand NavigationPlanningRootCommand { get; }
        public ICommand NavigationStageEventRootCommand { get; }
        public ICommand NavigationMapRootCommand { get; }

        public AppNavigationRootPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            NavigationHomeCommand = new DelegateCommand(() =>
            {
                PlatformDependPageNavigation("HomePage");
            });

            NavigationPlanningRootCommand = new DelegateCommand(() =>
            {
                PlatformDependPageNavigation("PlanningListRootPage");
            });

            NavigationStageEventRootCommand = new DelegateCommand(() =>
            {
                PlatformDependPageNavigation("StageEventListRootPage");
            });

            NavigationMapRootCommand = new DelegateCommand(() =>
            {
                PlatformDependPageNavigation("FestaMapRootPage");
            });
        }

        private async Task PlatformDependPageNavigation(string pageName) => _navigationService.NavigateAsync($@"{(Device.RuntimePlatform == Device.Android ? "NavigationPage/" : "")}{pageName}");
    }
}
