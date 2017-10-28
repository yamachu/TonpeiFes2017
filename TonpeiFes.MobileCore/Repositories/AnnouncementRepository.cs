using System;
using System.Collections.Generic;
using TonpeiFes.Core.Models.DataObjects;
using TonpeiFes.MobileCore.Services;

namespace TonpeiFes.MobileCore.Repositories
{
    public class AnnouncementRepository : IRepository<Announcement>
    {
        private RealmDatabaseService dbService;

        public AnnouncementRepository(IDatabaseService databaseService)
        {
            dbService = databaseService as RealmDatabaseService;
        }

        public void Add(Announcement item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Announcement item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Announcement> GetAll()
        {
            if (dbService.InitializeDatabaseConnection().Result)
                return Realms.Realm.GetInstance(dbService.MasterDataConnectionConfiguration).All<Announcement>();
            else
                return new List<Announcement>();
        }

        public Announcement GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public Announcement GetOne(string id)
        {
            if (dbService.InitializeDatabaseConnection().Result)
                return Realms.Realm.GetInstance(dbService.MasterDataConnectionConfiguration).Find<Announcement>(id);
            else
                return null;
        }
    }
}
