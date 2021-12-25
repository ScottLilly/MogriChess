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
        public bool DisplayValidDestinations { get; set; }

        private Square SelectedSquare
        {
            get => _selectedSquare;
            set
            {
                _selectedSquare = value;

                ValidDestinationsForSelectedPiece.Clear();

                foreach (Square square in Board.Squares)
                {
                    square.IsValidDestination = false;
                }

                if (SelectedSquare != null)
                {
                    List<Move> validDestinations =
                        ValidDestinationsForPieceAt(SelectedSquare.Rank, SelectedSquare.File);

                    foreach (Move move in validDestinations)
                    {
                        ValidDestinationsForSelectedPiece.Add(move);

                        if (DisplayValidDestinations)
                        {
                            SquareAt(move.DestinationRank, move.DestinationFile).IsValidDestination = true;
                        }
                    }
                }
            }
        }

        public Game(Board board)
        {
            Board = board;
        }

        public Piece PieceAt(int rank, int file)
        {
            return SquareAt(rank, file).Piece;
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
            square.PlacePiece(SelectedSquare.Piece);

            // Clear out square the moving piece moved from
            SelectedSquare.Piece = null;
            SelectedSquare.IsSelected = false;
            SelectedSquare = null;

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

        private Square SquareAt(int rank, int file)
        {
            return Board.Squares.First(s => s.Rank.Equals(rank) && s.File.Equals(file));
        }

        public List<Move> ValidDestinationsForPieceAt(int rank, int file)
        {
            Piece piece = PieceAt(rank, file);

            List<Move> validMoves = new List<Move>();

            // Need to handle different directions.
            // Dark pieces face "down", while light pieces face "up".
            int rankForwardMultiplier = piece.ColorType == Enums.ColorType.Light ? 1 : -1;
            int rankBackwardMultiplier = piece.ColorType == Enums.ColorType.Light ? -1 : 1;
            int fileLeftMultiplier = piece.ColorType == Enums.ColorType.Light ? -1 : 1;
            int fileRightMultiplier = piece.ColorType == Enums.ColorType.Light ? 1 : -1;

            validMoves.AddRange(ValidMovesInDirection(piece.SquaresForward, rank, file, rankForwardMultiplier, 0));
            validMoves.AddRange(ValidMovesInDirection(piece.SquaresForwardRight, rank, file, rankForwardMultiplier, fileRightMultiplier));
            validMoves.AddRange(ValidMovesInDirection(piece.SquaresRight, rank, file, 0, fileRightMultiplier));
            validMoves.AddRange(ValidMovesInDirection(piece.SquaresBackRight, rank, file, rankBackwardMultiplier, fileRightMultiplier));
            validMoves.AddRange(ValidMovesInDirection(piece.SquaresBack, rank, file, rankBackwardMultiplier, 0));
            validMoves.AddRange(ValidMovesInDirection(piece.SquaresBackLeft, rank, file, rankBackwardMultiplier, fileLeftMultiplier));
            validMoves.AddRange(ValidMovesInDirection(piece.SquaresLeft, rank, file, 0, fileLeftMultiplier));
            validMoves.AddRange(ValidMovesInDirection(piece.SquaresForwardLeft, rank, file, rankForwardMultiplier, fileLeftMultiplier));

            return validMoves;
        }

        private List<Move> ValidMovesInDirection(int squaresToCheck, int currentRank, int currentFile,
            int rankMultiplier, int fileMultiplier)
        {
            List<Move> validMoves = new List<Move>();

            for (int i = 1; i <= squaresToCheck; i++)
            {
                int destinationRank = currentRank + (i * rankMultiplier);
                int destinationFile = currentFile + (i * fileMultiplier);

                // Off board, stop checking
                if (destinationRank is < 1 or > 8 || destinationFile is < 1 or > 8)
                {
                    break;
                }

                Piece pieceAtDestination = PieceAt(destinationRank, destinationFile);

                if (pieceAtDestination == null)
                {
                    // Square is empty
                    validMoves.Add(new Move(SquareAt(currentRank, currentFile), SquareAt(destinationRank, destinationFile)));
                }
                else if (pieceAtDestination.ColorType != PieceAt(currentRank, currentFile).ColorType)
                {
                    // Square is occupied by an opponent's piece
                    Move move = new Move(SquareAt(currentRank, currentFile), SquareAt(destinationRank, destinationFile));

                    move.IsCapturingMove = true;
                    move.IsWinningMove = pieceAtDestination.IsKing;

                    validMoves.Add(move);

                    break;
                }
                else if (pieceAtDestination.ColorType == PieceAt(currentRank, currentFile).ColorType)
                {
                    // Square is occupied by a player's piece
                    break;
                }
            }

            return validMoves;
        }
    }
}