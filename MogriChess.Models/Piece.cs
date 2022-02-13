using System;
using System.ComponentModel;

namespace MogriChess.Models;

public class Piece : INotifyPropertyChanged
{
    public ColorScheme _colorScheme;
    public readonly Enums.PieceType _pieceType;
    public bool _isPromoted;

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
        _pieceType == Enums.PieceType.King;
    public bool IsUnpromotedPawn =>
        _pieceType == Enums.PieceType.Pawn && !_isPromoted;

    public string UiColor =>
        Color == Enums.Color.Light
            ? _colorScheme.LightColor
            : _colorScheme.DarkColor;
    public string KingIndicatorUiColor =>
        IsKing
            ? Color == Enums.Color.Light
                ? _colorScheme.DarkColor
                : _colorScheme.LightColor
            : UiColor;
    public int PieceColorTransformAngle =>
        Color == Enums.Color.Light ? 0 : 180;

    public Piece(ColorScheme colorScheme, Enums.Color color, Enums.PieceType type,
        int squaresForward, int squaresForwardRight,
        int squaresRight, int squaresBackRight,
        int squaresBack, int squaresBackLeft,
        int squaresLeft, int squaresForwardLeft)
    {
        _colorScheme = colorScheme;
        _pieceType = type;

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

    public Piece Clone()
    {
        return new Piece(_colorScheme, Color, _pieceType,
            Forward, ForwardRight,
            Right, BackRight,
            Back, BackLeft,
            Left, ForwardLeft);
    }
}