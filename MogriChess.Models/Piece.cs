using System;
using System.ComponentModel;

namespace MogriChess.Models
{
    public class Piece : INotifyPropertyChanged
    {
        private readonly Enums.PieceType _pieceType;
        private readonly bool _hasReceivedPawnPromotion;

        public event PropertyChangedEventHandler PropertyChanged;

        public Enums.PlayerColor Color { get; }

        public int SquaresForward { get; private set; }
        public int SquaresForwardRight { get; private set; }
        public int SquaresRight { get; private set; }
        public int SquaresBackRight { get; private set; }
        public int SquaresBack { get; private set; }
        public int SquaresBackLeft { get; private set; }
        public int SquaresLeft { get; private set; }
        public int SquaresForwardLeft { get; private set; }

        public bool IsKing => _pieceType.Equals(Enums.PieceType.King);

        public string UiColor => Color == Enums.PlayerColor.Light ? "#FFFFFF" : "#000000";
        public string KingHighlightUiColor =>
            IsKing ? Color == Enums.PlayerColor.Light ? "#000000" : "#FFFFFF" : UiColor;

        public int TransformAngle => Color == Enums.PlayerColor.Light ? 0 : 180;

        public Piece(Enums.PlayerColor color, Enums.PieceType type,
            int squaresForward, int squaresForwardRight,
            int squaresRight, int squaresBackRight,
            int squaresBack, int squaresBackLeft,
            int squaresLeft, int squaresForwardLeft)
        {
            Color = color;

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