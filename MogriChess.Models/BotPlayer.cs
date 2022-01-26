using System.Collections.Generic;
using System.Linq;

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

            // Calculate current board points (bot and opponent)
            int botPiecesValue =
                squaresWithBotPlayerPieces.Select(s => s.Piece)
                    .Sum(p => _pieceValueCalculator.CalculatePieceValue(p));

            var opponentPieceSquares =
                board.Squares
                    .Where(s => s.Piece?.ColorType == ColorType.OpponentColorType())
                    .ToList();
            int opponentPiecesValue =
                opponentPieceSquares.Select(s => s.Piece)
                    .Sum(p => _pieceValueCalculator.CalculatePieceValue(p));

            // Check each move, calculating points after move

            // Select highest point improvement
            // Randomize, if multiple moves have equivalent point improvements

            // TODO: Add code to decide best move
            return validMoves.FirstOrDefault();
        }
    }
}