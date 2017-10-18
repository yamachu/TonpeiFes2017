using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace TonpeiFes.Forms.Converters
{
    public class ListEmptyCheckConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is IList || value is IList<object>)) return true;
            var genericListObj = value as IList<object>;
            if (genericListObj != null) return genericListObj.Count == 0;
            var listObj = value as IList;
            if (genericListObj != null) return listObj.Count == 0;
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
