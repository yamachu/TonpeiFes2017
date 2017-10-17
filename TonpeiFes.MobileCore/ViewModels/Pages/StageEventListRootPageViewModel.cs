﻿using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using Prism.Commands;
using Reactive.Bindings;
using TonpeiFes.MobileCore.Helpers;
using TonpeiFes.MobileCore.Models.DataObjects;
using TonpeiFes.MobileCore.Usecases;

namespace TonpeiFes.MobileCore.ViewModels.Pages
{
    public class StageEventListRootPageViewModel : ViewModelBase
    {
        public string Title => "ステージイベント";
        public string Day1Segment => "11/3";
        public string Day2Segment => "11/4";
        public string Day3Segment => "11/5";

        public ReactiveProperty<int> SelectedSegment { get; } = new ReactiveProperty<int>(0);
        public ReadOnlyReactiveProperty<string> IconSource { get; }
        public ICommand FavButtonClickCommand { get; }
        public ReadOnlyReactiveCollection<ObservableGroupCollection<string, ISearchableListPlanning>> Plannings { get; }

        private ReactiveProperty<bool> FavStateObservable = new ReactiveProperty<bool>(false);
        private IFilterGroupingStageEvent _eventUsecase;

        public StageEventListRootPageViewModel(IFilterGroupingStageEvent eventUsecase)
        {
            _eventUsecase = eventUsecase;

            SelectedSegment.Subscribe(selectedSegment =>
            {
                _eventUsecase.UpdateFilterConditions(SelectedSegment.Value, FavStateObservable.Value);
            });

            FavButtonClickCommand = new DelegateCommand(() =>
            {
                FavStateObservable.Value = !FavStateObservable.Value;
                _eventUsecase.UpdateFilterConditions(SelectedSegment.Value, FavStateObservable.Value);
            });

            IconSource = FavStateObservable.Select((isFavActive) =>
            {
                return $@"ion-ios-heart{(isFavActive ? "" : "-outline")}";
            }).ToReadOnlyReactiveProperty("ion-ios-heart");

            Plannings = _eventUsecase.Plannings.ToReadOnlyReactiveCollection();
        }
    }
}
