﻿using MogriChess.Models;
using MogriChess.ViewModels;
using Xunit;

namespace Test.MogriChess.ViewModels;

public class TestPiecesAtStartup
{
    private const int FILE_A = 1;
    private const int FILE_B = 2;
    private const int FILE_C = 3;
    private const int FILE_D = 4;
    private const int FILE_E = 5;
    private const int FILE_F = 6;
    private const int FILE_G = 7;
    private const int FILE_H = 8;

    private static Piece GetPiece(Board board, int rank, int file) =>
        board.Squares[ModelFunctions.GetShorthand(rank, file)]?.Piece;

    #region Light - Major pieces

    [Fact]
    public void Test_ExpectedPiece_A1()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 1, FILE_A);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Light);
        Assert.False(piece.IsKing);

        Assert.Equal(Constants.UnlimitedMoves, piece.Forward);
        Assert.Equal(0, piece.ForwardRight);
        Assert.Equal(Constants.UnlimitedMoves, piece.Right);
        Assert.Equal(0, piece.BackRight);
        Assert.Equal(Constants.UnlimitedMoves, piece.Back);
        Assert.Equal(0, piece.BackLeft);
        Assert.Equal(Constants.UnlimitedMoves, piece.Left);
        Assert.Equal(0, piece.ForwardLeft);
    }

    [Fact]
    public void Test_ExpectedPiece_B1()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 1, FILE_B);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Light);
        Assert.False(piece.IsKing);

        Assert.Equal(2, piece.Forward);
        Assert.Equal(2, piece.ForwardRight);
        Assert.Equal(2, piece.Right);
        Assert.Equal(2, piece.BackRight);
        Assert.Equal(2, piece.Back);
        Assert.Equal(2, piece.BackLeft);
        Assert.Equal(2, piece.Left);
        Assert.Equal(2, piece.ForwardLeft);
    }

    [Fact]
    public void Test_ExpectedPiece_C1()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 1, FILE_C);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Light);
        Assert.False(piece.IsKing);

        Assert.Equal(0, piece.Forward);
        Assert.Equal(Constants.UnlimitedMoves, piece.ForwardRight);
        Assert.Equal(0, piece.Right);
        Assert.Equal(Constants.UnlimitedMoves, piece.BackRight);
        Assert.Equal(0, piece.Back);
        Assert.Equal(Constants.UnlimitedMoves, piece.BackLeft);
        Assert.Equal(0, piece.Left);
        Assert.Equal(Constants.UnlimitedMoves, piece.ForwardLeft);
    }

    [Fact]
    public void Test_ExpectedPiece_D1()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 1, FILE_D);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Light);
        Assert.False(piece.IsKing);

        Assert.Equal(Constants.UnlimitedMoves, piece.Forward);
        Assert.Equal(Constants.UnlimitedMoves, piece.ForwardRight);
        Assert.Equal(Constants.UnlimitedMoves, piece.Right);
        Assert.Equal(Constants.UnlimitedMoves, piece.BackRight);
        Assert.Equal(Constants.UnlimitedMoves, piece.Back);
        Assert.Equal(Constants.UnlimitedMoves, piece.BackLeft);
        Assert.Equal(Constants.UnlimitedMoves, piece.Left);
        Assert.Equal(Constants.UnlimitedMoves, piece.ForwardLeft);
    }

    [Fact]
    public void Test_ExpectedPiece_E1()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 1, FILE_E);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Light);
        Assert.True(piece.IsKing);

        Assert.Equal(1, piece.Forward);
        Assert.Equal(1, piece.ForwardRight);
        Assert.Equal(1, piece.Right);
        Assert.Equal(1, piece.BackRight);
        Assert.Equal(1, piece.Back);
        Assert.Equal(1, piece.BackLeft);
        Assert.Equal(1, piece.Left);
        Assert.Equal(1, piece.ForwardLeft);
    }

    [Fact]
    public void Test_ExpectedPiece_F1()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 1, FILE_F);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Light);
        Assert.False(piece.IsKing);

        Assert.Equal(0, piece.Forward);
        Assert.Equal(Constants.UnlimitedMoves, piece.ForwardRight);
        Assert.Equal(0, piece.Right);
        Assert.Equal(Constants.UnlimitedMoves, piece.BackRight);
        Assert.Equal(0, piece.Back);
        Assert.Equal(Constants.UnlimitedMoves, piece.BackLeft);
        Assert.Equal(0, piece.Left);
        Assert.Equal(Constants.UnlimitedMoves, piece.ForwardLeft);
    }

    [Fact]
    public void Test_ExpectedPiece_G1()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 1, FILE_G);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Light);
        Assert.False(piece.IsKing);

        Assert.Equal(2, piece.Forward);
        Assert.Equal(2, piece.ForwardRight);
        Assert.Equal(2, piece.Right);
        Assert.Equal(2, piece.BackRight);
        Assert.Equal(2, piece.Back);
        Assert.Equal(2, piece.BackLeft);
        Assert.Equal(2, piece.Left);
        Assert.Equal(2, piece.ForwardLeft);
    }

    [Fact]
    public void Test_ExpectedPiece_H1()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 1, FILE_H);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Light);
        Assert.False(piece.IsKing);

        Assert.Equal(Constants.UnlimitedMoves, piece.Forward);
        Assert.Equal(0, piece.ForwardRight);
        Assert.Equal(Constants.UnlimitedMoves, piece.Right);
        Assert.Equal(0, piece.BackRight);
        Assert.Equal(Constants.UnlimitedMoves, piece.Back);
        Assert.Equal(0, piece.BackLeft);
        Assert.Equal(Constants.UnlimitedMoves, piece.Left);
        Assert.Equal(0, piece.ForwardLeft);
    }

    #endregion

    #region Light - Pawns

    [Fact]
    public void Test_ExpectedPiece_A2()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 2, FILE_A);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Light);
        Assert.False(piece.IsKing);

        Assert.Equal(1, piece.Forward);
        Assert.Equal(1, piece.ForwardRight);
        Assert.Equal(0, piece.Right);
        Assert.Equal(0, piece.BackRight);
        Assert.Equal(0, piece.Back);
        Assert.Equal(0, piece.BackLeft);
        Assert.Equal(0, piece.Left);
        Assert.Equal(1, piece.ForwardLeft);
    }

    [Fact]
    public void Test_ExpectedPiece_B2()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 2, FILE_B);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Light);
        Assert.False(piece.IsKing);

        Assert.Equal(1, piece.Forward);
        Assert.Equal(1, piece.ForwardRight);
        Assert.Equal(0, piece.Right);
        Assert.Equal(0, piece.BackRight);
        Assert.Equal(0, piece.Back);
        Assert.Equal(0, piece.BackLeft);
        Assert.Equal(0, piece.Left);
        Assert.Equal(1, piece.ForwardLeft);
    }

    [Fact]
    public void Test_ExpectedPiece_C2()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 2, FILE_C);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Light);
        Assert.False(piece.IsKing);

        Assert.Equal(1, piece.Forward);
        Assert.Equal(1, piece.ForwardRight);
        Assert.Equal(0, piece.Right);
        Assert.Equal(0, piece.BackRight);
        Assert.Equal(0, piece.Back);
        Assert.Equal(0, piece.BackLeft);
        Assert.Equal(0, piece.Left);
        Assert.Equal(1, piece.ForwardLeft);
    }

    [Fact]
    public void Test_ExpectedPiece_D2()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 2, FILE_D);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Light);
        Assert.False(piece.IsKing);

        Assert.Equal(1, piece.Forward);
        Assert.Equal(1, piece.ForwardRight);
        Assert.Equal(0, piece.Right);
        Assert.Equal(0, piece.BackRight);
        Assert.Equal(0, piece.Back);
        Assert.Equal(0, piece.BackLeft);
        Assert.Equal(0, piece.Left);
        Assert.Equal(1, piece.ForwardLeft);
    }

    [Fact]
    public void Test_ExpectedPiece_E2()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 2, FILE_E);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Light);
        Assert.False(piece.IsKing);

        Assert.Equal(1, piece.Forward);
        Assert.Equal(1, piece.ForwardRight);
        Assert.Equal(0, piece.Right);
        Assert.Equal(0, piece.BackRight);
        Assert.Equal(0, piece.Back);
        Assert.Equal(0, piece.BackLeft);
        Assert.Equal(0, piece.Left);
        Assert.Equal(1, piece.ForwardLeft);
    }

    [Fact]
    public void Test_ExpectedPiece_F2()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 2, FILE_F);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Light);
        Assert.False(piece.IsKing);

        Assert.Equal(1, piece.Forward);
        Assert.Equal(1, piece.ForwardRight);
        Assert.Equal(0, piece.Right);
        Assert.Equal(0, piece.BackRight);
        Assert.Equal(0, piece.Back);
        Assert.Equal(0, piece.BackLeft);
        Assert.Equal(0, piece.Left);
        Assert.Equal(1, piece.ForwardLeft);
    }

    [Fact]
    public void Test_ExpectedPiece_G2()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 2, FILE_G);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Light);
        Assert.False(piece.IsKing);

        Assert.Equal(1, piece.Forward);
        Assert.Equal(1, piece.ForwardRight);
        Assert.Equal(0, piece.Right);
        Assert.Equal(0, piece.BackRight);
        Assert.Equal(0, piece.Back);
        Assert.Equal(0, piece.BackLeft);
        Assert.Equal(0, piece.Left);
        Assert.Equal(1, piece.ForwardLeft);
    }

    [Fact]
    public void Test_ExpectedPiece_H2()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 2, FILE_H);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Light);
        Assert.False(piece.IsKing);

        Assert.Equal(1, piece.Forward);
        Assert.Equal(1, piece.ForwardRight);
        Assert.Equal(0, piece.Right);
        Assert.Equal(0, piece.BackRight);
        Assert.Equal(0, piece.Back);
        Assert.Equal(0, piece.BackLeft);
        Assert.Equal(0, piece.Left);
        Assert.Equal(1, piece.ForwardLeft);
    }

    #endregion

    #region Empty squares

    [Fact]
    public void Test_ExpectedPieces_Rank_3()
    {
        Game game = new Game();
        game.StartGame();

        Assert.Null(GetPiece(game.Board, 3, FILE_A));
        Assert.Null(GetPiece(game.Board, 3, FILE_B));
        Assert.Null(GetPiece(game.Board, 3, FILE_C));
        Assert.Null(GetPiece(game.Board, 3, FILE_D));
        Assert.Null(GetPiece(game.Board, 3, FILE_E));
        Assert.Null(GetPiece(game.Board, 3, FILE_F));
        Assert.Null(GetPiece(game.Board, 3, FILE_G));
        Assert.Null(GetPiece(game.Board, 3, FILE_H));
    }

    [Fact]
    public void Test_ExpectedPieces_Rank_4()
    {
        Game game = new Game();
        game.StartGame();

        Assert.Null(GetPiece(game.Board, 4, FILE_A));
        Assert.Null(GetPiece(game.Board, 4, FILE_B));
        Assert.Null(GetPiece(game.Board, 4, FILE_C));
        Assert.Null(GetPiece(game.Board, 4, FILE_D));
        Assert.Null(GetPiece(game.Board, 4, FILE_E));
        Assert.Null(GetPiece(game.Board, 4, FILE_F));
        Assert.Null(GetPiece(game.Board, 4, FILE_G));
        Assert.Null(GetPiece(game.Board, 4, FILE_H));
    }

    [Fact]
    public void Test_ExpectedPieces_Rank_5()
    {
        Game game = new Game();
        game.StartGame();

        Assert.Null(GetPiece(game.Board, 5, FILE_A));
        Assert.Null(GetPiece(game.Board, 5, FILE_B));
        Assert.Null(GetPiece(game.Board, 5, FILE_C));
        Assert.Null(GetPiece(game.Board, 5, FILE_D));
        Assert.Null(GetPiece(game.Board, 5, FILE_E));
        Assert.Null(GetPiece(game.Board, 5, FILE_F));
        Assert.Null(GetPiece(game.Board, 5, FILE_G));
        Assert.Null(GetPiece(game.Board, 5, FILE_H));
    }

    [Fact]
    public void Test_ExpectedPieces_Rank_6()
    {
        Game game = new Game();
        game.StartGame();

        Assert.Null(GetPiece(game.Board, 6, FILE_A));
        Assert.Null(GetPiece(game.Board, 6, FILE_B));
        Assert.Null(GetPiece(game.Board, 6, FILE_C));
        Assert.Null(GetPiece(game.Board, 6, FILE_D));
        Assert.Null(GetPiece(game.Board, 6, FILE_E));
        Assert.Null(GetPiece(game.Board, 6, FILE_F));
        Assert.Null(GetPiece(game.Board, 6, FILE_G));
        Assert.Null(GetPiece(game.Board, 6, FILE_H));
    }

    #endregion

    #region Dark - Pawns

    [Fact]
    public void Test_ExpectedPiece_A7()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 7, FILE_A);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Dark);
        Assert.False(piece.IsKing);

        Assert.Equal(1, piece.Forward);
        Assert.Equal(1, piece.ForwardRight);
        Assert.Equal(0, piece.Right);
        Assert.Equal(0, piece.BackRight);
        Assert.Equal(0, piece.Back);
        Assert.Equal(0, piece.BackLeft);
        Assert.Equal(0, piece.Left);
        Assert.Equal(1, piece.ForwardLeft);
    }

    [Fact]
    public void Test_ExpectedPiece_B7()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 7, FILE_B);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Dark);
        Assert.False(piece.IsKing);

        Assert.Equal(1, piece.Forward);
        Assert.Equal(1, piece.ForwardRight);
        Assert.Equal(0, piece.Right);
        Assert.Equal(0, piece.BackRight);
        Assert.Equal(0, piece.Back);
        Assert.Equal(0, piece.BackLeft);
        Assert.Equal(0, piece.Left);
        Assert.Equal(1, piece.ForwardLeft);
    }

    [Fact]
    public void Test_ExpectedPiece_C7()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 7, FILE_C);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Dark);
        Assert.False(piece.IsKing);

        Assert.Equal(1, piece.Forward);
        Assert.Equal(1, piece.ForwardRight);
        Assert.Equal(0, piece.Right);
        Assert.Equal(0, piece.BackRight);
        Assert.Equal(0, piece.Back);
        Assert.Equal(0, piece.BackLeft);
        Assert.Equal(0, piece.Left);
        Assert.Equal(1, piece.ForwardLeft);
    }

    [Fact]
    public void Test_ExpectedPiece_D7()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 7, FILE_D);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Dark);
        Assert.False(piece.IsKing);

        Assert.Equal(1, piece.Forward);
        Assert.Equal(1, piece.ForwardRight);
        Assert.Equal(0, piece.Right);
        Assert.Equal(0, piece.BackRight);
        Assert.Equal(0, piece.Back);
        Assert.Equal(0, piece.BackLeft);
        Assert.Equal(0, piece.Left);
        Assert.Equal(1, piece.ForwardLeft);
    }

    [Fact]
    public void Test_ExpectedPiece_E7()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 7, FILE_E);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Dark);
        Assert.False(piece.IsKing);

        Assert.Equal(1, piece.Forward);
        Assert.Equal(1, piece.ForwardRight);
        Assert.Equal(0, piece.Right);
        Assert.Equal(0, piece.BackRight);
        Assert.Equal(0, piece.Back);
        Assert.Equal(0, piece.BackLeft);
        Assert.Equal(0, piece.Left);
        Assert.Equal(1, piece.ForwardLeft);
    }

    [Fact]
    public void Test_ExpectedPiece_F7()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 7, FILE_F);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Dark);
        Assert.False(piece.IsKing);

        Assert.Equal(1, piece.Forward);
        Assert.Equal(1, piece.ForwardRight);
        Assert.Equal(0, piece.Right);
        Assert.Equal(0, piece.BackRight);
        Assert.Equal(0, piece.Back);
        Assert.Equal(0, piece.BackLeft);
        Assert.Equal(0, piece.Left);
        Assert.Equal(1, piece.ForwardLeft);
    }

    [Fact]
    public void Test_ExpectedPiece_G7()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 7, FILE_G);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Dark);
        Assert.False(piece.IsKing);

        Assert.Equal(1, piece.Forward);
        Assert.Equal(1, piece.ForwardRight);
        Assert.Equal(0, piece.Right);
        Assert.Equal(0, piece.BackRight);
        Assert.Equal(0, piece.Back);
        Assert.Equal(0, piece.BackLeft);
        Assert.Equal(0, piece.Left);
        Assert.Equal(1, piece.ForwardLeft);
    }

    [Fact]
    public void Test_ExpectedPiece_H7()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 7, FILE_H);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Dark);
        Assert.False(piece.IsKing);

        Assert.Equal(1, piece.Forward);
        Assert.Equal(1, piece.ForwardRight);
        Assert.Equal(0, piece.Right);
        Assert.Equal(0, piece.BackRight);
        Assert.Equal(0, piece.Back);
        Assert.Equal(0, piece.BackLeft);
        Assert.Equal(0, piece.Left);
        Assert.Equal(1, piece.ForwardLeft);
    }

    #endregion

    #region Dark - Major pieces

    [Fact]
    public void Test_ExpectedPiece_A8()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 8, FILE_A);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Dark);
        Assert.False(piece.IsKing);

        Assert.Equal(Constants.UnlimitedMoves, piece.Forward);
        Assert.Equal(0, piece.ForwardRight);
        Assert.Equal(Constants.UnlimitedMoves, piece.Right);
        Assert.Equal(0, piece.BackRight);
        Assert.Equal(Constants.UnlimitedMoves, piece.Back);
        Assert.Equal(0, piece.BackLeft);
        Assert.Equal(Constants.UnlimitedMoves, piece.Left);
        Assert.Equal(0, piece.ForwardLeft);
    }

    [Fact]
    public void Test_ExpectedPiece_B8()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 8, FILE_B);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Dark);
        Assert.False(piece.IsKing);

        Assert.Equal(2, piece.Forward);
        Assert.Equal(2, piece.ForwardRight);
        Assert.Equal(2, piece.Right);
        Assert.Equal(2, piece.BackRight);
        Assert.Equal(2, piece.Back);
        Assert.Equal(2, piece.BackLeft);
        Assert.Equal(2, piece.Left);
        Assert.Equal(2, piece.ForwardLeft);
    }

    [Fact]
    public void Test_ExpectedPiece_C8()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 8, FILE_C);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Dark);
        Assert.False(piece.IsKing);

        Assert.Equal(0, piece.Forward);
        Assert.Equal(Constants.UnlimitedMoves, piece.ForwardRight);
        Assert.Equal(0, piece.Right);
        Assert.Equal(Constants.UnlimitedMoves, piece.BackRight);
        Assert.Equal(0, piece.Back);
        Assert.Equal(Constants.UnlimitedMoves, piece.BackLeft);
        Assert.Equal(0, piece.Left);
        Assert.Equal(Constants.UnlimitedMoves, piece.ForwardLeft);
    }

    [Fact]
    public void Test_ExpectedPiece_D8()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 8, FILE_D);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Dark);
        Assert.False(piece.IsKing);

        Assert.Equal(Constants.UnlimitedMoves, piece.Forward);
        Assert.Equal(Constants.UnlimitedMoves, piece.ForwardRight);
        Assert.Equal(Constants.UnlimitedMoves, piece.Right);
        Assert.Equal(Constants.UnlimitedMoves, piece.BackRight);
        Assert.Equal(Constants.UnlimitedMoves, piece.Back);
        Assert.Equal(Constants.UnlimitedMoves, piece.BackLeft);
        Assert.Equal(Constants.UnlimitedMoves, piece.Left);
        Assert.Equal(Constants.UnlimitedMoves, piece.ForwardLeft);
    }

    [Fact]
    public void Test_ExpectedPiece_E8()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 8, FILE_E);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Dark);
        Assert.True(piece.IsKing);

        Assert.Equal(1, piece.Forward);
        Assert.Equal(1, piece.ForwardRight);
        Assert.Equal(1, piece.Right);
        Assert.Equal(1, piece.BackRight);
        Assert.Equal(1, piece.Back);
        Assert.Equal(1, piece.BackLeft);
        Assert.Equal(1, piece.Left);
        Assert.Equal(1, piece.ForwardLeft);
    }

    [Fact]
    public void Test_ExpectedPiece_F8()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 8, FILE_F);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Dark);
        Assert.False(piece.IsKing);

        Assert.Equal(0, piece.Forward);
        Assert.Equal(Constants.UnlimitedMoves, piece.ForwardRight);
        Assert.Equal(0, piece.Right);
        Assert.Equal(Constants.UnlimitedMoves, piece.BackRight);
        Assert.Equal(0, piece.Back);
        Assert.Equal(Constants.UnlimitedMoves, piece.BackLeft);
        Assert.Equal(0, piece.Left);
        Assert.Equal(Constants.UnlimitedMoves, piece.ForwardLeft);
    }

    [Fact]
    public void Test_ExpectedPiece_G8()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 8, FILE_G);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Dark);
        Assert.False(piece.IsKing);

        Assert.Equal(2, piece.Forward);
        Assert.Equal(2, piece.ForwardRight);
        Assert.Equal(2, piece.Right);
        Assert.Equal(2, piece.BackRight);
        Assert.Equal(2, piece.Back);
        Assert.Equal(2, piece.BackLeft);
        Assert.Equal(2, piece.Left);
        Assert.Equal(2, piece.ForwardLeft);
    }

    [Fact]
    public void Test_ExpectedPiece_H8()
    {
        Game game = new Game();
        game.StartGame();

        var piece = GetPiece(game.Board, 8, FILE_H);

        Assert.NotNull(piece);

        Assert.True(piece.Color == Enums.Color.Dark);
        Assert.False(piece.IsKing);

        Assert.Equal(Constants.UnlimitedMoves, piece.Forward);
        Assert.Equal(0, piece.ForwardRight);
        Assert.Equal(Constants.UnlimitedMoves, piece.Right);
        Assert.Equal(0, piece.BackRight);
        Assert.Equal(Constants.UnlimitedMoves, piece.Back);
        Assert.Equal(0, piece.BackLeft);
        Assert.Equal(Constants.UnlimitedMoves, piece.Left);
        Assert.Equal(0, piece.ForwardLeft);
    }

    #endregion
}