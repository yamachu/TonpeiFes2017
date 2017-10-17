using System;
using System.Collections.Generic;
using System.Linq;
using TonpeiFes.MobileCore.Models.DataObjects;
namespace TonpeiFes.MobileCore.Repositories.Debug
{
    public class MockEventDateRepository : IRepository<EventDate>
    {
        private List<EventDate> Source;

        public MockEventDateRepository()
        {
            // ToDo: Set dummy data
            Source = new List<EventDate>();
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
