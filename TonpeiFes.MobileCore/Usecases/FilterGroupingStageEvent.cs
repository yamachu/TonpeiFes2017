using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TonpeiFes.MobileCore.Helpers;
using TonpeiFes.MobileCore.Models.DataObjects;
using TonpeiFes.MobileCore.Repositories;
using TonpeiFes.MobileCore.Extensions;

namespace TonpeiFes.MobileCore.Usecases
{
    public class FilterGroupingStageEvent : IFilterGroupingStageEvent
    {
        private readonly ObservableCollection<ObservableGroupCollection<string, ISearchableListPlanning>> _plannings = new ObservableCollection<ObservableGroupCollection<string, ISearchableListPlanning>>();
        public ReadOnlyObservableCollection<ObservableGroupCollection<string, ISearchableListPlanning>> Plannings { get; }

        private IRepository<StageEvent> _stageEventRepository;
        private IRepository<FavoritedPlanning> _favoritedRepository;
        private IRepository<EventDate> _eventDateRepository;

        private int ActiveSegment = 0;
        private bool IsFavorited = false;

        public FilterGroupingStageEvent(IRepository<StageEvent> stageRep,
                                        IRepository<FavoritedPlanning> favoritedRep,
                                        IRepository<EventDate> dateRep)
        {
            _stageEventRepository = stageRep;
            _favoritedRepository = favoritedRep;
            _eventDateRepository = dateRep;

            Plannings = new ReadOnlyObservableCollection<ObservableGroupCollection<string, ISearchableListPlanning>>(_plannings);
        }

        public async Task UpdateFilterConditions(int activeSegment, bool favorited)
        {
            ActiveSegment = activeSegment;
            IsFavorited = favorited;

            _plannings.Clear();

            var openDay = _eventDateRepository.GetOne(ActiveSegment);

            foreach (var ex in _stageEventRepository.GetAll().FilterByOpeningDay(openDay).FilterByFavoritedExhibition(IsFavorited, _favoritedRepository).GroupingPlannings())
            {
                _plannings.Add(ex);
            }
        }
    }

    public static class FilterGroupStageEventExtension
    {
        public static IEnumerable<StageEvent> FilterByOpeningDay(this IEnumerable<StageEvent> list, EventDate openDay)
        {
            // 実運用では Contains でも問題はないが，Mockデータで行う場合Object Equalのoverrideをしていないのでこうせざるを得ない
            return list.Where(item => item.OpenDate.Any((day) => day.Id == openDay.Id));
        }

        public static IEnumerable<StageEvent> FilterByFavoritedExhibition(this IEnumerable<StageEvent> list, bool favorited, IRepository<FavoritedPlanning> repository)
        {
            if (!favorited) return list;
            var favoritedList = repository.GetAll().Where((item) => item.PlanningType == Models.Consts.PlanningTypeEnum.STAGE);
            return favoritedList.Select((fav) => list.First((item) => item.Id == fav.Id));
        }
    }
}
