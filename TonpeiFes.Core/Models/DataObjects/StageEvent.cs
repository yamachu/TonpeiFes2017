using System;
using System.Collections.Generic;
using System.Linq;
using Realms;
using TonpeiFes.Core.Extensions;
using TonpeiFes.Core.Models.Consts;

namespace TonpeiFes.Core.Models.DataObjects
{
    public class StageEvent : RealmObject, ISearchableListPlanning
    {
        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Title { get; set; }

        public string Owner { get; set; }

        private IList<StageEventDescription> Descriptions_ { get; }

        [Ignored]
        public IList<IDescription> Descriptions
        {
            get
            {
                return Descriptions_.Select((description) => description as IDescription).ToList();
            }
        }

        private int PlanningType_ { get; set; } = (int)PlanningTypeEnum.STAGE;

        [Ignored]
        public PlanningTypeEnum PlanningType
        {
            get
            {
                return (PlanningTypeEnum)Enum.ToObject(typeof(PlanningTypeEnum), PlanningType_);
            }
            set
            {
                PlanningType_ = (int)value;
            }
        }

        public SNSInformations SNS { get; set; }

        public bool IsT1 { get; set; } = false;

        public bool IsAcademic { get; set; } = false;

        private int OpenDate_ { get; set; } = (int)(EventDateEnum.DAY1 | EventDateEnum.DAY2 | EventDateEnum.DAY3);

        [Ignored]
        public EventDateEnum OpenDate
        {
            get
            {
                return (EventDateEnum)Enum.ToObject(typeof(EventDateEnum), OpenDate_);
            }
            set
            {
                OpenDate_ = (int)value;
            }
        }

        public DateTimeOffset StartAt { get; set; }

        // ToDo: Change class type string to ...
        public string MappedRegion { get; set; }

        public string LocationDetail { get; set; }

        // Dummy
        public string SearchableKeywords { get; }
        public List<string> Keywords { get; }

        [Ignored]
        public string GroupHeader
        {
            get
            {
                return StartAt.ToString("HH:mm");
            }
        }

        public string OpenDateDetail_ { get; set; }

        [Ignored]
        public string OpenDateDetail
        {
            get
            {
                if (OpenDateDetail_.IsNullOrEmptyOrWhitespace() && OpenDate.IsAll()) return "";
                if (OpenDateDetail_.IsNullOrEmptyOrWhitespace()) return OpenDate.GetFormattedString();
                return OpenDateDetail_;
            }
        }

        public void UpdateLocationDetail()
        {
            LocationDetail = MappedRegion;
        }

        public void UpdateSearchableKeywords()
        {
            throw new NotImplementedException();
        }
    }
}
