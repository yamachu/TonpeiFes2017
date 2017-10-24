using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using Reactive.Bindings;
using TonpeiFes.MobileCore.Repositories;
using TonpeiFes.Core.Models.DataObjects;

namespace TonpeiFes.MobileCore.ViewModels.Pages
{
    public class DetailFloorPageViewModel : ViewModelBase
    {
        public static readonly string ParameterPlaceId = "ParameterPlaceId";

        public ReactiveProperty<string> Title { get; } = new ReactiveProperty<string>();
        public ReactiveProperty<string> ImageSource { get; } = new ReactiveProperty<string>();
        public AsyncReactiveCommand CloseButtonClickCommand { get; }
        private INavigationService _navigationService;
        private IRepository<MyGroupHeader> _repository;

        public DetailFloorPageViewModel(INavigationService navigationService, IRepository<MyGroupHeader> repository)
        {
            _navigationService = navigationService;
            _repository = repository;

            CloseButtonClickCommand = new AsyncReactiveCommand();
            CloseButtonClickCommand.Subscribe(async () =>
            {
                await _navigationService.GoBackAsync(null, true);
            });
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            var place = _repository.GetOne(parameters[ParameterPlaceId] as string);
            Title.Value = $"{place.Key}の詳細";
            ImageSource.Value = $"TonpeiFes.Forms.Resources.{place.Source}";
        }

        public static NavigationParameters GetNavigationParameter(string key)
        {
            return new NavigationParameters
            {
                { ParameterPlaceId, key }
            };
        }
    }
}
