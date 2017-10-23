using System;
using System.Collections.Generic;
using TonpeiFes.Core.Models.Consts;

namespace TonpeiFes.Core.Models.DataObjects
{
    public interface IPlanning
    {
        string Id { get; }

        string Title { get; }
        string Owner { get; }
        IList<IDescriptionImpl> Descriptions { get; }
        PlanningTypeEnum PlanningType { get; }
        // Icon

        SNSInformations SNS { get; }

        bool IsT1 { get; }
        bool IsAcademic { get; }

        EventDateEnum OpenDate { get; }
        string OpenDateDetail { get; }

        MapRegion MappedRegion { get; }
        MyGroupHeader HeaderGroupedRegion { get; }
        string LocationDetail { get; }

        void UpdateLocationDetail();
    }
}
