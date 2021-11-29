using MogriChess.Models;

namespace MogriChess.Services
{
    public static class BoardFactory
    {
        private const string PIECE_COLOR_DARK = "#000000";
        private const string PIECE_COLOR_LIGHT = "#FFFFFF";

        public static Board GetNewGameBoard()
        {
            Board board = new Board();

            PopulatePieces(board, PIECE_COLOR_LIGHT);
            PopulatePieces(board, PIECE_COLOR_DARK);

            return board;
        }

        private static void PopulatePieces(Board board, string color)
        {
            int pawnRow = color.Equals(PIECE_COLOR_LIGHT) ? 2 : 7;
            int majorPieceRow = color.Equals(PIECE_COLOR_LIGHT) ? 1 : 8;

            Enums.PlayerColor colorEnum =
                color.Equals(PIECE_COLOR_LIGHT) ? Enums.PlayerColor.Light : Enums.PlayerColor.Dark;

            // Add major pieces
            board.SquareAt(majorPieceRow, 1).PlacePiece(PieceFactory.GetRook(colorEnum));
            board.SquareAt(majorPieceRow, 2).PlacePiece(PieceFactory.GetKnight(colorEnum));
            board.SquareAt(majorPieceRow, 3).PlacePiece(PieceFactory.GetBishop(colorEnum));
            board.SquareAt(majorPieceRow, 4).PlacePiece(PieceFactory.GetQueen(colorEnum));
            board.SquareAt(majorPieceRow, 5).PlacePiece(PieceFactory.GetKing(colorEnum));
            board.SquareAt(majorPieceRow, 6).PlacePiece(PieceFactory.GetBishop(colorEnum));
            board.SquareAt(majorPieceRow, 7).PlacePiece(PieceFactory.GetKnight(colorEnum));
            board.SquareAt(majorPieceRow, 8).PlacePiece(PieceFactory.GetRook(colorEnum));

            // Add pawns
            board.SquareAt(pawnRow, 1).PlacePiece(PieceFactory.GetPawn(colorEnum));
            board.SquareAt(pawnRow, 2).PlacePiece(PieceFactory.GetPawn(colorEnum));
            board.SquareAt(pawnRow, 3).PlacePiece(PieceFactory.GetPawn(colorEnum));
            board.SquareAt(pawnRow, 4).PlacePiece(PieceFactory.GetPawn(colorEnum));
            board.SquareAt(pawnRow, 5).PlacePiece(PieceFactory.GetPawn(colorEnum));
            board.SquareAt(pawnRow, 6).PlacePiece(PieceFactory.GetPawn(colorEnum));
            board.SquareAt(pawnRow, 7).PlacePiece(PieceFactory.GetPawn(colorEnum));
            board.SquareAt(pawnRow, 8).PlacePiece(PieceFactory.GetPawn(colorEnum));
        }
    }
}