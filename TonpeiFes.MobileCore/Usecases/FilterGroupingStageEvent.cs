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
    public class FilterGroupingStageEvent : IFilterGroupingStageEvent
    {
        private readonly ObservableCollection<ObservableGroupCollection<string, ISearchableListPlanning>> _plannings = new ObservableCollection<ObservableGroupCollection<string, ISearchableListPlanning>>();
        public ReadOnlyObservableCollection<ObservableGroupCollection<string, ISearchableListPlanning>> Plannings { get; }

        private IRepository<StageEvent> _stageEventRepository;
        private IRepository<FavoritedPlanning> _favoritedRepository;

        private int ActiveSegment = 0;
        private bool IsFavorited = false;

        public FilterGroupingStageEvent(IRepository<StageEvent> stageRep,
                                        IRepository<FavoritedPlanning> favoritedRep)
        {
            _stageEventRepository = stageRep;
            _favoritedRepository = favoritedRep;

            Plannings = new ReadOnlyObservableCollection<ObservableGroupCollection<string, ISearchableListPlanning>>(_plannings);
        }

        public async Task UpdateFilterConditions(int activeSegment, bool favorited)
        {
            ActiveSegment = activeSegment;
            IsFavorited = favorited;

            _plannings.Clear();

            var openDay = (EventDateEnum)Enum.ToObject(typeof(EventDateEnum), (1 << activeSegment));

            foreach (var ex in _stageEventRepository.GetAll().FilterByOpeningDay(openDay).FilterByFavoritedExhibition(IsFavorited, _favoritedRepository).Select(item => (dynamic)item as ISearchableListPlanning).GroupingPlannings())
            {
                _plannings.Add(ex);
            }
        }
    }

    public static class FilterGroupStageEventExtension
    {
        public static IEnumerable<StageEvent> FilterByOpeningDay(this IEnumerable<StageEvent> list, EventDateEnum openDay)
        {
            return list.Where(item => item.OpenDate.HasFlag(openDay));
        }

        public static IEnumerable<StageEvent> FilterByFavoritedExhibition(this IEnumerable<StageEvent> list, bool favorited, IRepository<FavoritedPlanning> repository)
        {
            if (!favorited) return list;
            var favoritedList = repository.GetAll().Where((item) => item.PlanningType == PlanningTypeEnum.STAGE);
            return favoritedList.Select((fav) => list.First((item) => item.Id == fav.Id));
        }
    }
}
