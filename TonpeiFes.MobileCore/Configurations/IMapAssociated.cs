using System;
namespace TonpeiFes.MobileCore.Configurations
{
    public interface IMapAssociated
    {
        double MapCenterLangitude { get; }
        double MapCenterLongitude { get; }

        string HexColorExhibition { get; }
        string HexColorStageEvent { get; }
        string HexColorStall { get; }
    }
}
