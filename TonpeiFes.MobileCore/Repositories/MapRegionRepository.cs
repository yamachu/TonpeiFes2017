using System;
using System.Collections.Generic;
using TonpeiFes.Core.Models.DataObjects;
using TonpeiFes.MobileCore.Services;

namespace TonpeiFes.MobileCore.Repositories
{
    public class MapRegionRepository : IRepository<MapRegion>
    {
        private RealmDatabaseService dbService;

        public MapRegionRepository(IDatabaseService databaseService)
        {
            dbService = databaseService as RealmDatabaseService;
        }

        public void Add(MapRegion item)
        {
            throw new NotImplementedException();
        }

        public void Delete(MapRegion item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MapRegion> GetAll()
        {
            if (dbService.InitializeDatabaseConnection().Result)
            {
                var realm = Realms.Realm.GetInstance(dbService.MasterDataConnectionConfiguration);
                realm.Refresh();
                return realm.All<MapRegion>();
            }
            else
                return new List<MapRegion>();
        }

        public MapRegion GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public MapRegion GetOne(string id)
        {
            if (dbService.InitializeDatabaseConnection().Result)
            {
                var realm = Realms.Realm.GetInstance(dbService.MasterDataConnectionConfiguration);
                realm.Refresh();
                return realm.Find<MapRegion>(id);
            }
            else
                return null;
        }
    }
}
