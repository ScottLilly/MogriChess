using System.Collections.Generic;
using System.Linq;
using MogriChess.Engine.Core;

namespace MogriChess.Engine.Models;

/// <summary>
/// Contains core game-rule evaluations that operate on the board and moves,
/// decoupled from any UI concerns.
/// </summary>
public static class GameEngine
{
    /// <summary>
    /// Returns all legal moves for the specified player.
    /// Legal moves are pseudo-legal moves that also ensure the moving side's king remains safe.
    /// </summary>
    public static IEnumerable<Move> GetLegalMovesForPlayer(Board board, Color playerColor)
    {
        List<Move> legalMoves = [];

        foreach (Square square in board.SquaresWithPiecesOfColor(playerColor))
        {
            foreach (Move move in board.GeneratePseudoLegalMovesForPieceAt(square))
            {
                if (board.GetSimulatedMoveResult(move,
                        () => board.IsKingSafe(move.MovingPieceColor)))
                {
                    legalMoves.Add(move);
                }
            }
        }

        return legalMoves;
    }

    /// <summary>
    /// Determines if the specified player is in checkmate.
    /// A player is in checkmate if their king is in check and they have no legal moves.
    /// </summary>
    public static bool IsPlayerInCheckmate(Board board, Color playerColor) =>
        board.SquaresWithPiecesOfColor(playerColor)
            .All(square => board.GeneratePseudoLegalMovesForPieceAt(square)
                .None(move => MoveGetsKingOutOfCheck(board, playerColor, move)));

    private static bool MoveGetsKingOutOfCheck(Board board, Color kingColor, Move potentialMove) =>
        board.GetSimulatedMoveResult(potentialMove,
            () => board.IsKingSafe(kingColor));
}

