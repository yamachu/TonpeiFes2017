using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Events;
using TonpeiFes.MobileCore.Models.EventArgs;

namespace TonpeiFes.MobileCore.ViewModels.Pages
{
    public class FestaMapRootPageViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;

        public FestaMapRootPageViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<MapMoveEvent>().Publish(new MapMoveEventArgs(0, 0, true));
        }
    }
}
