using MogriChess.Models;

namespace MogriChess.Services
{
    public static class BoardFactory
    {
        public static Board GetNewGameBoard()
        {
            ColorScheme boardColorScheme = new ColorScheme("#DDDDDD", "#ADADAD");
            ColorScheme piecesColorScheme = new ColorScheme("#FFFFFF", "#000000");

            Board board = new Board(boardColorScheme, piecesColorScheme);

            PopulatePieces(board, piecesColorScheme, Enums.ColorType.Light);
            PopulatePieces(board, piecesColorScheme, Enums.ColorType.Dark);

            return board;
        }

        private static void PopulatePieces(Board board, ColorScheme colorScheme, Enums.ColorType colorType)
        {
            int pawnRow = colorType == Enums.ColorType.Light ? 2 : 7;
            int majorPieceRow = colorType == Enums.ColorType.Light ? 1 : 8;

            // Add major pieces
            board.SquareAt(majorPieceRow, 1).PlacePiece(PieceFactory.GetRook(colorScheme, colorType));
            board.SquareAt(majorPieceRow, 2).PlacePiece(PieceFactory.GetKnight(colorScheme, colorType));
            board.SquareAt(majorPieceRow, 3).PlacePiece(PieceFactory.GetBishop(colorScheme, colorType));
            board.SquareAt(majorPieceRow, 4).PlacePiece(PieceFactory.GetQueen(colorScheme, colorType));
            board.SquareAt(majorPieceRow, 5).PlacePiece(PieceFactory.GetKing(colorScheme, colorType));
            board.SquareAt(majorPieceRow, 6).PlacePiece(PieceFactory.GetBishop(colorScheme, colorType));
            board.SquareAt(majorPieceRow, 7).PlacePiece(PieceFactory.GetKnight(colorScheme, colorType));
            board.SquareAt(majorPieceRow, 8).PlacePiece(PieceFactory.GetRook(colorScheme, colorType));

            // Add pawns
            board.SquareAt(pawnRow, 1).PlacePiece(PieceFactory.GetPawn(colorScheme, colorType));
            board.SquareAt(pawnRow, 2).PlacePiece(PieceFactory.GetPawn(colorScheme, colorType));
            board.SquareAt(pawnRow, 3).PlacePiece(PieceFactory.GetPawn(colorScheme, colorType));
            board.SquareAt(pawnRow, 4).PlacePiece(PieceFactory.GetPawn(colorScheme, colorType));
            board.SquareAt(pawnRow, 5).PlacePiece(PieceFactory.GetPawn(colorScheme, colorType));
            board.SquareAt(pawnRow, 6).PlacePiece(PieceFactory.GetPawn(colorScheme, colorType));
            board.SquareAt(pawnRow, 7).PlacePiece(PieceFactory.GetPawn(colorScheme, colorType));
            board.SquareAt(pawnRow, 8).PlacePiece(PieceFactory.GetPawn(colorScheme, colorType));
        }
    }
}