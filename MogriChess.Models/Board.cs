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

        public Board(ColorScheme boardColorScheme, ColorScheme piecesColorScheme)
        {
            BoardColorScheme = boardColorScheme;
            PieceColorScheme = piecesColorScheme;

            PopulateBoardWithSquares();
        }

        #region Public methods

        public void PlaceStartingPieces(List<PiecePlacement> piecePlacements)
        {
            // Clear out old pieces
            foreach (Square square in Squares)
            {
                square.Piece = null;
            }

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

        public List<Move> ValidMovesForPieceAt(int rank, int file)
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

        public bool KingCanBeCaptured(Enums.ColorType playerColor)
        {
            Enums.ColorType attackingPlayerColor = playerColor.OpponentColorType();

            IEnumerable<Square> squaresWithOpponentPiece =
                Squares.Where(s => s.Piece != null &&
                                         s.Piece.ColorType == attackingPlayerColor);

            foreach (Square square in squaresWithOpponentPiece)
            {
                if (ValidMovesForPieceAt(square.Rank, square.File).Any(m => m.PutsOpponentInCheck))
                {
                    return true;
                }
            }

            return false;
        }

        public bool MoveGetsKingOutOfCheck(Enums.ColorType kingColor, Move potentialMove)
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

        #endregion

        #region Private methods

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

        private static void PlacePieceOnSquare(Piece piece, Square destinationSquare)
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
                    IsPawnPromotionMove(potentialMove);

                Piece destinationPiece = destinationSquare.Piece;

                if (destinationSquare.IsEmpty)
                {
                    validMoves.Add(potentialMove);
                }
                else
                {
                    if (destinationSquare.Piece.ColorType != movingPiece.ColorType)
                    {
                        // Square is occupied by an opponent's piece
                        potentialMove.IsCapturingMove = true;
                        potentialMove.PutsOpponentInCheck = destinationPiece.IsKing;

                        validMoves.Add(potentialMove);
                    }

                    break;
                }
            }

            return validMoves;
        }

        private static bool IsPawnPromotionMove(Move potentialMove)
        {
            Piece piece = potentialMove.OriginationSquare.Piece;
            int destinationRank = potentialMove.DestinationRank;

            return piece.IsUnpromotedPawn &&
                   (piece.ColorType == Enums.ColorType.Light && destinationRank == Constants.BackRankDark ||
                    piece.ColorType == Enums.ColorType.Dark && destinationRank == Constants.BackRankLight);
        }

        #endregion
    }
}