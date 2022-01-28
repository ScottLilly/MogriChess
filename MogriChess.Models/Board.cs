using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using MogriChess.Core;

namespace MogriChess.Models
{
    public class Board : INotifyPropertyChanged
    {
        public ColorScheme BoardColorScheme { get; }
        public ColorScheme PieceColorScheme { get; }

        public ObservableCollection<Square> Squares { get; } =
            new ObservableCollection<Square>();

        public event PropertyChangedEventHandler PropertyChanged;

        public Board(ColorScheme boardColorScheme, ColorScheme piecesColorScheme)
        {
            BoardColorScheme = boardColorScheme;
            PieceColorScheme = piecesColorScheme;

            PopulateBoardWithSquares();
        }

        #region Public methods

        public void PlaceStartingPieces(List<PiecePlacement> piecePlacements)
        {
            RemovePiecesFromAllSquares();

            foreach (PiecePlacement placement in piecePlacements)
            {
                PlacePieceOnSquare(placement.Piece, SquareAt(placement.Rank, placement.File));
            }
        }

        public Piece PieceAt(int rank, int file) =>
            SquareAt(rank, file).Piece;

        public void MovePiece(Square originationSquare, Square destinationSquare)
        {
            PlacePieceOnSquare(originationSquare.Piece, destinationSquare);
            originationSquare.Piece = null;
        }

        public List<Move> ValidMovesForPlayerColor(Enums.Color playerColor)
        {
            List<Move> potentialMoves = new List<Move>();

            foreach (Square square in SquaresWithPiecesOfColor(playerColor))
            {
                potentialMoves.AddRange(PotentialMovesForPieceAt(square.Rank, square.File));
            }

            // Only return moves that do not put moving player in check
            List<Move> validMoves = new List<Move>();

            if (KingCanBeCaptured(playerColor))
            {
                foreach (Move potentialMove in potentialMoves)
                {
                    if (MoveGetsKingOutOfCheck(playerColor, potentialMove))
                    {
                        validMoves.Add(potentialMove);
                    }
                }
            }
            else
            {
                validMoves.AddRange(potentialMoves);
            }

            return validMoves;
        }

        public List<Move> PotentialMovesForPieceAt(int rank, int file)
        {
            Square originationSquare = SquareAt(rank, file);

            List<Move> validMoves = new List<Move>();

            if (originationSquare.Piece == null)
            {
                return validMoves;
            }

            validMoves.AddRange(ValidMovesInDirection(originationSquare, Enums.Direction.Forward));
            validMoves.AddRange(ValidMovesInDirection(originationSquare, Enums.Direction.ForwardRight));
            validMoves.AddRange(ValidMovesInDirection(originationSquare, Enums.Direction.Right));
            validMoves.AddRange(ValidMovesInDirection(originationSquare, Enums.Direction.BackRight));
            validMoves.AddRange(ValidMovesInDirection(originationSquare, Enums.Direction.Back));
            validMoves.AddRange(ValidMovesInDirection(originationSquare, Enums.Direction.BackLeft));
            validMoves.AddRange(ValidMovesInDirection(originationSquare, Enums.Direction.Left));
            validMoves.AddRange(ValidMovesInDirection(originationSquare, Enums.Direction.ForwardLeft));

            return validMoves;
        }

        public bool KingCanBeCaptured(Enums.Color playerColor) =>
            SquaresWithPiecesOfColor(playerColor.OppositeColor())
                .Any(square => PotentialMovesForPieceAt(square.Rank, square.File)
                    .Any(m => m.PutsOpponentInCheck));

        public bool MoveGetsKingOutOfCheck(Enums.Color kingColor, Move potentialMove)
        {
            // Clone pieces pre-move
            var originalMovingPiece = potentialMove.OriginationSquare.Piece.Clone();
            var destinationPiece = potentialMove.DestinationSquare.Piece?.Clone();

            // Make simulated move
            MovePiece(potentialMove.OriginationSquare, potentialMove.DestinationSquare);

            bool stillInCheck = KingCanBeCaptured(kingColor);

            // Revert simulated move
            potentialMove.OriginationSquare.Piece = originalMovingPiece;
            potentialMove.DestinationSquare.Piece = destinationPiece;

            return !stillInCheck;
        }

        #endregion

        #region Internal methods

        internal void ClearValidDestinations() =>
            Squares.ApplyToEach(s => s.IsValidDestination = false);

        internal Square SquareAt(int rank, int file) =>
            Squares.First(s => s.Rank.Equals(rank) && s.File.Equals(file));

        #endregion

        #region Private methods

        private void PopulateBoardWithSquares()
        {
            for (int rank = 1; rank <= Constants.NumberOfRanks; rank++)
            {
                for (int file = 1; file <= Constants.NumberOfFiles; file++)
                {
                    Squares.Add(new Square(rank, file, BoardColorScheme));
                }
            }
        }

        private static void PlacePieceOnSquare(Piece piece, Square destinationSquare)
        {
            if (destinationSquare.Piece != null)
            {
                piece.CapturePiece(destinationSquare.Piece);
            }

            destinationSquare.Piece = piece;

            if (IsPawnPromotionMove(piece, destinationSquare))
            {
                piece.Promote();
            }
        }

        private List<Move> ValidMovesInDirection(Square originationSquare, Enums.Direction direction)
        {
            List<Move> validMoves = new List<Move>();

            Piece movingPiece = originationSquare.Piece;

            int maxMovementSquareForDirection =
                movingPiece.MaxMovementSquaresForDirection(direction);
            (int rankMultiplier, int fileMultiplier) =
                movingPiece.MovementMultipliersForDirection(direction);

            for (int i = 1; i <= maxMovementSquareForDirection; i++)
            {
                int destinationRank = originationSquare.Rank + (i * rankMultiplier);
                int destinationFile = originationSquare.File + (i * fileMultiplier);

                // Off board, stop checking
                if (destinationRank is < 1 or > Constants.NumberOfRanks ||
                    destinationFile is < 1 or > Constants.NumberOfFiles)
                {
                    break;
                }

                Square destinationSquare = SquareAt(destinationRank, destinationFile);
                Move potentialMove = new Move(originationSquare, destinationSquare);

                // Un-promoted pawn reached opponent's back rank, and needs to be promoted.
                potentialMove.IsPromotingMove =
                    IsPawnPromotionMove(movingPiece, destinationSquare);

                if (destinationSquare.IsEmpty)
                {
                    validMoves.Add(potentialMove);
                }
                else
                {
                    if (destinationSquare.Piece.Color != movingPiece.Color)
                    {
                        // Square is occupied by an opponent's piece
                        potentialMove.IsCapturingMove = true;
                        potentialMove.PutsOpponentInCheck =
                            destinationSquare.Piece.IsKing;

                        validMoves.Add(potentialMove);
                    }

                    break;
                }
            }

            return validMoves;
        }

        private static bool IsPawnPromotionMove(Piece movingPiece, Square destinationSquare) =>
            movingPiece.IsUnpromotedPawn &&
            (movingPiece.Color == Enums.Color.Light && destinationSquare.Rank == Constants.BackRankDark ||
             movingPiece.Color == Enums.Color.Dark && destinationSquare.Rank == Constants.BackRankLight);

        private IEnumerable<Square> SquaresWithPiecesOfColor(Enums.Color color) =>
            Squares.Where(s => s.Piece?.Color == color);

        private void RemovePiecesFromAllSquares() =>
            Squares.ApplyToEach(s => s.Piece = null);

        #endregion
    }
}