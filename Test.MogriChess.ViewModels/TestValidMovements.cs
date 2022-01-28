using MogriChess.Models;
using MogriChess.Services;
using MogriChess.ViewModels;
using Xunit;

namespace Test.MogriChess.ViewModels;

public class TestValidMovements
{
    [Fact]
    public void Test_ValidMoveForRank_1()
    {
        // Light major pieces are blocked at game start
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        Assert.Empty(game.Board.PotentialMovesForPieceAt(1, 1));
        Assert.Empty(game.Board.PotentialMovesForPieceAt(1, 2));
        Assert.Empty(game.Board.PotentialMovesForPieceAt(1, 3));
        Assert.Empty(game.Board.PotentialMovesForPieceAt(1, 4));
        Assert.Empty(game.Board.PotentialMovesForPieceAt(1, 5));
        Assert.Empty(game.Board.PotentialMovesForPieceAt(1, 6));
        Assert.Empty(game.Board.PotentialMovesForPieceAt(1, 7));
        Assert.Empty(game.Board.PotentialMovesForPieceAt(1, 8));
    }

    #region Light pawns

    [Fact]
    public void Test_ValidMovesForPiece_A2()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.PotentialMovesForPieceAt(2, 1);

        Assert.Equal(2, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(3) && d.DestinationFile.Equals(1)));
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(3) && d.DestinationFile.Equals(2)));
    }

    [Fact]
    public void Test_ValidMovesForPiece_B2()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.PotentialMovesForPieceAt(2, 2);

        Assert.Equal(3, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(3) && d.DestinationFile.Equals(1)));
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(3) && d.DestinationFile.Equals(2)));
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(3) && d.DestinationFile.Equals(3)));
    }

    [Fact]
    public void Test_ValidMovesForPiece_C2()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.PotentialMovesForPieceAt(2, 3);

        Assert.Equal(3, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(3) && d.DestinationFile.Equals(2)));
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(3) && d.DestinationFile.Equals(3)));
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(3) && d.DestinationFile.Equals(4)));
    }

    [Fact]
    public void Test_ValidMovesForPiece_D2()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.PotentialMovesForPieceAt(2, 4);

        Assert.Equal(3, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(3) && d.DestinationFile.Equals(3)));
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(3) && d.DestinationFile.Equals(4)));
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(3) && d.DestinationFile.Equals(5)));
    }

    [Fact]
    public void Test_ValidMovesForPiece_E2()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.PotentialMovesForPieceAt(2, 5);

        Assert.Equal(3, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(3) && d.DestinationFile.Equals(4)));
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(3) && d.DestinationFile.Equals(5)));
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(3) && d.DestinationFile.Equals(6)));
    }

    [Fact]
    public void Test_ValidMovesForPiece_F2()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.PotentialMovesForPieceAt(2, 6);

        Assert.Equal(3, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(3) && d.DestinationFile.Equals(5)));
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(3) && d.DestinationFile.Equals(6)));
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(3) && d.DestinationFile.Equals(7)));
    }

    [Fact]
    public void Test_ValidMovesForPiece_G2()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.PotentialMovesForPieceAt(2, 7);

        Assert.Equal(3, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(3) && d.DestinationFile.Equals(6)));
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(3) && d.DestinationFile.Equals(7)));
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(3) && d.DestinationFile.Equals(8)));
    }

    [Fact]
    public void Test_ValidMovesForPiece_H2()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.PotentialMovesForPieceAt(2, 8);

        Assert.Equal(2, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(3) && d.DestinationFile.Equals(7)));
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(3) && d.DestinationFile.Equals(8)));
    }

    #endregion

    #region Dark pawns

    [Fact]
    public void Test_ValidMovesForPiece_A7()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.PotentialMovesForPieceAt(7, 1);

        Assert.Equal(2, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(6) && d.DestinationFile.Equals(1)));
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(6) && d.DestinationFile.Equals(2)));
    }

    [Fact]
    public void Test_ValidMovesForPiece_B7()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.PotentialMovesForPieceAt(7, 2);

        Assert.Equal(3, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(6) && d.DestinationFile.Equals(1)));
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(6) && d.DestinationFile.Equals(2)));
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(6) && d.DestinationFile.Equals(3)));
    }

    [Fact]
    public void Test_ValidMovesForPiece_C7()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.PotentialMovesForPieceAt(7, 3);

        Assert.Equal(3, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(6) && d.DestinationFile.Equals(2)));
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(6) && d.DestinationFile.Equals(3)));
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(6) && d.DestinationFile.Equals(4)));
    }

    [Fact]
    public void Test_ValidMovesForPiece_D7()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.PotentialMovesForPieceAt(7, 4);

        Assert.Equal(3, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(6) && d.DestinationFile.Equals(3)));
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(6) && d.DestinationFile.Equals(4)));
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(6) && d.DestinationFile.Equals(5)));
    }

    [Fact]
    public void Test_ValidMovesForPiece_E7()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.PotentialMovesForPieceAt(7, 5);

        Assert.Equal(3, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(6) && d.DestinationFile.Equals(4)));
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(6) && d.DestinationFile.Equals(5)));
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(6) && d.DestinationFile.Equals(6)));
    }

    [Fact]
    public void Test_ValidMovesForPiece_F7()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.PotentialMovesForPieceAt(7, 6);

        Assert.Equal(3, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(6) && d.DestinationFile.Equals(5)));
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(6) && d.DestinationFile.Equals(6)));
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(6) && d.DestinationFile.Equals(7)));
    }

    [Fact]
    public void Test_ValidMovesForPiece_G7()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.PotentialMovesForPieceAt(7, 7);

        Assert.Equal(3, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(6) && d.DestinationFile.Equals(6)));
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(6) && d.DestinationFile.Equals(7)));
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(6) && d.DestinationFile.Equals(8)));
    }

    [Fact]
    public void Test_ValidMovesForPiece_H7()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.PotentialMovesForPieceAt(7, 8);

        Assert.Equal(2, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(6) && d.DestinationFile.Equals(7)));
        Assert.True(validDestinations.Exists(d => d.DestinationRank.Equals(6) && d.DestinationFile.Equals(8)));
    }

    #endregion

    [Fact]
    public void Test_ValidMoveForRank_8()
    {
        // Dark major pieces are blocked at game start
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        Assert.Empty(game.Board.PotentialMovesForPieceAt(8, 1));
        Assert.Empty(game.Board.PotentialMovesForPieceAt(8, 2));
        Assert.Empty(game.Board.PotentialMovesForPieceAt(8, 3));
        Assert.Empty(game.Board.PotentialMovesForPieceAt(8, 4));
        Assert.Empty(game.Board.PotentialMovesForPieceAt(8, 5));
        Assert.Empty(game.Board.PotentialMovesForPieceAt(8, 6));
        Assert.Empty(game.Board.PotentialMovesForPieceAt(8, 7));
        Assert.Empty(game.Board.PotentialMovesForPieceAt(8, 8));
    }
}