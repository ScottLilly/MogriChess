using System;
using System.ComponentModel;

namespace MogriChess.Models;

public class Piece : INotifyPropertyChanged
{
    public ColorScheme ColorScheme { get; }
    public Enums.PieceType PieceType { get; }
    public bool IsPromoted { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    public Enums.Color Color { get; }

    public int Forward { get; set; }
    public int ForwardRight { get; set; }
    public int Right { get; set; }
    public int BackRight { get; set; }
    public int Back { get; set; }
    public int BackLeft { get; set; }
    public int Left { get; set; }
    public int ForwardLeft { get; set; }

    public bool IsKing =>
        PieceType == Enums.PieceType.King;
    public bool IsUnpromotedPawn =>
        PieceType == Enums.PieceType.Pawn && !IsPromoted;

    public string UiColor =>
        Color == Enums.Color.Light
            ? ColorScheme.LightColor
            : ColorScheme.DarkColor;
    public string KingIndicatorUiColor =>
        IsKing
            ? Color == Enums.Color.Light
                ? ColorScheme.DarkColor
                : ColorScheme.LightColor
            : UiColor;
    public int PieceColorTransformAngle =>
        Color == Enums.Color.Light ? 0 : 180;

    public Piece(ColorScheme colorScheme, Enums.Color color, Enums.PieceType type,
        int squaresForward, int squaresForwardRight,
        int squaresRight, int squaresBackRight,
        int squaresBack, int squaresBackLeft,
        int squaresLeft, int squaresForwardLeft)
    {
        ColorScheme = colorScheme;
        PieceType = type;

        Color = color;

        Forward = squaresForward;
        ForwardRight = squaresForwardRight;
        Right = squaresRight;
        BackRight = squaresBackRight;
        Back = squaresBack;
        BackLeft = squaresBackLeft;
        Left = squaresLeft;
        ForwardLeft = squaresForwardLeft;
    }
}