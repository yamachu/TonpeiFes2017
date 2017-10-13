using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Reactive.Bindings;

namespace TonpeiFes.MobileCore.ViewModels.Pages
{
    public class StageEventListRootPageViewModel : ViewModelBase
    {
        public string Title => "ステージイベント";
        public string Day1Segment => "11/3";
        public string Day2Segment => "11/4";
        public string Day3Segment => "11/5";

        public ReactiveProperty<int> SelectedSegment { get; } = new ReactiveProperty<int>(0);
        public StageEventListRootPageViewModel()
        {

        }
    }
}
