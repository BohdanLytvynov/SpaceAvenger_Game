using System;
using System.Globalization;
using System.Windows.Data;

namespace SpaceAvenger.Converters
{
    [ValueConversion(typeof(bool), typeof(string))]
    public class BoolToMaleFemaleInvertedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool maleFemale = (bool)value;

            switch (maleFemale)
            {
                case true:
                    return "Female";
                default:
                    return "Male";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string? str = value as string;
            if (string.IsNullOrEmpty(str))
            {
                return "Fail to Convert to male/female";
            }

            if (str.Equals("Female"))
            {
                return true;
            }
            else if (str.Equals("Male"))
            {
                return false;
            }

            return "Fail to convert!";
        }
    }

}
