using MogriChess.Models;

using Constants = MogriChess.Models.Constants;

namespace MogriChess.Services
{
    public static class PieceFactory
    {
        internal static Piece GetBishop(Enums.PlayerColor color)
        {
            return new Piece(color, Enums.PieceType.Other,
                0, Constants.UnlimitedMoves,
                0, Constants.UnlimitedMoves,
                0, Constants.UnlimitedMoves,
                0, Constants.UnlimitedMoves);
        }

        internal static Piece GetKing(Enums.PlayerColor color)
        {
            return new Piece(color, Enums.PieceType.King, 1, 1, 1, 1, 1, 1, 1, 1);
        }

        internal static Piece GetKnight(Enums.PlayerColor color)
        {
            return new Piece(color, Enums.PieceType.Other, 2, 2, 2, 2, 2, 2, 2, 2);
        }

        internal static Piece GetPawn(Enums.PlayerColor color)
        {
            return new Piece(color, Enums.PieceType.Pawn, 1, 1, 0, 0, 0, 0, 0, 1);
        }

        internal static Piece GetQueen(Enums.PlayerColor color)
        {
            return new Piece(color, Enums.PieceType.Other,
                Constants.UnlimitedMoves, Constants.UnlimitedMoves,
                Constants.UnlimitedMoves, Constants.UnlimitedMoves,
                Constants.UnlimitedMoves, Constants.UnlimitedMoves,
                Constants.UnlimitedMoves, Constants.UnlimitedMoves);
        }

        internal static Piece GetRook(Enums.PlayerColor color)
        {
            return new Piece(color, Enums.PieceType.Other,
                Constants.UnlimitedMoves, 0,
                Constants.UnlimitedMoves, 0,
                Constants.UnlimitedMoves, 0,
                Constants.UnlimitedMoves, 0);
        }
    }
}