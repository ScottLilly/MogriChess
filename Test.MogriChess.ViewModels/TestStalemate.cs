using System.Linq;
using MogriChess.Models;
using MogriChess.Services;
using Xunit;

namespace Test.MogriChess.ViewModels
{
    public class TestStalemate
    {
        [Fact]
        public void Test_Stalemate_1()
        {
            Game game = new Game(new Board(null, null));

            // Dark piece
            AddPiece(game, "a8", PieceFactory.GetKing(null, Enums.Color.Dark));

            // Light pieces
            AddPiece(game, "b6", PieceFactory.GetRook(null, Enums.Color.Light));
            AddPiece(game, "c7", PieceFactory.GetRook(null, Enums.Color.Light));

            Square square = game.Board.Squares.First(s => s.SquareShorthand == "a8");
            var moves =
                game.Board.LegalMovesForPieceAt(square.Rank, square.File);

            Assert.Empty(moves);
        }

        private static void AddPiece(Game game, string squareShorthand, Piece piece)
        {
            game.Board.Squares.First(s => s.SquareShorthand.Equals(squareShorthand))
                .Piece = piece;
        }
    }
}