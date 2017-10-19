﻿using System;
using System.Collections.Generic;
using System.Linq;
using Realms;
using TonpeiFes.MobileCore.Models.Consts;

namespace TonpeiFes.MobileCore.Models.DataObjects
{
    public class StageEvent : RealmObject, ISearchableListPlanning
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

        public IList<StageEventDescription> InnerDescriptions { get; }

        // ToDo: Validate PlannningTypeNumber 1~3 or not
        public int PlanningTypeNumber { get; set; } = (int)PlanningTypeEnum.STAGE;

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

        public SNSInformations SNS { get; set; }

        public bool IsT1 { get; set; } = false;

        public bool IsAcademic { get; set; } = false;

        public IList<EventDate> OpenDate { get; }

        public DateTimeOffset StartAt { get; set; }

        public DateTimeOffset EndAt { get; set; }

        // ToDo: Change class type string to ...
        public string MappedRegion { get; set; }

        public string LocationDetail { get; set; }

        [Ignored]
        public string GroupHeader
        {
            get
            {
                return StartAt.ToString("HH:mm");
            }
        }

        public void UpdateLocationDetail()
        {
            LocationDetail = MappedRegion;
        }

        // Dummy
        public List<string> Keywords { get; }
        public string SearchableKeywords { get; }
        public void UpdateSearchableKeywords()
        {
            throw new NotImplementedException();
        }
    }
}
