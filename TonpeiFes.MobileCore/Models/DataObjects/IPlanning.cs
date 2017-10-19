using System;
using System.Collections.Generic;
using TonpeiFes.MobileCore.Models.Consts;

namespace TonpeiFes.MobileCore.Models.DataObjects
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

        // always open, this field must empty
        IList<EventDate> OpenDate { get; }

        // Location => 大域(Google Mapに載せるやつ)
        // Headerに載せるやつの中域
        // 展示のみの部屋の詳細
        // これらを連結したやつ
        string LocationDetail { get; }

        void UpdateLocationDetail();
    }
}
