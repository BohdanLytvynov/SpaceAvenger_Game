using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace WPF.UI.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool hiddenCollapsed = (bool)parameter;
            bool v = (bool)value;
            if (v) 
                return Visibility.Visible;
            else
            { 
                if(hiddenCollapsed)
                    return Visibility.Hidden;
                else 
                    return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;

            switch (visibility)
            {
                case Visibility.Visible:
                    return true;
                case Visibility.Hidden:                    
                case Visibility.Collapsed:
                    return false;               
            }

            return DependencyProperty.UnsetValue;
        }
    }
}
