using System;
using System.Globalization;
using Xamarin.Forms;

namespace TonpeiFes.Forms.Converters
{
    public class T1BooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
            {
                return default(string);
            }
            return ((bool)value) ? "T-1" : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
