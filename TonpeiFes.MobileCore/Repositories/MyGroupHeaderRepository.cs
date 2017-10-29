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
            if (dbService.InitializeDatabaseConnection().Result)
            {
                var realm = Realms.Realm.GetInstance(dbService.MasterDataConnectionConfiguration);
                realm.Refresh();
                return realm.All<MyGroupHeader>();
            }
            else
                return new List<MyGroupHeader>();
        }

        public MyGroupHeader GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public MyGroupHeader GetOne(string id)
        {
            if (dbService.InitializeDatabaseConnection().Result)
            {
                var realm = Realms.Realm.GetInstance(dbService.MasterDataConnectionConfiguration);
                realm.Refresh();
                return realm.Find<MyGroupHeader>(id);
            }
            else
                return null;
        }
    }
}
