using System;
namespace TonpeiFes.Core.Models.Consts
{
    [Flags]
    public enum MapObjectEnum
    {
        EXHIBITION = 1 << 0,
        STAGE = 1 << 1,
        STALL = 1 << 2,
        TOILET = 1 << 3,
    }
}
