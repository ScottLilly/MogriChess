﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace MogriChess.Models
{
    public class Game : INotifyPropertyChanged
    {
        private Square _selectedSquare;

        #region Private properties

        public Enums.ColorType CurrentPlayerColor { get; set; } =
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
                        ValidMovesForPieceAt(SelectedSquare.Rank, SelectedSquare.File);

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
        public bool DisplayValidDestinations { get; set; } = true;

        public BotPlayer LightPlayerBot { get; set; }
        public BotPlayer DarkPlayerBot { get; set; }

        public event EventHandler OnMoveCompleted; 
        public event EventHandler OnCheckmate;
        public event PropertyChangedEventHandler PropertyChanged;

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

            // Move piece to new square
            Board.PlacePieceOnSquare(SelectedSquare.Piece, square);

            var movingPieceColorType = SelectedSquare.Piece.ColorType;
            var opponentColorType = movingPieceColorType.OpponentColorType();

            // Clear out square the moving piece moved from
            SelectedSquare.Piece = null;
            SelectedSquare.IsSelected = false;
            SelectedSquare = null;

            bool opponentIsInCheck =
                KingCanBeCaptured(opponentColorType);

            // Determine if opponent is in checkmate
            if (opponentIsInCheck)
            {
                move.PutsOpponentInCheck = true;

                bool opponentCanEscape = false;

                // See if they are in checkmate
                foreach (var opponentSquare in Board.Squares.Where(s => s.Piece != null &&
                                                                        s.Piece.ColorType == opponentColorType))
                {
                    foreach (Move potentialMove in ValidMovesForPieceAt(opponentSquare.Rank, opponentSquare.File))
                    {
                        // Clone pieces pre-move
                        var originalMovingPiece = potentialMove.OriginationSquare.Piece.Clone();
                        var destinationPiece = potentialMove.DestinationSquare.Piece?.Clone();

                        // Make the move
                        Board.PlacePieceOnSquare(potentialMove.OriginationSquare.Piece, potentialMove.DestinationSquare);
                        potentialMove.OriginationSquare.Piece = null;

                        // Check if King is still in check
                        bool stillInCheck = KingCanBeCaptured(opponentColorType);

                        // If not, return (after reverting move)
                        potentialMove.OriginationSquare.Piece = originalMovingPiece;
                        potentialMove.DestinationSquare.Piece = destinationPiece;

                        if (!stillInCheck)
                        {
                            opponentCanEscape = true;
                            break;
                        }
                    }

                    if (opponentCanEscape)
                    {
                        break;
                    }
                }

                move.IsCheckmateMove = !opponentCanEscape;
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
            List<Move> potentialMoves = new List<Move>();

            foreach (Square square in
                     Board.Squares.Where(s =>
                         s.Piece?.ColorType.Equals(botPlayer.ColorType) ?? false))
            {
                potentialMoves.AddRange(ValidMovesForPieceAt(square.Rank, square.File));
            }

            var bestMove = botPlayer.FindBestMove(potentialMoves);

            SelectSquare(bestMove.OriginationSquare);
            SelectSquare(bestMove.DestinationSquare);
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

        private bool KingCanBeCaptured(Enums.ColorType playerColor)
        {
            Enums.ColorType attackingPlayerColor = playerColor.OpponentColorType();

            IEnumerable<Square> squaresWithOpponentPiece =
                Board.Squares.Where(s => s.Piece != null &&
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

        public List<Move> ValidMovesForPieceAt(int rank, int file)
        {
            Square startingSquare = Board.SquareAt(rank, file);

            List<Move> validMoves = new List<Move>();

            validMoves.AddRange(ValidMovesInDirection(startingSquare, Enums.Direction.Forward));
            validMoves.AddRange(ValidMovesInDirection(startingSquare, Enums.Direction.ForwardRight));
            validMoves.AddRange(ValidMovesInDirection(startingSquare, Enums.Direction.Right));
            validMoves.AddRange(ValidMovesInDirection(startingSquare, Enums.Direction.BackRight));
            validMoves.AddRange(ValidMovesInDirection(startingSquare, Enums.Direction.Back));
            validMoves.AddRange(ValidMovesInDirection(startingSquare, Enums.Direction.BackLeft));
            validMoves.AddRange(ValidMovesInDirection(startingSquare, Enums.Direction.Left));
            validMoves.AddRange(ValidMovesInDirection(startingSquare, Enums.Direction.ForwardLeft));

            return validMoves;
        }

        private List<Move> ValidMovesInDirection(Square startingSquare, Enums.Direction direction)
        {
            List<Move> validMoves = new List<Move>();

            Piece movingPiece = startingSquare.Piece;

            if (movingPiece == null)
            {
                return validMoves;
            }

            int maxMovementSquareForDirection =
                movingPiece.MaxMovementSquaresForDirection(direction);
            (int rankMultiplier, int fileMultiplier) =
                movingPiece.MovementMultipliersForDirection(direction);

            for (int i = 1; i <= maxMovementSquareForDirection; i++)
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
                    potentialMove.PutsOpponentInCheck = pieceAtDestination.IsKing;

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

        private bool PutsMovingPlayerIntoCheckOrCheckmate(Move move)
        {
            Piece originatingMovePiece = move.OriginationSquare.Piece.Clone();

            Piece movingPiece = move.OriginationSquare.Piece.Clone();
            Piece destinationPiece = move.DestinationSquare.Piece?.Clone();

            // Simulate the move
            move.OriginationSquare.Piece = null;
            Board.PlacePieceOnSquare(movingPiece, move.DestinationSquare);

            // Check if moving player's king can be captured
            bool putsMovingPlayerIntoCheckOrCheckmate =
                KingCanBeCaptured(movingPiece.ColorType);

            // Undo the simulated move
            Board.SquareAt(move.OriginationSquare.Rank, move.OriginationSquare.File).Piece =
                originatingMovePiece;
            Board.SquareAt(move.DestinationRank, move.DestinationFile).Piece =
                destinationPiece;

            return putsMovingPlayerIntoCheckOrCheckmate;
        }
    }
}