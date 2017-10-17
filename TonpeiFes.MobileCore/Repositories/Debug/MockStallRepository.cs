using System;
using System.Collections.Generic;
using System.Linq;
using TonpeiFes.MobileCore.Models.DataObjects;
namespace TonpeiFes.MobileCore.Repositories.Debug
{
    public class MockStallRepository : IRepository<Stall>
    {
        private List<Stall> Source;

        public MockStallRepository()
        {
            Source = new List<Stall>();
        }

        public void Add(Stall item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Stall item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Stall> GetAll()
        {
            return Source;
        }

        public Stall GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public Stall GetOne(string id)
        {
            return Source.First((elem) => elem.Id == id);
        }
    }
}
