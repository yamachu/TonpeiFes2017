using System;
using System.Collections.Generic;
using System.Linq;
using TonpeiFes.Core.Models.DataObjects;
namespace TonpeiFes.MobileCore.Repositories.Debug
{
    public class MockFavoritedPlanningRepository : IRepository<FavoritedPlanning>
    {
        private List<FavoritedPlanning> Source;

        public MockFavoritedPlanningRepository()
        {
            Source = new List<FavoritedPlanning>();
        }

        public void Add(FavoritedPlanning item)
        {
            Source.Add(item);
        }

        public void Delete(FavoritedPlanning item)
        {
            var _item = Source.First((elem) => elem.Id == item.Id && elem.PlanningType == item.PlanningType);
            Source.Remove(_item);
        }

        public IEnumerable<FavoritedPlanning> GetAll()
        {
            return Source;
        }

        public FavoritedPlanning GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public FavoritedPlanning GetOne(string id)
        {
            return Source.First((elem) => elem.Id == id);
        }
    }
}
