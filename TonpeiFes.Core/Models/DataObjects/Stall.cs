using System;
using System.Collections.Generic;
using System.Linq;
using Realms;
using TonpeiFes.Core.Models.Consts;

namespace TonpeiFes.Core.Models.DataObjects
{
    public class Stall : RealmObject, IPlanning, ISearchable, IGroupable
    {
        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Title { get; set; }

        public string Owner { get; set; }

        [Ignored]
        public IList<IDescription> Descriptions
        {
            get
            {
                return InnerDescriptions.Select((description) => description as IDescription).ToList();
            }
        }

        public IList<StallDescription> InnerDescriptions { get; }

        // ToDo: Validate PlannningTypeNumber 1~3 or not
        public int PlanningTypeNumber { get; set; } = (int)PlanningTypeEnum.STALL;

        [Ignored]
        public PlanningTypeEnum PlanningType
        {
            get
            {
                return (PlanningTypeEnum)Enum.ToObject(typeof(PlanningTypeEnum), PlanningTypeNumber);
            }
            set
            {
                PlanningTypeNumber = (int)value;
            }
        }

        public SNSInfomations SNS { get; set; }

        public bool IsT1 { get; set; } = false;

        public bool IsAcademic { get; set; } = false;

        public IList<EventDate> OpenDate { get; }

        // ToDo: Change class type string to ...
        public string MappedRegion { get; set; }

        // ToDo: Change class type string to ...
        public string HeaderGroupedRegion { get; set; }

        public string LocationDetail { get; set; }

        [Indexed]
        public string SearchableKeywords { get; set; }

        public List<string> Keywords { get; }

        [Ignored]
        public string GroupHeader
        {
            get
            {
                return HeaderGroupedRegion;
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
