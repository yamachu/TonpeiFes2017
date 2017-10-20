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
            return Realms.Realm.GetInstance(dbService.MasterDataConnectionConfiguration).All<Exhibition>();
        }

        public Exhibition GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public Exhibition GetOne(string id)
        {
            return Realms.Realm.GetInstance(dbService.MasterDataConnectionConfiguration).Find<Exhibition>(id);
        }
    }
}
