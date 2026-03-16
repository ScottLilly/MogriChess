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

        // 2. Order moves by a cheap 1‑ply evaluation and keep the top N for deeper search.
        var orderedMovesWithScores =
            legalMoves
                .Select(m => new
                {
                    Move = m,
                    Score = board.GetSimulatedMoveResult(m, () => EvaluateBoard(board))
                })
                .OrderByDescending(x => x.Score)
                .ToList();

        int beamWidth = Weighting.RootMoveBeamWidth;
        var candidateMoves =
            orderedMovesWithScores
                .Take(beamWidth)
                .Select(x => x.Move)
                .ToList();

        // 3. Run a shallow minimax with alpha-beta pruning over the candidate moves.
        int bestScore = int.MinValue;
        List<Move> bestMoves = [];

        foreach (Move move in candidateMoves)
        {
            int score = board.GetSimulatedMoveResult(move,
                () => Minimax(board,
                    depth: Weighting.SearchDepth - 1,
                    alpha: int.MinValue + 1,
                    beta: int.MaxValue,
                    isMaximizingPlayer: false,
                    sideToMove: _botColor.OppositeColor()));

            if (score > bestScore)
            {
                bestScore = score;
                bestMoves.Clear();
                bestMoves.Add(move);
            }
            else if (score == bestScore)
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

    private int Minimax(Board board,
        int depth,
        int alpha,
        int beta,
        bool isMaximizingPlayer,
        Color sideToMove)
    {
        if (depth == 0)
        {
            return EvaluateBoard(board);
        }

        List<Move> legalMoves = board.LegalMovesForPlayer(sideToMove);

        if (legalMoves.None())
        {
            return EvaluateBoard(board);
        }

        if (isMaximizingPlayer)
        {
            int value = int.MinValue + 1;

            foreach (Move move in legalMoves)
            {
                int childScore = board.GetSimulatedMoveResult(move,
                    () => Minimax(board,
                        depth - 1,
                        alpha,
                        beta,
                        isMaximizingPlayer: false,
                        sideToMove: sideToMove.OppositeColor()));

                value = System.Math.Max(value, childScore);
                alpha = System.Math.Max(alpha, value);

                if (alpha >= beta)
                {
                    break;
                }
            }

            return value;
        }
        else
        {
            int value = int.MaxValue;

            foreach (Move move in legalMoves)
            {
                int childScore = board.GetSimulatedMoveResult(move,
                    () => Minimax(board,
                        depth - 1,
                        alpha,
                        beta,
                        isMaximizingPlayer: true,
                        sideToMove: sideToMove.OppositeColor()));

                value = System.Math.Min(value, childScore);
                beta = System.Math.Min(beta, value);

                if (beta <= alpha)
                {
                    break;
                }
            }

            return value;
        }
    }

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