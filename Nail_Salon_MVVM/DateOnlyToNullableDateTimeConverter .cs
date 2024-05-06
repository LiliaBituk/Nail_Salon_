using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Nail_Salon_MVVM
{
    public class DateOnlyToNullableDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateOnly date)
            {
                return new DateTime(date.Year, date.Month, date.Day);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                return new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
            }

            return null;
        }
    }
}
