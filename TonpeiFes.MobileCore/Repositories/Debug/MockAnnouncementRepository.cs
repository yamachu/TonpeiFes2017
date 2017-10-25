using System;
using System.Collections.Generic;
using System.Linq;
using TonpeiFes.Core.Models.DataObjects;

namespace TonpeiFes.MobileCore.Repositories.Debug
{
    public class MockAnnouncementRepository : IRepository<Announcement>
    {
        private List<Announcement> Source;

        public MockAnnouncementRepository()
        {
            Source = new List<Announcement>
            {
                new Announcement(){ Index = 2, Title = "2: タイトル1", HasContents = false },
                new Announcement(){ Index = 2, Title = "2: タイトル2", HasContents = true, Contents = "FooBar" },
                new Announcement(){ Index = 1, Title = "1: タイトルタイタイタイタイタイタイタイタイ1", HasContents = false, IsOutWebPage = true, Uri = "http://www.festa-tohoku.org/" },
                new Announcement(){ Index = 0, Title = "0: タイトル", HasContents = false },
            };
        }

        public void Add(Announcement item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Announcement item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Announcement> GetAll()
        {
            return Source;
        }

        public Announcement GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public Announcement GetOne(string id)
        {
            return Source.FirstOrDefault(item => item.Id == id);
        }
    }
}
