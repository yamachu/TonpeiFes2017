using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Reactive.Bindings;
using System.Reactive.Linq;

namespace TonpeiFes.ViewModels.Pages
{
    public class PlanningListRootPageViewModel : ViewModelBase
    {
        // ToDo: 文字列リソースはどこかにまとめる
        public string Title => "模擬店/企画";
        public string OutdoorSegment { get; } = "屋外";
        public string IndoorSegment { get; } = "屋外";

        public ReactiveProperty<int> SelectedSegment { get; } = new ReactiveProperty<int>(0);
        public ReactiveProperty<string> SearchQuery { get; } = new ReactiveProperty<string>();

        public PlanningListRootPageViewModel()
        {
            SelectedSegment.Subscribe(selectedSegment => 
            {
                if (selectedSegment == 0) {}
                else {}
            });

            SearchQuery.Throttle(TimeSpan.FromMilliseconds(400)).Subscribe(query =>
            {
                System.Diagnostics.Debug.WriteLine($"Query Update: {query}");
            });
        }
    }
}
