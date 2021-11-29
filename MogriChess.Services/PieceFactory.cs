using MogriChess.Models;

using Constants = MogriChess.Models.Constants;

namespace MogriChess.Services
{
    public static class PieceFactory
    {
        public static Piece GetBishop(Enums.PlayerColor color)
        {
            return new Piece(color, Enums.PieceType.Other,
                0, Constants.UnlimitedMoves,
                0, Constants.UnlimitedMoves,
                0, Constants.UnlimitedMoves,
                0, Constants.UnlimitedMoves);
        }

        public static Piece GetKing(Enums.PlayerColor color)
        {
            return new Piece(color, Enums.PieceType.King, 1, 1, 1, 1, 1, 1, 1, 1);
        }

        public static Piece GetKnight(Enums.PlayerColor color)
        {
            return new Piece(color, Enums.PieceType.Other, 2, 2, 2, 2, 2, 2, 2, 2);
        }

        public static Piece GetPawn(Enums.PlayerColor color)
        {
            return new Piece(color, Enums.PieceType.Pawn, 1, 1, 0, 0, 0, 0, 0, 1);
        }

        public static Piece GetQueen(Enums.PlayerColor color)
        {
            return new Piece(color, Enums.PieceType.Other,
                Constants.UnlimitedMoves, Constants.UnlimitedMoves,
                Constants.UnlimitedMoves, Constants.UnlimitedMoves,
                Constants.UnlimitedMoves, Constants.UnlimitedMoves,
                Constants.UnlimitedMoves, Constants.UnlimitedMoves);
        }

        public static Piece GetRook(Enums.PlayerColor color)
        {
            return new Piece(color, Enums.PieceType.Other,
                Constants.UnlimitedMoves, 0,
                Constants.UnlimitedMoves, 0,
                Constants.UnlimitedMoves, 0,
                Constants.UnlimitedMoves, 0);
        }
    }
}