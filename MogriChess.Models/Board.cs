using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace MogriChess.Models
{
    public class Board : INotifyPropertyChanged
    {
        private Enums.ColorType _squareColorType;

        public ColorScheme BoardColorScheme { get; }
        public ColorScheme PieceColorScheme { get; }

        public ObservableCollection<Square> Squares { get; } =
            new ObservableCollection<Square>();

        public event PropertyChangedEventHandler PropertyChanged;

        public Board(ColorScheme boardColorScheme, ColorScheme piecesColorScheme,
            List<PiecePlacement> piecePlacements)
        {
            BoardColorScheme = boardColorScheme;
            PieceColorScheme = piecesColorScheme;

            PopulateBoardWithSquares();

            foreach (PiecePlacement placement in piecePlacements)
            {
                PlacePieceOnSquare(placement.Piece, SquareAt(placement.Rank, placement.File));
            }
        }

        public Piece PieceAt(int rank, int file)
        {
            return SquareAt(rank, file).Piece;
        }

        public void MovePiece(Square originationSquare, Square destinationSquare)
        {
            PlacePieceOnSquare(originationSquare.Piece, destinationSquare);
            originationSquare.Piece = null;
        }

        internal void ClearValidDestinations()
        {
            foreach (Square square in Squares)
            {
                square.IsValidDestination = false;
            }
        }

        internal Square SquareAt(int rank, int file)
        {
            return Squares.First(s => s.Rank.Equals(rank) && s.File.Equals(file));
        }

        private void PopulateBoardWithSquares()
        {
            for (int rank = 1; rank <= Constants.NumberOfRanks; rank++)
            {
                _squareColorType = rank % 2 == 0 ? Enums.ColorType.Light : Enums.ColorType.Dark;

                for (int file = 1; file <= Constants.NumberOfFiles; file++)
                {
                    Squares.Add(new Square(rank, file, BoardColorScheme, GetCurrentSquareColor()));
                }
            }
        }

        private Enums.ColorType GetCurrentSquareColor()
        {
            Enums.ColorType currentSquareColorType = _squareColorType;

            // Switch to next color
            _squareColorType =
                _squareColorType ==
                Enums.ColorType.Light ? Enums.ColorType.Dark : Enums.ColorType.Light;

            return currentSquareColorType;
        }

        private void PlacePieceOnSquare(Piece piece, Square destinationSquare)
        {
            // Perform capture
            if (destinationSquare.Piece != null)
            {
                piece.AddMovementAbilities(destinationSquare.Piece);
            }

            destinationSquare.Piece = piece;

            if (!piece.IsPawn)
            {
                return;
            }

            if ((piece.ColorType == Enums.ColorType.Light && destinationSquare.Rank == Constants.BackRankDark) ||
                (piece.ColorType == Enums.ColorType.Dark && destinationSquare.Rank == Constants.BackRankLight))
            {
                piece.Promote();
            }
        }
    }
}