using System;
using System.Collections.Generic;
using System.Linq;
using TonpeiFes.Core.Models.DataObjects;

namespace TonpeiFes.MobileCore.Repositories.Debug
{
    public class MockMyGroupHeaderRepository : IRepository<MyGroupHeader>
    {
        private static List<MyGroupHeader> Source;

        public MockMyGroupHeaderRepository()
        {
            Source = new List<MyGroupHeader>()
            {
                new MyGroupHeader(){ Key = "A棟1階", Source = "A1.jpg" },
                new MyGroupHeader(){ Key = "A棟2階", Source = "A2.jpg" },
                new MyGroupHeader(){ Key = "A棟3階", Source = "A3.jpg" },
                new MyGroupHeader(){ Key = "A棟4階", Source = "A4.jpg" },
                new MyGroupHeader(){ Key = "B棟1階", Source = "B1.jpg" },
                new MyGroupHeader(){ Key = "B棟1階", Source = "B2.jpg" },
                new MyGroupHeader(){ Key = "C棟1階", Source = "C1.jpg" },
                new MyGroupHeader(){ Key = "C棟2階", Source = "C2.jpg" },
                new MyGroupHeader(){ Key = "C棟3階", Source = "C3.jpg" },
                new MyGroupHeader(){ Key = "C棟4階", Source = "C4.jpg" },
            };
        }

        public void Add(MyGroupHeader item)
        {
            throw new NotImplementedException();
        }

        public void Delete(MyGroupHeader item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MyGroupHeader> GetAll()
        {
            return Source;
        }

        public MyGroupHeader GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public MyGroupHeader GetOne(string id)
        {
            return Source.FirstOrDefault((elem) => elem.Id == id);
        }
    }
}
