using System.Collections.Generic;
using System.Linq;
using MogriChess.Core;

namespace MogriChess.Models
{
    public class BotPlayer
    {
        private readonly Enums.ColorType _botColor;
        private readonly PieceValueCalculator _pieceValueCalculator;

        public BotPlayer(Enums.ColorType botColor,
            PieceValueCalculator pieceValueCalculator)
        {
            _botColor = botColor;
            _pieceValueCalculator = pieceValueCalculator;
        }

        public Move FindBestMove(Board board)
        {
            List<Move> potentialMoves = new List<Move>();

            var squaresWithBotPlayerPieces =
                board.Squares
                    .Where(s => s.Piece?.ColorType == _botColor)
                    .ToList();

            foreach (Square square in squaresWithBotPlayerPieces)
            {
                potentialMoves.AddRange(board.ValidMovesForPieceAt(square.Rank, square.File));
            }

            List<Move> validMoves = new List<Move>();

            if (board.KingCanBeCaptured(_botColor))
            {
                foreach (Move potentialMove in potentialMoves)
                {
                    if (board.MoveGetsKingOutOfCheck(_botColor, potentialMove))
                    {
                        validMoves.Add(potentialMove);
                    }
                }
            }
            else
            {
                validMoves.AddRange(potentialMoves);
            }

            // If bot can put opponent in checkmate, do that
            if (validMoves.Any(m => m.IsCheckmateMove))
            {
                return validMoves.First(m => m.IsCheckmateMove);
            }

            // Check each move, calculating points after move
            int currentBestMoveAdvantage = int.MinValue;
            List<Move> bestMoves = new List<Move>();

            foreach (Move move in validMoves.Where(m => m.IsCapturingMove))
            {
                // Clone pieces pre-move
                var originalMovingPiece = move.OriginationSquare.Piece.Clone();
                var destinationPiece = move.DestinationSquare.Piece?.Clone();

                // Make simulated move
                board.MovePiece(move.OriginationSquare, move.DestinationSquare);

                int postMoveAdvantage = Advantage(board);

                // If this move is the best move (or tied with the best)
                // Add it to the "bestMoves" list.
                if (postMoveAdvantage > currentBestMoveAdvantage)
                {
                    currentBestMoveAdvantage = postMoveAdvantage;
                    bestMoves.Clear();
                    bestMoves.Add(move);
                }
                else if(postMoveAdvantage == currentBestMoveAdvantage)
                {
                    bestMoves.Add(move);
                }

                // Revert simulated move
                move.OriginationSquare.Piece = originalMovingPiece;
                move.DestinationSquare.Piece = destinationPiece;
            }

            // Select highest point improvement
            return bestMoves.Any()
                ? bestMoves.RandomElement()
                : validMoves.RandomElement();
        }

        private int PiecesValueFor(Board board, Enums.ColorType colorType)
        {
            return
                board.Squares
                    .Where(s => s.Piece?.ColorType == colorType)
                    .Sum(s => _pieceValueCalculator.CalculatePieceValue(s.Piece));
        }

        private int Advantage(Board board)
        {
            return PiecesValueFor(board, _botColor) -
                   PiecesValueFor(board, _botColor.OpponentColorType());
        }
    }
}