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
            int pawnRow = colorType == Enums.ColorType.Light ? Constants.PawnRankLight : Constants.PawnRankDark;
            int majorPieceRow = colorType == Enums.ColorType.Light ? Constants.BackRankLight : Constants.BackRankDark;

            // Add major pieces
            board.PlacePieceOnSquare(PieceFactory.GetRook(colorScheme, colorType), board.SquareAt(majorPieceRow, 1));
            board.PlacePieceOnSquare(PieceFactory.GetKnight(colorScheme, colorType), board.SquareAt(majorPieceRow, 2));
            board.PlacePieceOnSquare(PieceFactory.GetBishop(colorScheme, colorType), board.SquareAt(majorPieceRow, 3));
            board.PlacePieceOnSquare(PieceFactory.GetQueen(colorScheme, colorType), board.SquareAt(majorPieceRow, 4));
            board.PlacePieceOnSquare(PieceFactory.GetKing(colorScheme, colorType), board.SquareAt(majorPieceRow, 5));
            board.PlacePieceOnSquare(PieceFactory.GetBishop(colorScheme, colorType), board.SquareAt(majorPieceRow, 6));
            board.PlacePieceOnSquare(PieceFactory.GetKnight(colorScheme, colorType), board.SquareAt(majorPieceRow, 7));
            board.PlacePieceOnSquare(PieceFactory.GetRook(colorScheme, colorType), board.SquareAt(majorPieceRow, 8));

            // Add pawns
            board.PlacePieceOnSquare(PieceFactory.GetPawn(colorScheme, colorType), board.SquareAt(pawnRow, 1));
            board.PlacePieceOnSquare(PieceFactory.GetPawn(colorScheme, colorType), board.SquareAt(pawnRow, 2));
            board.PlacePieceOnSquare(PieceFactory.GetPawn(colorScheme, colorType), board.SquareAt(pawnRow, 3));
            board.PlacePieceOnSquare(PieceFactory.GetPawn(colorScheme, colorType), board.SquareAt(pawnRow, 4));
            board.PlacePieceOnSquare(PieceFactory.GetPawn(colorScheme, colorType), board.SquareAt(pawnRow, 5));
            board.PlacePieceOnSquare(PieceFactory.GetPawn(colorScheme, colorType), board.SquareAt(pawnRow, 6));
            board.PlacePieceOnSquare(PieceFactory.GetPawn(colorScheme, colorType), board.SquareAt(pawnRow, 7));
            board.PlacePieceOnSquare(PieceFactory.GetPawn(colorScheme, colorType), board.SquareAt(pawnRow, 8));
        }
    }
}