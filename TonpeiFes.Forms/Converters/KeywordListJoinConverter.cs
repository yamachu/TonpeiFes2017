using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace TonpeiFes.Forms.Converters
{
    public class KeywordListJoinConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is IList<string>))
            {
                return default(string);
            }
            return ((IList<string>)value)?.Aggregate((acc, next) => $"{acc}, {next}");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
