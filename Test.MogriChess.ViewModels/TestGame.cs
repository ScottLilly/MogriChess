using System.Drawing;
using System.Linq;
using MogriChess.ViewModels;
using MogriChess.Models;


using Xunit;

namespace Test.MogriChess.ViewModels
{
    public class TestGame
    {
        [Fact]
        public void Test1()
        {
            Game game = new Game();

            Assert.NotNull(game);
            Assert.NotNull(game.Board);
            Assert.NotNull(game.Board.Squares);
            
            // Check board squares
            Assert.Equal(64, game.Board.Squares.Count);
            Assert.Equal(32, game.Board.Squares.Count(s => s.SquareColor.Equals(ColorTranslator.FromHtml(Board.COLOR_LIGHT))));
            Assert.Equal(32, game.Board.Squares.Count(s => s.SquareColor.Equals(ColorTranslator.FromHtml(Board.COLOR_DARK))));
            Assert.Equal(ColorTranslator.FromHtml(Board.COLOR_DARK), game.Board.Squares.First(s => s.Rank.Equals(1) && s.File.Equals(1)).SquareColor);
            Assert.Equal(ColorTranslator.FromHtml(Board.COLOR_LIGHT), game.Board.Squares.First(s => s.Rank.Equals(1) && s.File.Equals(2)).SquareColor);
            Assert.Equal(ColorTranslator.FromHtml(Board.COLOR_LIGHT), game.Board.Squares.First(s => s.Rank.Equals(8) && s.File.Equals(1)).SquareColor);
            Assert.Equal(ColorTranslator.FromHtml(Board.COLOR_DARK), game.Board.Squares.First(s => s.Rank.Equals(8) && s.File.Equals(2)).SquareColor);
        }
    }
}