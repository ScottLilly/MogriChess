using System.Linq;
using MogriChess.Models;
using MogriChess.Services;
using MogriChess.ViewModels;
using Xunit;

namespace Test.MogriChess.ViewModels
{
    public class TestGame
    {
        [Fact]
        public void Test_InstantiateGame()
        {
            PlaySession session = new PlaySession();
            session.SetBoardToStartingState();
            Game game = session.CurrentGame;

            Assert.NotNull(game);
            Assert.NotNull(game.Board);
            Assert.NotNull(game.Board.Squares);
            
            // Check board squares
            Assert.Equal(64, game.Board.Squares.Count);
            Assert.Equal(32, game.Board.Squares.Count(s => s.ColorType.Equals(Enums.ColorType.Light)));
            Assert.Equal(32, game.Board.Squares.Count(s => s.ColorType.Equals(Enums.ColorType.Dark)));
            Assert.True(game.Board.Squares.First(s => s.Rank.Equals(1) && s.File.Equals(1)).ColorType == Enums.ColorType.Dark);
            Assert.True(game.Board.Squares.First(s => s.Rank.Equals(1) && s.File.Equals(2)).ColorType == Enums.ColorType.Light);
            Assert.True(game.Board.Squares.First(s => s.Rank.Equals(8) && s.File.Equals(1)).ColorType == Enums.ColorType.Light);
            Assert.True(game.Board.Squares.First(s => s.Rank.Equals(8) && s.File.Equals(2)).ColorType == Enums.ColorType.Dark);

            // Check pieces
            Assert.Equal(16, game.Board.Squares.Count(s => s.Piece?.ColorType == Enums.ColorType.Light));
            Assert.Equal(16, game.Board.Squares.Count(s => s.Piece?.ColorType == Enums.ColorType.Dark));
            Assert.Equal(1, game.Board.Squares.Count(s => s.Piece?.ColorType == Enums.ColorType.Light && s.Piece.IsKing));
            Assert.Equal(1, game.Board.Squares.Count(s => s.Piece?.ColorType == Enums.ColorType.Dark && s.Piece.IsKing));
        }
    }
}