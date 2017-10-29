using System;
namespace TonpeiFes.Core.Models.Consts
{
    [Flags]
    public enum EventDateEnum
    {
        DAY1 = 1,
        DAY2 = 1 << 1,
        DAY3 = 1 << 2,
    }

    public static class EventDateEnumExtension
    {
        public static string GetFormattedString(this EventDateEnum days)
        {
            var formattedText = "";
            if (days.HasFlag(EventDateEnum.DAY1)) formattedText += "11/3";
            if (days.HasFlag(EventDateEnum.DAY2)) formattedText += (!string.IsNullOrEmpty(formattedText) ? ", " : "") + "11/4";
            if (days.HasFlag(EventDateEnum.DAY3)) formattedText += (!string.IsNullOrEmpty(formattedText) ? ", " : "") + "11/5";

            return formattedText;
        }

        public static bool IsAll(this EventDateEnum days)
        {
            return days.HasFlag(EventDateEnum.DAY1)
                       && days.HasFlag(EventDateEnum.DAY2)
                       && days.HasFlag(EventDateEnum.DAY3);
        }
    }
}
