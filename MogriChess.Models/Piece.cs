using System;
using System.ComponentModel;

namespace MogriChess.Models;

public class Piece : INotifyPropertyChanged
{
    private ColorScheme _colorScheme;
    private readonly Enums.PieceType _pieceType;
    private bool _isPromoted;

    public event PropertyChangedEventHandler PropertyChanged;

    public Enums.Color Color { get; }

    public int Forward { get; private set; }
    public int ForwardRight { get; private set; }
    public int Right { get; private set; }
    public int BackRight { get; private set; }
    public int Back { get; private set; }
    public int BackLeft { get; private set; }
    public int Left { get; private set; }
    public int ForwardLeft { get; private set; }

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

        AddMovementAbilities(squaresForward, squaresForwardRight,
            squaresRight, squaresBackRight,
            squaresBack, squaresBackLeft,
            squaresLeft, squaresForwardLeft);
    }

    public void CapturePiece(Piece capturedPiece)
    {
        // Kings do not acquire the movement abilities of pieces they capture
        if (IsKing)
        {
            return;
        }

        AddMovementAbilities(capturedPiece.Forward, capturedPiece.ForwardRight,
            capturedPiece.Right, capturedPiece.BackRight,
            capturedPiece.Back, capturedPiece.BackLeft,
            capturedPiece.Left, capturedPiece.ForwardLeft);
    }

    public int MaxMovementSquaresForDirection(Enums.Direction direction)
    {
        return direction switch
        {
            Enums.Direction.Forward => Forward,
            Enums.Direction.ForwardRight => ForwardRight,
            Enums.Direction.Right => Right,
            Enums.Direction.BackRight => BackRight,
            Enums.Direction.Back => Back,
            Enums.Direction.BackLeft => BackLeft,
            Enums.Direction.Left => Left,
            Enums.Direction.ForwardLeft => ForwardLeft,
            _ => throw new InvalidEnumArgumentException(
                "Invalid enum passed to MaxMovementSquaresForDirection() function")
        };
    }

    public (int rankMultiplier, int fileMultiplier) MovementMultipliersForDirection(Enums.Direction direction)
    {
        (int rm, int fm) multipliers = direction switch
        {
            Enums.Direction.Forward => (1, 0),
            Enums.Direction.ForwardRight => (1, 1),
            Enums.Direction.Right => (0, 1),
            Enums.Direction.BackRight => (-1, 1),
            Enums.Direction.Back => (-1, 0),
            Enums.Direction.BackLeft => (-1, -1),
            Enums.Direction.Left => (0, -1),
            Enums.Direction.ForwardLeft => (1, -1),
            _ => throw new InvalidEnumArgumentException(
                "Invalid direction parameter sent to MovementMultipliersForDirection")
        };

        return Color == Enums.Color.Light
            ? multipliers
            : (-multipliers.rm, -multipliers.fm);
    }

    public void Promote()
    {
        // Pawns that reach opponent's back rank gain ability to move one square in all directions
        if (IsUnpromotedPawn)
        {
            AddMovementAbilities(1, 1, 1, 1, 1, 1, 1, 1);

            _isPromoted = true;
        }
    }

    public Piece Clone()
    {
        return new Piece(_colorScheme, Color, _pieceType,
            Forward, ForwardRight,
            Right, BackRight,
            Back, BackLeft,
            Left, ForwardLeft);
    }

    private void AddMovementAbilities(int squaresForward, int squaresForwardRight,
        int squaresRight, int squaresBackRight,
        int squaresBack, int squaresBackLeft,
        int squaresLeft, int squaresForwardLeft)
    {
        Forward = Math.Max(Forward, squaresForward);
        ForwardRight = Math.Max(ForwardRight, squaresForwardRight);
        Right = Math.Max(Right, squaresRight);
        BackRight = Math.Max(BackRight, squaresBackRight);
        Back = Math.Max(Back, squaresBack);
        BackLeft = Math.Max(BackLeft, squaresBackLeft);
        Left = Math.Max(Left, squaresLeft);
        ForwardLeft = Math.Max(ForwardLeft, squaresForwardLeft);
    }
}