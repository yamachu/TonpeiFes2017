using System;
using System.Collections.Generic;
using System.Linq;
using TonpeiFes.Core.Models.DataObjects;
namespace TonpeiFes.MobileCore.Repositories.Debug
{
    public class MockEventDateRepository : IRepository<EventDate>
    {
        private List<EventDate> Source;

        public MockEventDateRepository()
        {
            Source = new List<EventDate>()
            {
                new EventDate(){ Id = 0, Date = new DateTimeOffset(2017, 11, 3, 0, 0, 0, new TimeSpan(9, 0, 0))},
                new EventDate(){ Id = 0, Date = new DateTimeOffset(2017, 11, 4, 0, 0, 0, new TimeSpan(9, 0, 0))},
                new EventDate(){ Id = 0, Date = new DateTimeOffset(2017, 11, 5, 0, 0, 0, new TimeSpan(9, 0, 0))},
            };
        }

        public void Add(EventDate item)
        {
            throw new NotImplementedException();
        }

        public void Delete(EventDate item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EventDate> GetAll()
        {
            return Source;
        }

        public EventDate GetOne(int id)
        {
            return Source.First((elem) => elem.Id == id);
        }

        public EventDate GetOne(string id)
        {
            throw new NotImplementedException();
        }
    }
}
