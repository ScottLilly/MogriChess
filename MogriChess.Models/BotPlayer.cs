using System.Collections.Generic;
using System.Linq;
using MogriChess.Core;

namespace MogriChess.Models;

public class BotPlayer
{
    private readonly Enums.Color _botColor;
    private readonly PieceValueCalculator _pieceValueCalculator;

    public BotPlayer(Enums.Color botColor,
        PieceValueCalculator pieceValueCalculator)
    {
        _botColor = botColor;
        _pieceValueCalculator = pieceValueCalculator;
    }

    public Move FindBestMove(Board board)
    {
        List<Move> validMoves = board.ValidMovesForPlayerColor(_botColor);

        // If bot can put opponent in checkmate, do that
        if (validMoves.Any(m => m.PutsOpponentInCheckmate))
        {
            return validMoves.First(m => m.PutsOpponentInCheckmate);
        }

        int currentBestMoveAdvantage = int.MinValue;
        List<Move> bestMoves = new List<Move>();

        // Calculate piece values differences after each capturing move
        foreach (Move move in validMoves.Where(m => m.IsCapturingMove))
        {
            int postMoveAdvantage =
                board.GetSimulatedMoveResult(move, () => Advantage(board));

            // If this move is the best move (or tied with the best)
            // Add it to the "bestMoves" list.
            if (postMoveAdvantage > currentBestMoveAdvantage)
            {
                currentBestMoveAdvantage = postMoveAdvantage;
                bestMoves.Clear();
                bestMoves.Add(move);
            }
            else if (postMoveAdvantage == currentBestMoveAdvantage)
            {
                bestMoves.Add(move);
            }
        }

        // Select highest point improvement
        return bestMoves.Any()
            ? bestMoves.RandomElement()
            : validMoves.RandomElement();
    }

    private int PiecesValueFor(Board board, Enums.Color color) =>
        board.SquaresWithPiecesOfColor(color)
            .Sum(s => _pieceValueCalculator.CalculatePieceValue(s.Piece));

    private int Advantage(Board board) =>
        PiecesValueFor(board, _botColor) -
        PiecesValueFor(board, _botColor.OppositeColor());
}