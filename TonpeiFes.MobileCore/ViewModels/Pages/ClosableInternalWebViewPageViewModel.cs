using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Reactive.Bindings;
using Prism.Navigation;
using TonpeiFes.MobileCore.Repositories;
using TonpeiFes.Core.Models.DataObjects;

namespace TonpeiFes.MobileCore.ViewModels.Pages
{
    public class ClosableInternalWebViewPageViewModel : ViewModelBase
    {
        public static readonly string ParameterAnnounceId = "ParameterAnnounceId";

        public ReactiveProperty<string> Title { get; } = new ReactiveProperty<string>();
        public ReactiveProperty<string> Content { get; } = new ReactiveProperty<string>();
        public AsyncReactiveCommand CloseButtonClickCommand { get; }
        private INavigationService _navigationService;
        private IRepository<Announcement> _repository;

        public ClosableInternalWebViewPageViewModel(INavigationService navigationService, IRepository<Announcement> announceRep)
        {
            _navigationService = navigationService;
            _repository = announceRep;

            CloseButtonClickCommand = new AsyncReactiveCommand();
            CloseButtonClickCommand.Subscribe(async () =>
            {
                await _navigationService.GoBackAsync(null, true);
            });
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            var announce = _repository.GetOne(parameters[ParameterAnnounceId] as string);
            if (announce == null) return;
            Title.Value = $"{announce.Title}";
            Content.Value = announce.Contents;
        }

        public static NavigationParameters GetNavigationParameter(string key)
        {
            return new NavigationParameters
            {
                { ParameterAnnounceId, key }
            };
        }
    }
}
