using System.Collections.Generic;
using System.Linq;

namespace MogriChess.Models
{
    public class BotPlayer
    {
        public Enums.ColorType ColorType { get; }

        public BotPlayer(Enums.ColorType colorType)
        {
            ColorType = colorType;
        }

        public Move FindBestMove(Board board)
        {
            List<Move> potentialMoves = new List<Move>();

            var squaresWithBotPlayerPieces =
                board.Squares
                    .Where(s => s.Piece?.ColorType == ColorType);

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

            // TODO: Add code to decide best move
            return validMoves.FirstOrDefault();
        }
    }
}