using System.Collections.Generic;
using System.Linq;
using MogriChess.Core;

namespace MogriChess.Models
{
    public class BotPlayer
    {
        private readonly PieceValueCalculator _pieceValueCalculator;

        public Enums.ColorType ColorType { get; }

        public BotPlayer(Enums.ColorType colorType,
            PieceValueCalculator pieceValueCalculator)
        {
            ColorType = colorType;

            _pieceValueCalculator = pieceValueCalculator;
        }

        public Move FindBestMove(Board board)
        {
            List<Move> potentialMoves = new List<Move>();

            var squaresWithBotPlayerPieces =
                board.Squares
                    .Where(s => s.Piece?.ColorType == ColorType)
                    .ToList();

            foreach (Square square in squaresWithBotPlayerPieces)
            {
                potentialMoves.AddRange(board.ValidMovesForPieceAt(square.Rank, square.File));
            }

            List<Move> validMoves = new List<Move>();

            if (board.KingCanBeCaptured(ColorType))
            {
                foreach (Move potentialMove in potentialMoves)
                {
                    if (board.MoveGetsKingOutOfCheck(ColorType, potentialMove))
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

            // Pre-move advantage
            int botAdvantage = Advantage(board);

            // Check each move, calculating points after move
            int bestMoveAdvantage = int.MinValue;
            List<Move> bestMoves = new List<Move>();

            foreach (Move move in validMoves.Where(m => m.IsCapturingMove))
            {
                // Simulate move
                // Clone pieces pre-move
                var originalMovingPiece = move.OriginationSquare.Piece.Clone();
                var destinationPiece = move.DestinationSquare.Piece?.Clone();

                // Make simulated move
                board.MovePiece(move.OriginationSquare, move.DestinationSquare);

                // Post-move advantage
                int postMoveAdvantage = Advantage(board);

                if (postMoveAdvantage > bestMoveAdvantage)
                {
                    bestMoveAdvantage = postMoveAdvantage;
                    bestMoves.Clear();
                    bestMoves.Add(move);
                }
                else if(postMoveAdvantage == bestMoveAdvantage)
                {
                    bestMoves.Add(move);
                }

                // Revert simulated move
                move.OriginationSquare.Piece = originalMovingPiece;
                move.DestinationSquare.Piece = destinationPiece;
            }

            // Select highest point improvement
            if (bestMoves.Any())
            {
                return bestMoves.RandomElement();
            }

            return validMoves.RandomElement();
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
            return PiecesValueFor(board, ColorType) -
                   PiecesValueFor(board, ColorType.OpponentColorType());
        }
    }
}