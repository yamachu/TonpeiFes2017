using System;
using System.Collections.Generic;
using TonpeiFes.Core.Models.DataObjects;
using TonpeiFes.MobileCore.Services;
namespace TonpeiFes.MobileCore.Repositories
{
    public class ExhibitionRepository : IRepository<Exhibition>
    {
        private RealmDatabaseService dbService;

        public ExhibitionRepository(IDatabaseService databaseService)
        {
            dbService = databaseService as RealmDatabaseService;
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
            if (dbService.InitializeDatabaseConnection().Result)
            {
                var realm = Realms.Realm.GetInstance(dbService.MasterDataConnectionConfiguration);
                realm.Refresh();
                return realm.All<Exhibition>();
            }
            else
                return new List<Exhibition>();
        }

        public Exhibition GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public Exhibition GetOne(string id)
        {
            if (dbService.InitializeDatabaseConnection().Result)
            {
                var realm = Realms.Realm.GetInstance(dbService.MasterDataConnectionConfiguration);
                realm.Refresh();
                return realm.Find<Exhibition>(id);
            }
            else
                return null;
        }
    }
}
