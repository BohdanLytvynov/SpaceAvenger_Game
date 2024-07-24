using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SpaceAvenger.Converters
{
    [ValueConversion(typeof(DateTime), typeof(string))]
    internal class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;

            if (date.Equals(default))
                return (parameter as string)?.ToString() ?? "This is a default DateTime!";

            return date.ToShortDateString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date;

            if (!DateTime.TryParse(value.ToString(), out date))
                return DependencyProperty.UnsetValue;

            return date;
        }
    }
}
