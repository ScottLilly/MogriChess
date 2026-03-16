using MogriChess.Engine.Core;
using System.Collections.Generic;
using System.Linq;

namespace MogriChess.Engine.Models;

public class BotPlayer(Color botColor,
    PieceValueCalculator pieceValueCalculator)
{
    private readonly Color _botColor = botColor;
    private readonly PieceValueCalculator _pieceValueCalculator = pieceValueCalculator;

    public Move FindBestMove(Board board)
    {
        List<Move> legalMoves = board.LegalMovesForPlayer(_botColor);

        if (legalMoves.None())
        {
            return null;
        }

        // 1. If any move immediately checkmates the opponent, prefer it.
        foreach (Move move in legalMoves)
        {
            bool isCheckmateMove = board.GetSimulatedMoveResult(move,
                () => GameEngine.IsPlayerInCheckmate(board, _botColor.OppositeColor()));

            if (isCheckmateMove)
            {
                return move;
            }
        }

        // 2. Otherwise, evaluate all legal moves using a heuristic that considers:
        //    - material advantage (piece values, including captured powers)
        //    - mobility (how many squares our pieces can move to vs opponent)
        //    - king safety / putting the opponent in check
        int currentBestScore = int.MinValue;
        List<Move> bestMoves = [];

        foreach (Move move in legalMoves)
        {
            int score = board.GetSimulatedMoveResult(move, () => EvaluateBoard(board));

            // Small tie-breakers to favor forcing moves.
            if (move.IsCapturingMove)
            {
                score += Weighting.CaptureMoveBonus;
            }

            if (move.IsPromotingMove)
            {
                score += Weighting.PromotionMoveBonus;
            }

            if (score > currentBestScore)
            {
                currentBestScore = score;
                bestMoves.Clear();
                bestMoves.Add(move);
            }
            else if (score == currentBestScore)
            {
                bestMoves.Add(move);
            }
        }

        return bestMoves.Any()
            ? bestMoves.RandomElement()
            : legalMoves.RandomElement();
    }

    private int PiecesValueFor(Board board, Color color) =>
        board.SquaresWithPiecesOfColor(color)
            .Sum(s => _pieceValueCalculator.CalculatePieceValue(s.Piece));

    private int Advantage(Board board) =>
        PiecesValueFor(board, _botColor) -
        PiecesValueFor(board, _botColor.OppositeColor());

    private int Mobility(Board board, Color color) =>
        board.SquaresWithPiecesOfColor(color)
            .Sum(s => board.GeneratePseudoLegalMovesForPieceAt(s).Count);

    private int EvaluateBoard(Board board)
    {
        int material = Advantage(board);

        int myMobility = Mobility(board, _botColor);
        int opponentMobility = Mobility(board, _botColor.OppositeColor());
        int mobilityScore = myMobility - opponentMobility;

        int kingSafetyScore = 0;
        if (!board.IsKingSafe(_botColor))
        {
            kingSafetyScore -= Weighting.KingInCheckPenalty;
        }

        if (!board.IsKingSafe(_botColor.OppositeColor()))
        {
            kingSafetyScore += Weighting.OpponentKingInCheckBonus;
        }

        // Weight material highest, then mobility, then king (already high magnitude).
        return (material * Weighting.MaterialWeight) + (mobilityScore * Weighting.MobilityWeight) + kingSafetyScore;
    }
}