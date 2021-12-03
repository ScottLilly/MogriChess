using MogriChess.Models;

namespace MogriChess.Services
{
    public static class BoardFactory
    {
        private static readonly ColorScheme s_boardColorScheme = new ColorScheme
        {
            LightColor = "#DDDDDD",
            DarkColor = "#ADADAD"
        };

        private static readonly ColorScheme s_piecesColorScheme = new ColorScheme
        {
            LightColor = "#FFFFFF",
            DarkColor = "#000000"
        };

        public static Board GetNewGameBoard()
        {
            Board board = new Board(s_boardColorScheme, s_piecesColorScheme);

            PopulatePieces(board, s_piecesColorScheme.LightColor);
            PopulatePieces(board, s_piecesColorScheme.DarkColor);

            return board;
        }

        private static void PopulatePieces(Board board, string color)
        {
            int pawnRow = color.Equals(s_piecesColorScheme.LightColor) ? 2 : 7;
            int majorPieceRow = color.Equals(s_piecesColorScheme.LightColor) ? 1 : 8;

            Enums.ColorType colorEnum =
                color.Equals(s_piecesColorScheme.LightColor) ? Enums.ColorType.Light : Enums.ColorType.Dark;

            // Add major pieces
            board.SquareAt(majorPieceRow, 1).PlacePiece(PieceFactory.GetRook(s_piecesColorScheme, colorEnum));
            board.SquareAt(majorPieceRow, 2).PlacePiece(PieceFactory.GetKnight(s_piecesColorScheme, colorEnum));
            board.SquareAt(majorPieceRow, 3).PlacePiece(PieceFactory.GetBishop(s_piecesColorScheme, colorEnum));
            board.SquareAt(majorPieceRow, 4).PlacePiece(PieceFactory.GetQueen(s_piecesColorScheme, colorEnum));
            board.SquareAt(majorPieceRow, 5).PlacePiece(PieceFactory.GetKing(s_piecesColorScheme, colorEnum));
            board.SquareAt(majorPieceRow, 6).PlacePiece(PieceFactory.GetBishop(s_piecesColorScheme, colorEnum));
            board.SquareAt(majorPieceRow, 7).PlacePiece(PieceFactory.GetKnight(s_piecesColorScheme, colorEnum));
            board.SquareAt(majorPieceRow, 8).PlacePiece(PieceFactory.GetRook(s_piecesColorScheme, colorEnum));

            // Add pawns
            board.SquareAt(pawnRow, 1).PlacePiece(PieceFactory.GetPawn(s_piecesColorScheme, colorEnum));
            board.SquareAt(pawnRow, 2).PlacePiece(PieceFactory.GetPawn(s_piecesColorScheme, colorEnum));
            board.SquareAt(pawnRow, 3).PlacePiece(PieceFactory.GetPawn(s_piecesColorScheme, colorEnum));
            board.SquareAt(pawnRow, 4).PlacePiece(PieceFactory.GetPawn(s_piecesColorScheme, colorEnum));
            board.SquareAt(pawnRow, 5).PlacePiece(PieceFactory.GetPawn(s_piecesColorScheme, colorEnum));
            board.SquareAt(pawnRow, 6).PlacePiece(PieceFactory.GetPawn(s_piecesColorScheme, colorEnum));
            board.SquareAt(pawnRow, 7).PlacePiece(PieceFactory.GetPawn(s_piecesColorScheme, colorEnum));
            board.SquareAt(pawnRow, 8).PlacePiece(PieceFactory.GetPawn(s_piecesColorScheme, colorEnum));
        }
    }
}