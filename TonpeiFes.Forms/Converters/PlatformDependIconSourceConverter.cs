using System;
using System.Globalization;
using Xamarin.Forms;

namespace TonpeiFes.Forms.Converters
{
    public class PlatformDependIconSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string) || value == null)
            {
                return default(string);
            }
            if (Device.RuntimePlatform == Device.iOS)
            {
                return ((string)value).Replace("_", "-");
            }
            else
            {
                return ((string)value).Replace("-", "_");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
