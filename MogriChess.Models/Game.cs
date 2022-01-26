using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace MogriChess.Models
{
    public class Game : INotifyPropertyChanged
    {
        private Square _selectedSquare;
        private bool _displayValidDestinations = true;

        #region Private properties

        public Enums.ColorType CurrentPlayerColor { get; private set; } =
            Enums.ColorType.Light;

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
                        Board.ValidMovesForPieceAt(SelectedSquare.Rank, SelectedSquare.File);

                    foreach (Move move in validDestinations.Where(m => 
                                 !PutsMovingPlayerIntoCheckOrCheckmate(m)))
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

        private ObservableCollection<Move> ValidDestinationsForSelectedPiece { get; } =
            new ObservableCollection<Move>();

        #endregion

        public Board Board { get; }
        public ObservableCollection<Move> MoveHistory { get; } =
            new ObservableCollection<Move>();
        public bool DisplayRankFileLabel { get; set; } = true;

        public bool DisplayValidDestinations
        {
            get => _displayValidDestinations;
            set
            {
                _displayValidDestinations = value;

                if (!_displayValidDestinations)
                {
                    Board.ClearValidDestinations();
                }
            }
        }

        public BotPlayer LightPlayerBot { get; set; }
        public BotPlayer DarkPlayerBot { get; set; }

        public event EventHandler OnMoveCompleted; 
        public event EventHandler OnCheckmate;
        public event PropertyChangedEventHandler PropertyChanged;

        public Game(Board board)
        {
            Board = board;
        }

        #region Public methods

        public void StartGame()
        {
            if (SelectedSquare != null)
            {
                SelectedSquare.IsSelected = false;
                SelectedSquare = null;
            }

            Board.ClearValidDestinations();

            MoveHistory.Clear();
            CurrentPlayerColor = Enums.ColorType.Light;
        }

        public void SelectSquare(Square square)
        {
            // No square is currently selected
            if (SelectedSquare == null)
            {
                // No piece is on square, so return
                if (square.IsEmpty)
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
                DeselectSelectedSquare();

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

            var movingPieceColorType = SelectedSquare.Piece.ColorType;
            var opponentColorType = movingPieceColorType.OpponentColorType();

            // Move piece to new square
            Board.MovePiece(SelectedSquare, square);

            DeselectSelectedSquare();

            bool opponentIsInCheck =
                Board.KingCanBeCaptured(opponentColorType);

            // Determine if opponent is in checkmate
            if (opponentIsInCheck)
            {
                move.PutsOpponentInCheck = true;

                move.IsCheckmateMove = OpponentIsInCheckmate(opponentColorType);
            }

            MoveHistory.Add(move);

            EndCurrentPlayerTurn();

            if (move.IsCheckmateMove)
            {
                HandleCheckmate();
            }
        }

        public void MakeBotMove(BotPlayer botPlayer)
        {
            if (MoveHistory.Last().IsCheckmateMove)
            {
                return;
            }

            Move bestMove = botPlayer.FindBestMove(Board);

            SelectSquare(bestMove.OriginationSquare);
            SelectSquare(bestMove.DestinationSquare);
        }

        #endregion

        #region Private methods

        private void DeselectSelectedSquare()
        {
            SelectedSquare.IsSelected = false;
            SelectedSquare = null;
        }

        private void HandleCheckmate()
        {
            OnCheckmate?.Invoke(this, EventArgs.Empty);
        }

        private void EndCurrentPlayerTurn()
        {
            CurrentPlayerColor = CurrentPlayerColor.OpponentColorType();
            OnMoveCompleted?.Invoke(this, EventArgs.Empty);
        }

        private bool PutsMovingPlayerIntoCheckOrCheckmate(Move move)
        {
            Piece originatingMovePiece = move.OriginationSquare.Piece.Clone();

            Piece movingPiece = move.OriginationSquare.Piece.Clone();
            Piece destinationPiece = move.DestinationSquare.Piece?.Clone();

            // Simulate the move
            Board.MovePiece(move.OriginationSquare, move.DestinationSquare);

            // Check if moving player's king can be captured
            bool putsMovingPlayerIntoCheckOrCheckmate =
                Board.KingCanBeCaptured(movingPiece.ColorType);

            // Revert the simulated move
            Board.SquareAt(move.OriginationSquare.Rank, move.OriginationSquare.File).Piece =
                originatingMovePiece;
            Board.SquareAt(move.DestinationRank, move.DestinationFile).Piece =
                destinationPiece;

            return putsMovingPlayerIntoCheckOrCheckmate;
        }

        private bool OpponentIsInCheckmate(Enums.ColorType opponentColorType)
        {
            bool isInCheckmate = true;

            // See if they are in checkmate
            foreach (var opponentSquare in Board.Squares.Where(s => s.Piece != null &&
                                                                    s.Piece.ColorType == opponentColorType))
            {
                foreach (Move potentialMove in Board.ValidMovesForPieceAt(opponentSquare.Rank, opponentSquare.File))
                {
                    if (Board.MoveGetsKingOutOfCheck(opponentColorType, potentialMove))
                    {
                        isInCheckmate = false;
                        break;
                    }
                }

                if (!isInCheckmate)
                {
                    break;
                }
            }

            return isInCheckmate;
        }

        #endregion
    }
}