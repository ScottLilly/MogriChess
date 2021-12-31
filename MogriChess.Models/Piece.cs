using System;
using System.ComponentModel;

namespace MogriChess.Models
{
    public class Piece : INotifyPropertyChanged
    {
        private ColorScheme _colorScheme;
        private readonly Enums.PieceType _pieceType;
        private bool _isPromoted;

        public event PropertyChangedEventHandler PropertyChanged;

        public Enums.ColorType ColorType { get; }

        public MovementIndicator Forward { get; } =
            new MovementIndicator(Enums.Direction.Forward);
        public MovementIndicator ForwardRight { get; } =
            new MovementIndicator(Enums.Direction.ForwardRight);
        public MovementIndicator Right { get; } =
            new MovementIndicator(Enums.Direction.Right);
        public MovementIndicator BackRight { get; } =
            new MovementIndicator(Enums.Direction.BackRight);
        public MovementIndicator Back { get; } =
            new MovementIndicator(Enums.Direction.Back);
        public MovementIndicator BackLeft { get; } =
            new MovementIndicator(Enums.Direction.BackLeft);
        public MovementIndicator Left { get; } =
            new MovementIndicator(Enums.Direction.Left);
        public MovementIndicator ForwardLeft { get; } =
            new MovementIndicator(Enums.Direction.ForwardLeft);

        public bool IsKing => _pieceType.Equals(Enums.PieceType.King);
        public bool IsPawn => _pieceType.Equals(Enums.PieceType.Pawn);
        public bool IsUnpromotedPawn => IsPawn && !_isPromoted;

        public string UiColor =>
            ColorType == Enums.ColorType.Light
                ? _colorScheme.LightColor
                : _colorScheme.DarkColor;
        public string KingIndicatorUiColor =>
            IsKing
                ? ColorType == Enums.ColorType.Light
                    ? _colorScheme.DarkColor
                    : _colorScheme.LightColor
                : UiColor;
        public int PieceColorTransformAngle =>
            ColorType == Enums.ColorType.Light ? 0 : 180;

        public Piece(ColorScheme colorScheme, Enums.ColorType colorType,
            Enums.PieceType type,
            int squaresForward, int squaresForwardRight,
            int squaresRight, int squaresBackRight,
            int squaresBack, int squaresBackLeft,
            int squaresLeft, int squaresForwardLeft)
        {
            _colorScheme = colorScheme;
            _pieceType = type;

            ColorType = colorType;

            AddMovementAbilities(squaresForward, squaresForwardRight,
                squaresRight, squaresBackRight,
                squaresBack, squaresBackLeft,
                squaresLeft, squaresForwardLeft);
        }

        public void AddMovementAbilities(Piece capturedPiece)
        {
            // Kings do not acquire the movement abilities of pieces they capture
            if (_pieceType == Enums.PieceType.King)
            {
                return;
            }

            AddMovementAbilities(capturedPiece.Forward.Squares, capturedPiece.ForwardRight.Squares,
                capturedPiece.Right.Squares, capturedPiece.BackRight.Squares,
                capturedPiece.Back.Squares, capturedPiece.BackLeft.Squares,
                capturedPiece.Left.Squares, capturedPiece.ForwardLeft.Squares);
        }

        public MovementIndicator MovementIndicatorForDirection(Enums.Direction direction)
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
                    "Invalid enum passed to MovementIndicatorForDirection() function")
            };
        }

        public void Promote()
        {
            if (_pieceType != Enums.PieceType.Pawn)
            {
                return;
            }

            // Pawns that reach opponent's back rank gain ability to move one square in all directions
            AddMovementAbilities(1, 1, 1, 1, 1, 1, 1, 1);

            _isPromoted = true;
        }

        private void AddMovementAbilities(int squaresForward, int squaresForwardRight,
            int squaresRight, int squaresBackRight,
            int squaresBack, int squaresBackLeft,
            int squaresLeft, int squaresForwardLeft)
        {
            Forward.Squares = Math.Max(Forward.Squares, squaresForward);
            ForwardRight.Squares = Math.Max(ForwardRight.Squares, squaresForwardRight);
            Right.Squares = Math.Max(Right.Squares, squaresRight);
            BackRight.Squares = Math.Max(BackRight.Squares, squaresBackRight);
            Back.Squares = Math.Max(Back.Squares, squaresBack);
            BackLeft.Squares = Math.Max(BackLeft.Squares, squaresBackLeft);
            Left.Squares = Math.Max(Left.Squares, squaresLeft);
            ForwardLeft.Squares = Math.Max(ForwardLeft.Squares, squaresForwardLeft);
        }
    }
}