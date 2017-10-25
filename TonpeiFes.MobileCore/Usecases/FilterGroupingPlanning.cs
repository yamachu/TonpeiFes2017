using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TonpeiFes.Core.Models.Consts;
using TonpeiFes.Core.Models.DataObjects;
using TonpeiFes.MobileCore.Extensions;
using TonpeiFes.MobileCore.Helpers;
using TonpeiFes.MobileCore.Repositories;

namespace TonpeiFes.MobileCore.Usecases
{
    public class FilterGroupingPlanning : IFilterGroupingPlanning
    {
        private readonly ObservableCollection<ObservableGroupCollection<MyGroupHeader, ISearchableListPlanning>> _plannings = new ObservableCollection<ObservableGroupCollection<MyGroupHeader, ISearchableListPlanning>>();
        public ReadOnlyObservableCollection<ObservableGroupCollection<MyGroupHeader, ISearchableListPlanning>> Plannings { get; }

        private IRepository<Exhibition> _exhibitionRepository;
        private IRepository<Stall> _stallRepository;
        private IRepository<FavoritedPlanning> _favoritedRepository;

        public FilterGroupingPlanning(IRepository<Exhibition> exhibitionRep,
                                      IRepository<Stall> stallRep,
                                      IRepository<FavoritedPlanning> favoritedRep)
        {
            _exhibitionRepository = exhibitionRep;
            _stallRepository = stallRep;
            _favoritedRepository = favoritedRep;

            Plannings = new ReadOnlyObservableCollection<ObservableGroupCollection<MyGroupHeader, ISearchableListPlanning>>(_plannings);
        }

        public async Task UpdateFilterConditions(string query, PlanningTypeEnum type, bool favorited, string placeId)
        {
            _plannings.Clear();

            switch (type)
            {
                case PlanningTypeEnum.STALL:
                    foreach (var ex in _stallRepository
                             .GetAll()
                             .FilterByKeyword(query)
                             .FilterByPlace(placeId)
                             .FilterByFavoritedStall(favorited, _favoritedRepository)
                             .Select(item => (dynamic)item as ISearchableListPlanning)
                             .IconedGroupingPlannings())
                    {
                        _plannings.Add(ex);
                    }
                    break;
                case PlanningTypeEnum.EXHIBITION:
                    foreach (var ex in _exhibitionRepository
                             .GetAll()
                             .FilterByKeyword(query)
                             .FilterByPlace(placeId)
                             .FilterByFavoritedExhibition(favorited, _favoritedRepository)
                             .Select(item => (dynamic)item as ISearchableListPlanning)
                             .IconedGroupingPlannings())
                    {
                        _plannings.Add(ex);
                    }
                    break;
            }
        }
    }

    // 毎回Where叩いててヤバイ（死
    // GetAllのやつは毎回同じなのだから，そこはいじらなくて良くする？
    public static class FilterGroupPlanningExtension
    {
        public static IEnumerable<Exhibition> FilterByKeyword(this IEnumerable<Exhibition> list, string query)
        {
            return string.IsNullOrEmpty(query) ? list : list.Where((item) => item.SearchableKeywords.Contains(query));
        }

        public static IEnumerable<Stall> FilterByKeyword(this IEnumerable<Stall> list, string query)
        {
            return string.IsNullOrEmpty(query) ? list : list.Where((item) => item.SearchableKeywords.Contains(query));
        }

        public static IEnumerable<Exhibition> FilterByPlace(this IEnumerable<Exhibition> list, string placeId)
        {
            if (placeId.IsNullOrEmptyOrWhitespace()) return list;
            return list.Where(item => item.MappedRegion.Id == placeId);
        }

        public static IEnumerable<Stall> FilterByPlace(this IEnumerable<Stall> list, string placeId)
        {
            if (placeId.IsNullOrEmptyOrWhitespace()) return list;
            return list.Where(item => item.MappedRegion.Id == placeId);
        }

        public static IEnumerable<Exhibition> FilterByFavoritedExhibition(this IEnumerable<Exhibition> list, bool favorited, IRepository<FavoritedPlanning> repository)
        {
            if (!favorited) return list;
            var favoritedList = repository.GetAll().Where((item) => item.PlanningType == PlanningTypeEnum.EXHIBITION);
            return favoritedList.Select((fav) => list.FirstOrDefault((item) => item.Id == fav.Id));
        }

        public static IEnumerable<Stall> FilterByFavoritedStall(this IEnumerable<Stall> list, bool favorited, IRepository<FavoritedPlanning> repository)
        {
            if (!favorited) return list;
            var favoritedList = repository.GetAll().Where((item) => item.PlanningType == PlanningTypeEnum.STALL);
            return favoritedList.Select((fav) => list.FirstOrDefault((item) => item.Id == fav.Id));
        }
    }
}
