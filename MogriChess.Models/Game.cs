using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace MogriChess.Models
{
    public class Game : INotifyPropertyChanged
    {
        private Square _selectedSquare;
        public event PropertyChangedEventHandler PropertyChanged;

        public Board Board { get; }
        public Enums.ColorType CurrentPlayerColor { get; private set; } =
            Enums.ColorType.Light;
        public ObservableCollection<Move> MoveHistory { get; } =
            new ObservableCollection<Move>();
        public ObservableCollection<Move> ValidDestinationsForSelectedPiece { get; } =
            new ObservableCollection<Move>();

        public bool DisplayRankFileLabel { get; set; } = true;
        public bool DisplayValidDestinations { get; set; } = true;

        private Square SelectedSquare
        {
            get => _selectedSquare;
            set
            {
                _selectedSquare = value;

                ValidDestinationsForSelectedPiece.Clear();

                Board.ClearValidDestinations();

                if (SelectedSquare != null)
                {
                    List<Move> validDestinations =
                        ValidMovesForPieceAt(SelectedSquare.Rank, SelectedSquare.File);

                    foreach (Move move in validDestinations)
                    {
                        ValidDestinationsForSelectedPiece.Add(move);

                        if (DisplayValidDestinations)
                        {
                            Board.SquareAt(move.DestinationRank, move.DestinationFile).IsValidDestination = true;
                        }
                    }
                }
            }
        }

        public Game(Board board)
        {
            Board = board;
        }

        public void SelectSquare(Square square)
        {
            // No square is currently selected
            if (SelectedSquare == null)
            {
                // No piece is on square, so return
                if (square?.Piece == null)
                {
                    return;
                }

                // There is a piece on the square, and it's the current player's
                if (square.Piece.ColorType == CurrentPlayerColor)
                {
                    SelectedSquare = square;
                    SelectedSquare.IsSelected = true;
                }

                return;
            }

            // If the player selected the currently-selected square, de-select it.
            if (SelectedSquare == square)
            {
                SelectedSquare = null;
                square.IsSelected = false;

                return;
            }

            // Check the destination square is a valid move
            Move move =
                ValidDestinationsForSelectedPiece.FirstOrDefault(d =>
                    d.DestinationRank == square.Rank &&
                    d.DestinationFile == square.File);

            if (move == null)
            {
                return;
            }

            // Perform capture
            if (square.Piece != null)
            {
                SelectedSquare.Piece.AddMovementAbilities(square.Piece);
            }

            // Move piece to new square
            Board.PlacePieceOnSquare(SelectedSquare.Piece, square);

            // Clear out square the moving piece moved from
            SelectedSquare.Piece = null;
            SelectedSquare.IsSelected = false;
            SelectedSquare = null;

            DetermineIfMovePutsOpponentInCheckOrCheckMate(move);

            MoveHistory.Add(move);

            EndCurrentPlayerTurn();
        }

        private void EndCurrentPlayerTurn()
        {
            CurrentPlayerColor =
                CurrentPlayerColor == Enums.ColorType.Light
                    ? Enums.ColorType.Dark
                    : Enums.ColorType.Light;
        }

        private void DetermineIfMovePutsOpponentInCheckOrCheckMate(Move move)
        {
            var nextMoves = ValidMovesForPieceAt(move.DestinationRank, move.DestinationFile);

            if (nextMoves.Any(m => m.IsCapturingMove && m.DestinationSquare.Piece.IsKing))
            {
                move.IsCheckMove = true;

                // TODO: See if King can be taken out of check
                // Use to determine if opponent is in checkmate
            }
        }

        public List<Move> ValidMovesForPieceAt(int rank, int file)
        {
            Square startingSquare = Board.SquareAt(rank, file);
            Piece movingPiece = startingSquare.Piece;

            List<Move> validMoves = new List<Move>();

            // Need to handle different directions.
            // Dark pieces face "down", while light pieces face "up".
            int rankForwardMultiplier = movingPiece.ColorType == Enums.ColorType.Light ? 1 : -1;
            int rankBackwardMultiplier = movingPiece.ColorType == Enums.ColorType.Light ? -1 : 1;
            int fileLeftMultiplier = movingPiece.ColorType == Enums.ColorType.Light ? -1 : 1;
            int fileRightMultiplier = movingPiece.ColorType == Enums.ColorType.Light ? 1 : -1;

            validMoves.AddRange(ValidMovesInDirection(startingSquare, Enums.Direction.Forward, rankForwardMultiplier, 0));
            validMoves.AddRange(ValidMovesInDirection(startingSquare, Enums.Direction.ForwardRight, rankForwardMultiplier, fileRightMultiplier));
            validMoves.AddRange(ValidMovesInDirection(startingSquare, Enums.Direction.Right, 0, fileRightMultiplier));
            validMoves.AddRange(ValidMovesInDirection(startingSquare, Enums.Direction.BackRight, rankBackwardMultiplier, fileRightMultiplier));
            validMoves.AddRange(ValidMovesInDirection(startingSquare, Enums.Direction.Back, rankBackwardMultiplier, 0));
            validMoves.AddRange(ValidMovesInDirection(startingSquare, Enums.Direction.BackLeft, rankBackwardMultiplier, fileLeftMultiplier));
            validMoves.AddRange(ValidMovesInDirection(startingSquare, Enums.Direction.Left, 0, fileLeftMultiplier));
            validMoves.AddRange(ValidMovesInDirection(startingSquare, Enums.Direction.ForwardLeft, rankForwardMultiplier, fileLeftMultiplier));

            return validMoves;
        }

        private List<Move> ValidMovesInDirection(Square startingSquare,
            Enums.Direction direction,
            int rankMultiplier, int fileMultiplier)
        {
            List<Move> validMoves = new List<Move>();

            Piece movingPiece = startingSquare.Piece;

            if (movingPiece == null)
            {
                return validMoves;
            }

            var movementIndicatorForDirection = 
                movingPiece.MovementIndicatorForDirection(direction);

            for (int i = 1; i <= movementIndicatorForDirection.Squares; i++)
            {
                int destinationRank = startingSquare.Rank + (i * rankMultiplier);
                int destinationFile = startingSquare.File + (i * fileMultiplier);

                // Off board, stop checking
                if (destinationRank is < 1 or > 8 || destinationFile is < 1 or > 8)
                {
                    break;
                }

                Square destinationSquare = Board.SquareAt(destinationRank, destinationFile);
                Move potentialMove = new Move(startingSquare, destinationSquare);

                // Un-promoted pawn reached opponent's back rank, and needs to be promoted.
                if (movingPiece.IsUnpromotedPawn &&
                    ((movingPiece.ColorType == Enums.ColorType.Light && destinationRank == 8) ||
                     (movingPiece.ColorType == Enums.ColorType.Dark && destinationRank == 1)))
                {
                    potentialMove.IsPromotingMove = true;
                }

                Piece pieceAtDestination = destinationSquare.Piece;

                if (pieceAtDestination == null)
                {
                    // Square is empty
                    validMoves.Add(potentialMove);
                }
                else if (pieceAtDestination.ColorType != movingPiece.ColorType)
                {
                    // Square is occupied by an opponent's piece
                    potentialMove.IsCapturingMove = true;
                    potentialMove.IsCheckMove = pieceAtDestination.IsKing;

                    validMoves.Add(potentialMove);

                    break;
                }
                else if (pieceAtDestination.ColorType == movingPiece.ColorType)
                {
                    // Square is occupied by a player's piece
                    break;
                }
            }

            return validMoves;
        }
    }
}