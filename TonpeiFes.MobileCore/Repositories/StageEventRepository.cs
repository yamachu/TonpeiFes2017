using System;
using System.Collections.Generic;
using TonpeiFes.Core.Models.DataObjects;
using TonpeiFes.MobileCore.Services;
namespace TonpeiFes.MobileCore.Repositories
{
    public class StageEventRepository : IRepository<StageEvent>
    {
        private RealmDatabaseService dbService;

        public StageEventRepository(IDatabaseService databaseService)
        {
            dbService = databaseService as RealmDatabaseService;
        }

        public void Add(StageEvent item)
        {
            throw new NotImplementedException();
        }

        public void Delete(StageEvent item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StageEvent> GetAll()
        {
            if (dbService.InitializeDatabaseConnection().Result)
            {
                var realm = Realms.Realm.GetInstance(dbService.MasterDataConnectionConfiguration);
                realm.Refresh();
                return realm.All<StageEvent>();
            }
            else
                return new List<StageEvent>();
        }

        public StageEvent GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public StageEvent GetOne(string id)
        {
            if (dbService.InitializeDatabaseConnection().Result)
            {
                var realm = Realms.Realm.GetInstance(dbService.MasterDataConnectionConfiguration);
                realm.Refresh();
                return realm.Find<StageEvent>(id);
            }
            else
                return null;
        }
    }
}
