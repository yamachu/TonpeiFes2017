using System;
using TonpeiFes.MobileCore.Configurations;

namespace TonpeiFes.Forms.Configurations
{
    public class MapAssociated : IMapAssociated
    {
        public double MapCenterLangitude { get; } = 38.260504;

        public double MapCenterLongitude { get; } = 140.8524148;

        public string HexColorExhibition { get; } = "#8DCF3F";

        public string HexColorStageEvent { get; } = "#FDC44F";

        public string HexColorStall { get; } = "#33BFDB";
    }
}
