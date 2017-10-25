using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Reactive.Bindings;
using TonpeiFes.Core.Models.DataObjects;
using TonpeiFes.MobileCore.Usecases;

namespace TonpeiFes.MobileCore.ViewModels.Pages
{
    public class HomePageViewModel : ViewModelBase
    {
        public string Title => "東北大学祭 2017";
        public ReadOnlyReactiveCollection<Announcement> Announcements { get; }
        private IShowAnnouncement _showAnnouce;

        public HomePageViewModel(IShowAnnouncement showAnnounce)
        {
            _showAnnouce = showAnnounce;

            Announcements = _showAnnouce.Announcements.ToReadOnlyReactiveCollection();

            _showAnnouce.InitializeAnnouncements();
        }
    }
}
