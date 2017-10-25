using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TonpeiFes.Core.Models.DataObjects;

namespace TonpeiFes.MobileCore.Usecases
{
    public interface IShowAnnouncement
    {
        ReadOnlyObservableCollection<Announcement> Announcements { get; }

        Task InitializeAnnouncements();
    }
}
