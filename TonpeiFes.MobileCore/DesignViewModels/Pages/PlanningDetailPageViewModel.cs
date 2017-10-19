using System;
using System.Linq;
using Reactive.Bindings;
using TonpeiFes.MobileCore.DesignModels.Consts;
using TonpeiFes.MobileCore.DesignModels.DataObjects;
using TonpeiFes.MobileCore.Repositories;
using TonpeiFes.MobileCore.Usecases;

namespace TonpeiFes.MobileCore.DesignViewModels.Pages
{
    public class PlanningDetailPageViewModel
    {
        public ReactiveProperty<bool> IsFavorited { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<IPlanning> DetailModel { get; } = new ReactiveProperty<IPlanning>();

        public PlanningDetailPageViewModel()
        {
            // ここにいい感じにViewの編集に使う定数とかを定義する
            DetailModel.Value = new StageEvent();
        }
    }
}
