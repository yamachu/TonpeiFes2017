using System;
using System.Threading.Tasks;
using Reactive.Bindings;
using TonpeiFes.MobileCore.Models.Consts;
using TonpeiFes.MobileCore.Models.DataObjects;
using TonpeiFes.MobileCore.Repositories;

namespace TonpeiFes.MobileCore.Usecases
{
    public class ShowPlanningDetail : IShowPlanningDetail
    {
        public ReadOnlyReactiveProperty<bool> IsFavorited { get; }
        private ReactiveProperty<bool> _isFavorited = new ReactiveProperty<bool>();

        private IRepository<Exhibition> _exhibitionRepository;
        private IRepository<Stall> _stallRepository;
        private IRepository<StageEvent> _stageRepository;
        private IRepository<FavoritedPlanning> _favoritedRepository;

        private string _id = null;
        private PlanningTypeEnum _planningType;

        public ShowPlanningDetail(IRepository<Exhibition> exhibitionRep,
                                  IRepository<Stall> stallRep,
                                  IRepository<StageEvent> stageRep,
                                  IRepository<FavoritedPlanning> favoritedRep)
        {
            _exhibitionRepository = exhibitionRep;
            _stallRepository = stallRep;
            _stageRepository = stageRep;
            _favoritedRepository = favoritedRep;

            IsFavorited = _isFavorited.ToReadOnlyReactiveProperty();
        }

        public void Initialize(string id, PlanningTypeEnum planningType)
        {
            _id = id;
            _planningType = planningType;
        }

        public ISearchableListPlanning GetPlanning()
        {
            switch(_planningType)
            {
                case PlanningTypeEnum.EXHIBITION:
                    return _exhibitionRepository.GetOne(_id);
                case PlanningTypeEnum.STAGE:
                    return _stageRepository.GetOne(_id);
                case PlanningTypeEnum.STALL:
                    return _stallRepository.GetOne(_id);
                default:
                    throw new ArgumentException();
            }
        }

        public async Task TogglePlanningFavoritedState(bool isFavorited)
        {
            if (isFavorited) _favoritedRepository.Add(new FavoritedPlanning { Id = _id, PlanningType = _planningType });
            else _favoritedRepository.Delete(new FavoritedPlanning { Id = _id, PlanningType = _planningType });

            _isFavorited.Value = isFavorited;
        }
    }
}
