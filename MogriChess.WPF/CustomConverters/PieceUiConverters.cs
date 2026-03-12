using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using MogriChess.Engine.Models;

namespace MogriChess.WPF.CustomConverters;

/// <summary>
/// Converts a <see cref="Piece"/> to its primary fill color based on the
/// current piece color scheme and piece color.
/// </summary>
internal class PieceUiColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Piece piece || piece.ColorScheme == null)
        {
            return Brushes.Transparent;
        }

        string colorHex = piece.Color == Enums.Color.Light
            ? piece.ColorScheme.LightColor
            : piece.ColorScheme.DarkColor;

        return (SolidColorBrush)(new BrushConverter().ConvertFromString(colorHex) ?? Brushes.Transparent);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

/// <summary>
/// Converts a <see cref="Piece"/> to the inner king-indicator color. For kings,
/// this is the opposite of the primary piece color; for other pieces it matches
/// the primary piece color.
/// </summary>
internal class PieceKingIndicatorColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Piece piece || piece.ColorScheme == null)
        {
            return Brushes.Transparent;
        }

        bool isKing = piece.PieceType == Enums.PieceType.King;

        string colorHex;

        if (isKing)
        {
            // Kings use the opposite color in the center
            colorHex = piece.Color == Enums.Color.Light
                ? piece.ColorScheme.DarkColor
                : piece.ColorScheme.LightColor;
        }
        else
        {
            colorHex = piece.Color == Enums.Color.Light
                ? piece.ColorScheme.LightColor
                : piece.ColorScheme.DarkColor;
        }

        return (SolidColorBrush)(new BrushConverter().ConvertFromString(colorHex) ?? Brushes.Transparent);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

/// <summary>
/// Converts a piece color to the rotation angle so that "forward" is always
/// visually up for light pieces and down for dark pieces.
/// </summary>
internal class PieceColorTransformAngleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Enums.Color color)
        {
            return color == Enums.Color.Light ? 0.0 : 180.0;
        }

        return 0.0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

