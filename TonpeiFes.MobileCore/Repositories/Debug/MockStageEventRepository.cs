using System;
using System.Collections.Generic;
using System.Linq;
using TonpeiFes.Core.Models.DataObjects;
namespace TonpeiFes.MobileCore.Repositories.Debug
{
    public class MockStageEventRepository : IRepository<StageEvent>
    {
        private List<StageEvent> Source;

        public MockStageEventRepository()
        {
            Source = new List<StageEvent>();
        }

        void IRepository<StageEvent>.Add(StageEvent item)
        {
            throw new NotImplementedException();
        }

        void IRepository<StageEvent>.Delete(StageEvent item)
        {
            throw new NotImplementedException();
        }

        IEnumerable<StageEvent> IRepository<StageEvent>.GetAll()
        {
            return Source;
        }

        StageEvent IRepository<StageEvent>.GetOne(int id)
        {
            throw new NotImplementedException();
        }

        StageEvent IRepository<StageEvent>.GetOne(string id)
        {
            return Source.First((elem) => elem.Id == id);
        }
    }
}
