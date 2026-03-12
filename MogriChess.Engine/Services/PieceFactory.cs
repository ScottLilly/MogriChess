using MogriChess.Engine.Models;
using Constants = MogriChess.Engine.Models.Constants;

namespace MogriChess.Engine.Services;

public static class PieceFactory
{
    public static Piece GetBishop(ColorScheme colorScheme, Color color)
    {
        return new Piece(colorScheme, color, PieceType.Other,
            0, Constants.UnlimitedMoves,
            0, Constants.UnlimitedMoves,
            0, Constants.UnlimitedMoves,
            0, Constants.UnlimitedMoves);
    }

    public static Piece GetKing(ColorScheme colorScheme, Color color)
    {
        return new Piece(colorScheme, color, PieceType.King, 1, 1, 1, 1, 1, 1, 1, 1);
    }

    public static Piece GetKnight(ColorScheme colorScheme, Color color)
    {
        return new Piece(colorScheme, color, PieceType.Other, 2, 2, 2, 2, 2, 2, 2, 2);
    }

    public static Piece GetPawn(ColorScheme colorScheme, Color color)
    {
        return new Piece(colorScheme, color, PieceType.Pawn, 1, 1, 0, 0, 0, 0, 0, 1);
    }

    public static Piece GetQueen(ColorScheme colorScheme, Color color)
    {
        return new Piece(colorScheme, color, PieceType.Other,
            Constants.UnlimitedMoves, Constants.UnlimitedMoves,
            Constants.UnlimitedMoves, Constants.UnlimitedMoves,
            Constants.UnlimitedMoves, Constants.UnlimitedMoves,
            Constants.UnlimitedMoves, Constants.UnlimitedMoves);
    }

    public static Piece GetRook(ColorScheme colorScheme, Color color)
    {
        return new Piece(colorScheme, color, PieceType.Other,
            Constants.UnlimitedMoves, 0,
            Constants.UnlimitedMoves, 0,
            Constants.UnlimitedMoves, 0,
            Constants.UnlimitedMoves, 0);
    }
}