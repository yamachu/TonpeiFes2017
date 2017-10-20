using System;
using System.Collections.Generic;
using System.Linq;

using TonpeiFes.MobileCore.Extensions;
using TonpeiFes.MobileCore.DesignModels.Consts;

namespace TonpeiFes.MobileCore.DesignModels.DataObjects
{
    public class Stall : ISearchableListPlanning
    {
        
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Title { get; set; }

        public string Owner { get; set; }

        public IList<StallDescription> Descriptions_ { get; } = new List<StallDescription>();

        
        public IList<IDescription> Descriptions
        {
            get
            {
                return Descriptions_.Select((description) => description as IDescription).ToList();
            }
        }

        private int PlanningType_ { get; set; } = (int)PlanningTypeEnum.STALL;

        
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

        // ToDo: Change class type string to ...
        public string MappedRegion { get; set; }

        // ToDo: Change class type string to ...
        public string HeaderGroupedRegion { get; set; }

        public string LocationDetail { get; set; }

        
        public string SearchableKeywords { get; set; }

        public List<string> Keywords { get; } = new List<string>();

        
        public string GroupHeader
        {
            get
            {
                return HeaderGroupedRegion;
            }
        }

        public string OpenDateDetail_ { get; set; }

        
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
            LocationDetail = HeaderGroupedRegion;
        }

        public void UpdateSearchableKeywords()
        {
            SearchableKeywords = Title
                + " $$$ " + Owner
                + " $$$ " + (Descriptions?.Select(description => $"{description.Title} $$$ {description.Detail}").Aggregate((acc, next) => $"{acc} $$$ {next}") ?? "")
                + " $$$ " + LocationDetail
                + " $$$ " + string.Join(" $$$ ", Keywords ?? new List<string>())
                + " $$$ " + $@"{(IsT1 ? "T1 $$$ T-1" : "")}"
                + " $$$ " + $@"{(IsAcademic ? "学術" : "")}";
        }
    }
}
