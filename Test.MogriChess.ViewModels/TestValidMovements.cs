using System.Linq;
using MogriChess.Models;
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

        Assert.Empty(game.Board.LegalMovesForPieceAt(1, 1));
        Assert.Empty(game.Board.LegalMovesForPieceAt(1, 2));
        Assert.Empty(game.Board.LegalMovesForPieceAt(1, 3));
        Assert.Empty(game.Board.LegalMovesForPieceAt(1, 4));
        Assert.Empty(game.Board.LegalMovesForPieceAt(1, 5));
        Assert.Empty(game.Board.LegalMovesForPieceAt(1, 6));
        Assert.Empty(game.Board.LegalMovesForPieceAt(1, 7));
        Assert.Empty(game.Board.LegalMovesForPieceAt(1, 8));
    }

    #region Light pawns

    [Fact]
    public void Test_ValidMovesForPiece_A2()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.LegalMovesForPieceAt(2, 1).ToList();

        Assert.Equal(2, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 3 && d.DestinationFile == 1));
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 3 && d.DestinationFile == 2));
    }

    [Fact]
    public void Test_ValidMovesForPiece_B2()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.LegalMovesForPieceAt(2, 2).ToList();

        Assert.Equal(3, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 3 && d.DestinationFile == 1));
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 3 && d.DestinationFile == 2));
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 3 && d.DestinationFile == 3));
    }

    [Fact]
    public void Test_ValidMovesForPiece_C2()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.LegalMovesForPieceAt(2, 3).ToList();

        Assert.Equal(3, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 3 && d.DestinationFile == 2));
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 3 && d.DestinationFile == 3));
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 3 && d.DestinationFile == 4));
    }

    [Fact]
    public void Test_ValidMovesForPiece_D2()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.LegalMovesForPieceAt(2, 4).ToList();

        Assert.Equal(3, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 3 && d.DestinationFile == 3));
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 3 && d.DestinationFile == 4));
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 3 && d.DestinationFile == 5));
    }

    [Fact]
    public void Test_ValidMovesForPiece_E2()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.LegalMovesForPieceAt(2, 5).ToList();

        Assert.Equal(3, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 3 && d.DestinationFile == 4));
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 3 && d.DestinationFile == 5));
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 3 && d.DestinationFile == 6));
    }

    [Fact]
    public void Test_ValidMovesForPiece_F2()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.LegalMovesForPieceAt(2, 6).ToList();

        Assert.Equal(3, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 3 && d.DestinationFile == 5));
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 3 && d.DestinationFile == 6));
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 3 && d.DestinationFile == 7));
    }

    [Fact]
    public void Test_ValidMovesForPiece_G2()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.LegalMovesForPieceAt(2, 7).ToList();

        Assert.Equal(3, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 3 && d.DestinationFile == 6));
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 3 && d.DestinationFile == 7));
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 3 && d.DestinationFile == 8));
    }

    [Fact]
    public void Test_ValidMovesForPiece_H2()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.LegalMovesForPieceAt(2, 8).ToList();

        Assert.Equal(2, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 3 && d.DestinationFile == 7));
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 3 && d.DestinationFile == 8));
    }

    #endregion

    #region Dark pawns

    [Fact]
    public void Test_ValidMovesForPiece_A7()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.LegalMovesForPieceAt(7, 1).ToList();

        Assert.Equal(2, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 6 && d.DestinationFile == 1));
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 6 && d.DestinationFile == 2));
    }

    [Fact]
    public void Test_ValidMovesForPiece_B7()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.LegalMovesForPieceAt(7, 2).ToList();

        Assert.Equal(3, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 6 && d.DestinationFile == 1));
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 6 && d.DestinationFile == 2));
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 6 && d.DestinationFile == 3));
    }

    [Fact]
    public void Test_ValidMovesForPiece_C7()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.LegalMovesForPieceAt(7, 3).ToList();

        Assert.Equal(3, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 6 && d.DestinationFile == 2));
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 6 && d.DestinationFile == 3));
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 6 && d.DestinationFile == 4));
    }

    [Fact]
    public void Test_ValidMovesForPiece_D7()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.LegalMovesForPieceAt(7, 4).ToList();

        Assert.Equal(3, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 6 && d.DestinationFile == 3));
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 6 && d.DestinationFile == 4));
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 6 && d.DestinationFile == 5));
    }

    [Fact]
    public void Test_ValidMovesForPiece_E7()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.LegalMovesForPieceAt(7, 5).ToList();

        Assert.Equal(3, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 6 && d.DestinationFile == 4));
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 6 && d.DestinationFile == 5));
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 6 && d.DestinationFile == 6));
    }

    [Fact]
    public void Test_ValidMovesForPiece_F7()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.LegalMovesForPieceAt(7, 6).ToList();

        Assert.Equal(3, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 6 && d.DestinationFile == 5));
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 6 && d.DestinationFile == 6));
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 6 && d.DestinationFile == 7));
    }

    [Fact]
    public void Test_ValidMovesForPiece_G7()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.LegalMovesForPieceAt(7, 7).ToList();

        Assert.Equal(3, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 6 && d.DestinationFile == 6));
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 6 && d.DestinationFile == 7));
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 6 && d.DestinationFile == 8));
    }

    [Fact]
    public void Test_ValidMovesForPiece_H7()
    {
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        var validDestinations = game.Board.LegalMovesForPieceAt(7, 8).ToList();

        Assert.Equal(2, validDestinations.Count);
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 6 && d.DestinationFile == 7));
        Assert.True(validDestinations.Exists(d => d.DestinationRank == 6 && d.DestinationFile == 8));
    }

    #endregion

    [Fact]
    public void Test_ValidMoveForRank_8()
    {
        // Dark major pieces are blocked at game start
        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        Assert.Empty(game.Board.LegalMovesForPieceAt(8, 1));
        Assert.Empty(game.Board.LegalMovesForPieceAt(8, 2));
        Assert.Empty(game.Board.LegalMovesForPieceAt(8, 3));
        Assert.Empty(game.Board.LegalMovesForPieceAt(8, 4));
        Assert.Empty(game.Board.LegalMovesForPieceAt(8, 5));
        Assert.Empty(game.Board.LegalMovesForPieceAt(8, 6));
        Assert.Empty(game.Board.LegalMovesForPieceAt(8, 7));
        Assert.Empty(game.Board.LegalMovesForPieceAt(8, 8));
    }
}