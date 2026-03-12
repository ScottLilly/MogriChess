using System.Collections.Generic;
using System.ComponentModel;

namespace MogriChess.Engine.Models;

/// <summary>
/// Responsible for generating pseudo-legal moves from a given board position.
/// Pseudo-legal moves respect piece movement and occupancy, but do not consider king safety.
/// </summary>
public static class MoveGenerator
{
    public static List<Move> GeneratePseudoLegalMovesForPieceAt(Board board, Square square) =>
        GeneratePseudoLegalMovesForPieceAt(board, square.SquareShorthand);

    public static List<Move> GeneratePseudoLegalMovesForPieceAt(Board board, string squareShorthand)
    {
        Square originationSquare = board.Squares[squareShorthand];

        List<Move> validMoves = [];

        if (originationSquare.Piece == null)
        {
            return validMoves;
        }

        validMoves.AddRange(PotentialMovesInDirection(board, originationSquare, Direction.Forward));
        validMoves.AddRange(PotentialMovesInDirection(board, originationSquare, Direction.ForwardRight));
        validMoves.AddRange(PotentialMovesInDirection(board, originationSquare, Direction.Right));
        validMoves.AddRange(PotentialMovesInDirection(board, originationSquare, Direction.BackRight));
        validMoves.AddRange(PotentialMovesInDirection(board, originationSquare, Direction.Back));
        validMoves.AddRange(PotentialMovesInDirection(board, originationSquare, Direction.BackLeft));
        validMoves.AddRange(PotentialMovesInDirection(board, originationSquare, Direction.Left));
        validMoves.AddRange(PotentialMovesInDirection(board, originationSquare, Direction.ForwardLeft));

        return validMoves;
    }

    private static List<Move> PotentialMovesInDirection(
        Board board,
        Square originationSquare,
        Direction direction)
    {
        List<Move> potentialMoves = [];

        Piece movingPiece = originationSquare.Piece;

        int maxMovementSquareForDirection =
            MaxMovementSquaresForDirection(movingPiece, direction);
        (int rankMultiplier, int fileMultiplier) =
            MovementMultipliersForDirection(movingPiece, direction);

        for (int i = 1; i <= maxMovementSquareForDirection; i++)
        {
            int destinationRank = originationSquare.Rank + (i * rankMultiplier);
            int destinationFile = originationSquare.File + (i * fileMultiplier);

            // Off board, stop checking
            if (destinationRank is < 1 or > Constants.NumberOfRanks ||
                destinationFile is < 1 or > Constants.NumberOfFiles)
            {
                break;
            }

            Square destinationSquare = board.GetSquareAt(destinationRank, destinationFile);
            Move potentialMove = new(originationSquare, destinationSquare);

            // Un-promoted pawn reached opponent's back rank, and needs to be promoted.
            potentialMove.IsPromotingMove =
                Board.IsPawnPromotionMove(movingPiece, destinationSquare);

            if (destinationSquare.IsEmpty)
            {
                potentialMoves.Add(potentialMove);
            }
            else
            {
                if (destinationSquare.Piece.Color != movingPiece.Color)
                {
                    // Square is occupied by an opponent's piece
                    potentialMove.IsCapturingMove = true;
                    potentialMove.PutsOpponentInCheck =
                        destinationSquare.Piece.IsKing;

                    potentialMoves.Add(potentialMove);
                }

                break;
            }
        }

        return potentialMoves;
    }

    private static int MaxMovementSquaresForDirection(Piece piece, Direction direction)
    {
        return direction switch
        {
            Direction.Forward => piece.Forward,
            Direction.ForwardRight => piece.ForwardRight,
            Direction.Right => piece.Right,
            Direction.BackRight => piece.BackRight,
            Direction.Back => piece.Back,
            Direction.BackLeft => piece.BackLeft,
            Direction.Left => piece.Left,
            Direction.ForwardLeft => piece.ForwardLeft,
            _ => throw new InvalidEnumArgumentException(
                "Invalid enum passed to MaxMovementSquaresForDirection() function")
        };
    }

    private static (int rankMultiplier, int fileMultiplier) MovementMultipliersForDirection(Piece piece, Direction direction)
    {
        (int rankMultiplier, int fileMultiplier) multipliers = direction switch
        {
            Direction.Forward => (1, 0),
            Direction.ForwardRight => (1, 1),
            Direction.Right => (0, 1),
            Direction.BackRight => (-1, 1),
            Direction.Back => (-1, 0),
            Direction.BackLeft => (-1, -1),
            Direction.Left => (0, -1),
            Direction.ForwardLeft => (1, -1),
            _ => throw new InvalidEnumArgumentException(
                "Invalid direction parameter sent to MovementMultipliersForDirection")
        };

        return piece.Color == Color.Light
            ? multipliers
            : (-multipliers.rankMultiplier, -multipliers.fileMultiplier);
    }
}
