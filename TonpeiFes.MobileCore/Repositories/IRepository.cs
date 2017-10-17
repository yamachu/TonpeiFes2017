using System;
using System.Collections.Generic;

namespace TonpeiFes.MobileCore.Repositories
{
    public interface IRepository<T>
    {
        T GetOne(int id);

        T GetOne(string id);

        IEnumerable<T> GetAll();

        void Add(T item);

        void Delete(T item);
    }
}
