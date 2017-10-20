using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TonpeiFes.Core.Models.DataObjects;
using TonpeiFes.MobileCore.Extensions;
using TonpeiFes.MobileCore.Helpers;
using TonpeiFes.MobileCore.Repositories;

namespace TonpeiFes.MobileCore.Usecases
{
    public class FilterGroupingPlanning : IFilterGroupingPlanning
    {
        private readonly ObservableCollection<ObservableGroupCollection<string, ISearchableListPlanning>> _plannings = new ObservableCollection<ObservableGroupCollection<string, ISearchableListPlanning>>();
        public ReadOnlyObservableCollection<ObservableGroupCollection<string, ISearchableListPlanning>> Plannings { get; }

        private IRepository<Exhibition> _exhibitionRepository;
        private IRepository<Stall> _stallRepository;
        private IRepository<FavoritedPlanning> _favoritedRepository;

        private string SearchQuery = "";
        private int ActiveSegment = 0;
        private bool IsFavorited = false;

        public FilterGroupingPlanning(IRepository<Exhibition> exhibitionRep,
                                      IRepository<Stall> stallRep,
                                      IRepository<FavoritedPlanning> favoritedRep)
        {
            _exhibitionRepository = exhibitionRep;
            _stallRepository = stallRep;
            _favoritedRepository = favoritedRep;

            Plannings = new ReadOnlyObservableCollection<ObservableGroupCollection<string, ISearchableListPlanning>>(_plannings);
        }

        public async Task UpdateFilterConditions(string query, int activeSegment, bool favorited)
        {
            SearchQuery = query;
            ActiveSegment = activeSegment;
            IsFavorited = favorited;

            _plannings.Clear();
            switch(ActiveSegment){
                case 0:
                    foreach (var ex in _exhibitionRepository.GetAll().FilterByKeyword(SearchQuery).FilterByFavoritedExhibition(IsFavorited, _favoritedRepository).Select(item => (dynamic)item as ISearchableListPlanning).GroupingPlannings())
                    {
                        _plannings.Add(ex);
                    }
                    break;
                case 1:
                    foreach (var ex in _stallRepository.GetAll().FilterByKeyword(SearchQuery).FilterByFavoritedStall(IsFavorited, _favoritedRepository).Select(item => (dynamic)item as ISearchableListPlanning).GroupingPlannings())
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

        public static IEnumerable<Exhibition> FilterByFavoritedExhibition(this IEnumerable<Exhibition> list, bool favorited, IRepository<FavoritedPlanning> repository)
        {
            if (!favorited) return list;
            var favoritedList = repository.GetAll().Where((item) => item.PlanningType == Core.Models.Consts.PlanningTypeEnum.EXHIBITION);
            return favoritedList.Select((fav) => list.First((item) => item.Id == fav.Id));
        }

        public static IEnumerable<Stall> FilterByFavoritedStall(this IEnumerable<Stall> list, bool favorited, IRepository<FavoritedPlanning> repository)
        {
            if (!favorited) return list;
            var favoritedList = repository.GetAll().Where((item) => item.PlanningType == Core.Models.Consts.PlanningTypeEnum.STALL);
            return favoritedList.Select((fav) => list.First((item) => item.Id == fav.Id));
        }
    }
}
