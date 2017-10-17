using System;
using System.Collections.Generic;
using TonpeiFes.MobileCore.Models.DataObjects;
using TonpeiFes.MobileCore.Services;
namespace TonpeiFes.MobileCore.Repositories
{
    public class StallRepository : IRepository<Stall>
    {
        private RealmDatabaseService dbService;

        public StallRepository(IDatabaseService databaseService)
        {
            dbService = databaseService as RealmDatabaseService;
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
            return Realms.Realm.GetInstance(dbService.MasterDataConnectionConfiguration).All<Stall>();
        }

        public Stall GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public Stall GetOne(string id)
        {
            return Realms.Realm.GetInstance(dbService.MasterDataConnectionConfiguration).Find<Stall>(id);
        }
    }
}
