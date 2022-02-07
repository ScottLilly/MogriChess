using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using MogriChess.Models;

namespace MogriChess.WPF.CustomConverters
{
    internal class DisplayRectangleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return Visibility.Collapsed;
            }

            if (int.TryParse(value.ToString(), out int movementSquares))
            {
                if (movementSquares == Constants.UnlimitedMoves)
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