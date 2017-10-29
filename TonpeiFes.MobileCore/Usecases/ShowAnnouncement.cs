using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using TonpeiFes.Core.Models.DataObjects;
using TonpeiFes.MobileCore.Repositories;

namespace TonpeiFes.MobileCore.Usecases
{
    public class ShowAnnouncement : IShowAnnouncement
    {
        private readonly ObservableCollection<Announcement> _announcements = new ObservableCollection<Announcement>();
        public ReadOnlyObservableCollection<Announcement> Announcements { get; }

        private IRepository<Announcement> _announcementRepository;

        public ShowAnnouncement(IRepository<Announcement> announceRep)
        {
            _announcementRepository = announceRep;

            Announcements = new ReadOnlyObservableCollection<Announcement>(_announcements);
        }

        public async Task InitializeAnnouncements()
        {
            _announcements.Clear();
            foreach (var item in _announcementRepository.GetAll().OrderBy(item => item.Index))
            {
                _announcements.Add(item);
            }
        }
    }
}
