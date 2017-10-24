using System;
using System.Collections.Generic;
using TonpeiFes.Core.Models.DataObjects;
using TonpeiFes.MobileCore.Services;

namespace TonpeiFes.MobileCore.Repositories
{
    public class MyGroupHeaderRepository : IRepository<MyGroupHeader>
    {
        private RealmDatabaseService dbService;

        public MyGroupHeaderRepository(IDatabaseService databaseService)
        {
            dbService = databaseService as RealmDatabaseService;
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
            return Realms.Realm.GetInstance(dbService.MasterDataConnectionConfiguration).All<MyGroupHeader>();
        }

        public MyGroupHeader GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public MyGroupHeader GetOne(string id)
        {
            return Realms.Realm.GetInstance(dbService.MasterDataConnectionConfiguration).Find<MyGroupHeader>(id);
        }
    }
}
