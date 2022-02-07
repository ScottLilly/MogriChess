using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MogriChess.WPF.CustomConverters
{
    internal class DisplayFirstEllipseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Visibility.Collapsed;
            }

            if (int.TryParse(value.ToString(), out int movementSquares))
            {
                if (movementSquares is 1 or 2)
                {
                    return Visibility.Visible;
                }
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}