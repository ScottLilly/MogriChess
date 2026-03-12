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
    /// Returns all legal moves for the specified player, taking into account
    /// whether the king can be captured after each move.
    /// </summary>
    public static IEnumerable<Move> GetLegalMovesForPlayer(Board board, Enums.Color playerColor) =>
        board.SquaresWithPiecesOfColor(playerColor)
            .SelectMany(square => board.LegalMovesForPieceAt(square.Rank, square.File))
            .Where(move =>
                board.GetSimulatedMoveResult(move,
                    () => board.KingCannotBeCaptured(move.MovingPieceColor)));

    /// <summary>
    /// Determines if the specified player is in checkmate.
    /// </summary>
    public static bool IsPlayerInCheckmate(Board board, Enums.Color playerColor) =>
        board.SquaresWithPiecesOfColor(playerColor)
            .All(square => board.PotentialMovesForPieceAt(square)
                .None(move => MoveGetsKingOutOfCheck(board, playerColor, move)));

    private static bool MoveGetsKingOutOfCheck(Board board, Enums.Color kingColor, Move potentialMove) =>
        board.GetSimulatedMoveResult(potentialMove,
            () => board.KingCannotBeCaptured(kingColor));
}

