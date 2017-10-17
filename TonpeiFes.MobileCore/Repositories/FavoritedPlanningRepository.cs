using System;
using System.Collections.Generic;
using System.Linq;
using TonpeiFes.MobileCore.Models.DataObjects;
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
            var realm = Realms.Realm.GetInstance(dbService.MasterDataConnectionConfiguration);
            realm.Write(() => {
                realm.Add(item);
            });
        }

        public void Delete(FavoritedPlanning item)
        {
            var realm = Realms.Realm.GetInstance(dbService.MasterDataConnectionConfiguration);
            var _item = realm.All<FavoritedPlanning>().First(elem => elem.Id == item.Id && elem.PlanningType == item.PlanningType);
            if (_item != null)
            {
                using (var trans = realm.BeginWrite())
                {
                    realm.Remove(_item);
                    trans.Commit();
                }
            }
        }

        public IEnumerable<FavoritedPlanning> GetAll()
        {
            return Realms.Realm.GetInstance(dbService.MasterDataConnectionConfiguration).All<FavoritedPlanning>();
        }

        public FavoritedPlanning GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public FavoritedPlanning GetOne(string id)
        {
            return Realms.Realm.GetInstance(dbService.MasterDataConnectionConfiguration).Find<FavoritedPlanning>(id);
        }
    }
}
