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
        IList<IDescription> Descriptions { get; }
        PlanningTypeEnum PlanningType { get; }
        // Icon

        SNSInformations SNS { get; }

        bool IsT1 { get; }
        bool IsAcademic { get; }

        EventDateEnum OpenDate { get; }
        string OpenDateDetail { get; }

        // Location => 大域(Google Mapに載せるやつ)
        // Headerに載せるやつの中域
        // 展示のみの部屋の詳細
        // これらを連結したやつ
        string LocationDetail { get; }

        void UpdateLocationDetail();
    }
}
