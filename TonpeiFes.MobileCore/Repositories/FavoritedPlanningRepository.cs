using System;
using System.Collections.Generic;
using System.Linq;
using TonpeiFes.Core.Models.DataObjects;
using TonpeiFes.MobileCore.Services;
namespace TonpeiFes.MobileCore.Repositories
{
    public class FavoritedPlanningRepository : IRepository<FavoritedPlanning>
    {
        private RealmDatabaseService dbService;

        public FavoritedPlanningRepository(IDatabaseService databaseService)
        {
            dbService = databaseService as RealmDatabaseService;
        }

        public void Add(FavoritedPlanning item)
        {
            if (!dbService.InitializeDatabaseConnection().Result) return;
            var realm = Realms.Realm.GetInstance(dbService.LocalDataConnectionConfiguration);
            realm.Write(() => {
                realm.Add(item);
            });
            realm.Refresh();
        }

        public void Delete(FavoritedPlanning item)
        {
            if (!dbService.InitializeDatabaseConnection().Result) return;
            var realm = Realms.Realm.GetInstance(dbService.LocalDataConnectionConfiguration);
            var _item = realm.All<FavoritedPlanning>().FirstOrDefault(elem => elem.Id == item.Id);
            if (_item != null)
            {
                using (var trans = realm.BeginWrite())
                {
                    realm.Remove(_item);
                    trans.Commit();
                }
            }
            realm.Refresh();
        }

        public IEnumerable<FavoritedPlanning> GetAll()
        {
            if (dbService.InitializeDatabaseConnection().Result)
                return Realms.Realm.GetInstance(dbService.LocalDataConnectionConfiguration).All<FavoritedPlanning>();
            else
                return new List<FavoritedPlanning>();
        }

        public FavoritedPlanning GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public FavoritedPlanning GetOne(string id)
        {
            if (dbService.InitializeDatabaseConnection().Result)
                return Realms.Realm.GetInstance(dbService.LocalDataConnectionConfiguration).Find<FavoritedPlanning>(id);
            else
                return null;
        }
    }
}
