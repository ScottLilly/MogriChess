using System;
using System.ComponentModel;

namespace MogriChess.Models
{
    public class Piece : INotifyPropertyChanged
    {
        private ColorScheme _colorScheme;
        private readonly Enums.PieceType _pieceType;

        public event PropertyChangedEventHandler PropertyChanged;

        public Enums.ColorType ColorType { get; }

        public int SquaresForward { get; private set; }
        public int SquaresForwardRight { get; private set; }
        public int SquaresRight { get; private set; }
        public int SquaresBackRight { get; private set; }
        public int SquaresBack { get; private set; }
        public int SquaresBackLeft { get; private set; }
        public int SquaresLeft { get; private set; }
        public int SquaresForwardLeft { get; private set; }

        public bool IsKing => _pieceType.Equals(Enums.PieceType.King);

        public string UiColor => ColorType == Enums.ColorType.Light ? _colorScheme.LightColor : _colorScheme.DarkColor;
        public string KingHighlightUiColor =>
            IsKing ? ColorType == Enums.ColorType.Light ? _colorScheme.DarkColor : _colorScheme.LightColor : UiColor;

        public int TransformAngle => ColorType == Enums.ColorType.Light ? 0 : 180;

        public bool ForwardOne => SquaresForward is 1 or 2;
        public bool ForwardTwo => SquaresForward == 2;
        public bool ForwardInfinite => SquaresForward > 2;

        public Piece(ColorScheme colorScheme, Enums.ColorType colorType,
            Enums.PieceType type,
            int squaresForward, int squaresForwardRight,
            int squaresRight, int squaresBackRight,
            int squaresBack, int squaresBackLeft,
            int squaresLeft, int squaresForwardLeft)
        {
            _colorScheme = colorScheme;

            ColorType = colorType;

            _pieceType = type;

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

            AddMovementAbilities(capturedPiece.SquaresForward, capturedPiece.SquaresForwardRight,
                capturedPiece.SquaresRight, capturedPiece.SquaresBackRight,
                capturedPiece.SquaresBack, capturedPiece.SquaresBackLeft,
                capturedPiece.SquaresLeft, capturedPiece.SquaresForwardLeft);
        }

        public void Promote()
        {
            if (_pieceType != Enums.PieceType.Pawn)
            {
                return;
            }

            // Pawns that reach opponent's back rank gain ability to move one square in all directions
            AddMovementAbilities(1, 1, 1, 1, 1, 1, 1, 1);
        }

        private void AddMovementAbilities(int squaresForward, int squaresForwardRight,
            int squaresRight, int squaresBackRight,
            int squaresBack, int squaresBackLeft,
            int squaresLeft, int squaresForwardLeft)
        {
            SquaresForward = Math.Max(SquaresForward, squaresForward);
            SquaresForwardRight = Math.Max(SquaresForwardRight, squaresForwardRight);
            SquaresRight = Math.Max(SquaresRight, squaresRight);
            SquaresBackRight = Math.Max(SquaresBackRight, squaresBackRight);
            SquaresBack = Math.Max(SquaresBack, squaresBack);
            SquaresBackLeft = Math.Max(SquaresBackLeft, squaresBackLeft);
            SquaresLeft = Math.Max(SquaresLeft, squaresLeft);
            SquaresForwardLeft = Math.Max(SquaresForwardLeft, squaresForwardLeft);
        }
    }
}