using System;
using System.Collections.Generic;
using System.Linq;
using TonpeiFes.MobileCore.Models.DataObjects;
namespace TonpeiFes.MobileCore.Repositories.Debug
{
    public class MockExhibitionRepository : IRepository<Exhibition>
    {
        private List<Exhibition> Source;

        public MockExhibitionRepository()
        {
            Source = new List<Exhibition>();
        }

        public void Add(Exhibition item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Exhibition item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Exhibition> GetAll()
        {
            return Source;
        }

        public Exhibition GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public Exhibition GetOne(string id)
        {
            return Source.First((elem) => elem.Id == id);
        }
    }
}
