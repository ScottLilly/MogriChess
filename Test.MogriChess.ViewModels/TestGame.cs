using System.Linq;
using MogriChess.Models;
using MogriChess.ViewModels;
using Xunit;

namespace Test.MogriChess.ViewModels;

public class TestGame
{
    [Fact]
    public void Test_InstantiateGame()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        Assert.NotNull(game);
        Assert.NotNull(game.Board);
        Assert.NotNull(game.Board.Squares);
            
        // Check board squares
        Assert.Equal(64, game.Board.Squares.Count);
        Assert.Equal(32, game.Board.Squares.Count(s => s.Color == Enums.Color.Light));
        Assert.Equal(32, game.Board.Squares.Count(s => s.Color == Enums.Color.Dark));
        Assert.True(game.Board.Squares.First(s => s.Rank == 1 && s.File == 1).Color == Enums.Color.Dark);
        Assert.True(game.Board.Squares.First(s => s.Rank == 1 && s.File == 2).Color == Enums.Color.Light);
        Assert.True(game.Board.Squares.First(s => s.Rank == 8 && s.File == 1).Color == Enums.Color.Light);
        Assert.True(game.Board.Squares.First(s => s.Rank == 8 && s.File == 2).Color == Enums.Color.Dark);

        // Check pieces
        Assert.Equal(16, game.Board.Squares.Count(s => s.Piece?.Color == Enums.Color.Light));
        Assert.Equal(16, game.Board.Squares.Count(s => s.Piece?.Color == Enums.Color.Dark));
        Assert.Equal(1, game.Board.Squares.Count(s => s.Piece?.Color == Enums.Color.Light && s.Piece.IsKing));
        Assert.Equal(1, game.Board.Squares.Count(s => s.Piece?.Color == Enums.Color.Dark && s.Piece.IsKing));
    }
}