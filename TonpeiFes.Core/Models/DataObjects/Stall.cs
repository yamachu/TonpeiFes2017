using System;
using System.Collections.Generic;
using System.Linq;
using Realms;
using TonpeiFes.Core.Extensions;
using TonpeiFes.Core.Models.Consts;

namespace TonpeiFes.Core.Models.DataObjects
{
    public class Stall : RealmObject, ISearchableListPlanning
    {
        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Title { get; set; }

        public string Owner { get; set; }

        private IList<StallDescription> Descriptions_ { get; }

        [Ignored]
        public IList<IDescriptionImpl> Descriptions
        {
            get
            {
                return Descriptions_?.Select((description) => new IDescriptionImpl { Title = description.Title, Detail = description.Detail }).ToList();
            }
        }

        private int PlanningType_ { get; set; } = (int)PlanningTypeEnum.STALL;

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

        public MapRegion MappedRegion { get; set; }

        public MyGroupHeader HeaderGroupedRegion { get; set; }

        public string LocationDetail { get; set; }

        [Indexed]
        public string SearchableKeywords { get; set; }

        public List<string> Keywords { get; }

        [Ignored]
        public string GroupHeader
        {
            get
            {
                return HeaderGroupedRegion.Key;
            }
        }

        [Ignored]
        public MyGroupHeader IconedGroupHeader
        {
            get
            {
                return HeaderGroupedRegion;
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

        public FileObject AttachFile { get; set; }

        public string ImageUrl
        {
            get
            {
                return AttachFile?.IconOptimizedFileUrl ?? "loading.png";
            }
        }

        public void UpdateLocationDetail()
        {
            LocationDetail = HeaderGroupedRegion.Key;
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
