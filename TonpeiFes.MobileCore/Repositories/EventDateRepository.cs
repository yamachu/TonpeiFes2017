using System;
using System.Collections.Generic;
using TonpeiFes.MobileCore.Models.DataObjects;
using TonpeiFes.MobileCore.Services;
namespace TonpeiFes.MobileCore.Repositories
{
    public class EventDateRepository : IRepository<EventDate>
    {
        private RealmDatabaseService dbService;

        public EventDateRepository(IDatabaseService databaseService)
        {
            dbService = databaseService as RealmDatabaseService;
        }

        public void Add(EventDate item)
        {
            throw new NotImplementedException();
        }

        public void Delete(EventDate item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EventDate> GetAll()
        {
            return Realms.Realm.GetInstance(dbService.MasterDataConnectionConfiguration).All<EventDate>();
        }

        public EventDate GetOne(int id)
        {
            return Realms.Realm.GetInstance(dbService.MasterDataConnectionConfiguration).Find<EventDate>(id);
        }

        public EventDate GetOne(string id)
        {
            throw new NotImplementedException();
        }
    }
}
