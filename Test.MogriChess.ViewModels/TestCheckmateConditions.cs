using System.Collections.Generic;
using System.Linq;
using MogriChess.Models;
using MogriChess.Models.DTOs;
using MogriChess.Services;
using MogriChess.ViewModels;
using Test.MogriChess.ViewModels.Utilities;
using Xunit;

namespace Test.MogriChess.ViewModels;

public class TestCheckmateConditions
{
    [Fact]
    public void Test_IncorrectCheckmateReported_1()
    {
        // Test fix for a reported false checkmate
        // https://github.com/ScottLilly/MogriChess/issues/3

        PlaySession session = new PlaySession();
        session.StartGame();
        Game game = session.CurrentGame;

        List<MoveHistoryDTO> moves =
            TestFileParser.GetMoveHistoryFromFile(".\\MoveHistories\\BadCheckmate_1_MoveHistory.json");

        foreach (MoveHistoryDTO move in moves)
        {
            Square originationSquare =
                game.Board.Squares[move.OriginationSquare];
            Square destinationSquare =
                game.Board.Squares[move.DestinationSquare];

            session.SelectSquare(originationSquare);
            session.SelectSquare(destinationSquare);
        }

        Assert.False(game.MoveHistory.Last().PutsOpponentInCheckmate);
    }

    [Fact]
    public void Test_IncorrectCheckmateReported_2()
    {
        Game game = new Game(new Board(null, null));

        AddPiece(game, "a1", PieceFactory.GetRook(null, Enums.Color.Light));
        AddPiece(game, "b1", PieceFactory.GetKnight(null, Enums.Color.Light));
        AddPiece(game, "c1", PieceFactory.GetBishop(null, Enums.Color.Light));
        AddPiece(game, "d1", PieceFactory.GetQueen(null, Enums.Color.Light));
        AddPiece(game, "e1", PieceFactory.GetKing(null, Enums.Color.Light));
        AddPiece(game, "f1", PieceFactory.GetBishop(null, Enums.Color.Light));
        AddPiece(game, "g1", PieceFactory.GetKnight(null, Enums.Color.Light));

        AddPiece(game, "a2", PieceFactory.GetPawn(null, Enums.Color.Light));
        AddPiece(game, "b2", PieceFactory.GetPawn(null, Enums.Color.Light));
        AddPiece(game, "c2", PieceFactory.GetPawn(null, Enums.Color.Light));
        AddPiece(game, "d2", PieceFactory.GetPawn(null, Enums.Color.Light));
        AddPiece(game, "e2", PieceFactory.GetPawn(null, Enums.Color.Light));
        AddPiece(game, "f2", PieceFactory.GetPawn(null, Enums.Color.Light));
        AddPiece(game, "g2", PieceFactory.GetPawn(null, Enums.Color.Light));

        AddPiece(game, "b4", PieceFactory.GetQueen(null, Enums.Color.Light));
        AddPiece(game, "f6", PieceFactory.GetPawn(null, Enums.Color.Light));

        AddPiece(game, "f4", PieceFactory.GetKing(null, Enums.Color.Dark));
        AddPiece(game, "g6", PieceFactory.GetPawn(null, Enums.Color.Dark));

        Assert.True(game.Board.KingCanBeCaptured(Enums.Color.Dark));
        Assert.False(game.Board.PlayerIsInCheckmate(Enums.Color.Dark));
        Assert.False(game.Board.KingCanBeCaptured(Enums.Color.Light));
    }

    [Fact]
    public void Test_IncorrectCheckmateReported_3()
    {
        Game game = new Game(new Board(null, null));

        // Light pieces
        AddPiece(game, "a1", PieceFactory.GetRook(null, Enums.Color.Light));
        AddPiece(game, "b1", PieceFactory.GetKnight(null, Enums.Color.Light));
        AddPiece(game, "c1", PieceFactory.GetBishop(null, Enums.Color.Light));
        AddPiece(game, "d1", PieceFactory.GetQueen(null, Enums.Color.Light));
        AddPiece(game, "e1", PieceFactory.GetKing(null, Enums.Color.Light));
        AddPiece(game, "f1", PieceFactory.GetBishop(null, Enums.Color.Light));
        AddPiece(game, "g1", PieceFactory.GetKnight(null, Enums.Color.Light));

        AddPiece(game, "a2", PieceFactory.GetPawn(null, Enums.Color.Light));
        AddPiece(game, "b2", PieceFactory.GetPawn(null, Enums.Color.Light));
        AddPiece(game, "c2", PieceFactory.GetPawn(null, Enums.Color.Light));
        AddPiece(game, "d2", PieceFactory.GetPawn(null, Enums.Color.Light));
        AddPiece(game, "e2", PieceFactory.GetPawn(null, Enums.Color.Light));
        AddPiece(game, "f2", PieceFactory.GetPawn(null, Enums.Color.Light));
        AddPiece(game, "g2", PieceFactory.GetPawn(null, Enums.Color.Light));

        AddPiece(game, "h3", PieceFactory.GetRook(null, Enums.Color.Light));
        AddPiece(game, "f4", PieceFactory.GetQueen(null, Enums.Color.Light));

        // Dark pieces
        AddPiece(game, "a8", PieceFactory.GetRook(null, Enums.Color.Dark));
        AddPiece(game, "b8", PieceFactory.GetKnight(null, Enums.Color.Dark));
        AddPiece(game, "c8", PieceFactory.GetBishop(null, Enums.Color.Dark));
        AddPiece(game, "f8", PieceFactory.GetBishop(null, Enums.Color.Dark));
        AddPiece(game, "g8", PieceFactory.GetKnight(null, Enums.Color.Dark));
        AddPiece(game, "h8", PieceFactory.GetRook(null, Enums.Color.Dark));

        AddPiece(game, "f7", PieceFactory.GetKing(null, Enums.Color.Dark));

        AddPiece(game, "a7", PieceFactory.GetPawn(null, Enums.Color.Dark));
        AddPiece(game, "b7", PieceFactory.GetPawn(null, Enums.Color.Dark));
        AddPiece(game, "c5", PieceFactory.GetPawn(null, Enums.Color.Dark));
        AddPiece(game, "d7", PieceFactory.GetPawn(null, Enums.Color.Dark));
        AddPiece(game, "h7", PieceFactory.GetPawn(null, Enums.Color.Dark));

        Assert.True(game.Board.KingCanBeCaptured(Enums.Color.Dark));
        Assert.False(game.Board.PlayerIsInCheckmate(Enums.Color.Dark));
        Assert.False(game.Board.KingCanBeCaptured(Enums.Color.Light));
    }

    [Fact]
    public void Test_IncorrectCheckmateReported_4()
    {
        Game game = new Game(new Board(null, null));

        // Light pieces
        AddPiece(game, "d2", PieceFactory.GetKing(null, Enums.Color.Light));
        AddPiece(game, "e2", PieceFactory.GetKnight(null, Enums.Color.Light));
        AddPiece(game, "h1", PieceFactory.GetRook(null, Enums.Color.Light));
        AddPiece(game, "h2", PieceFactory.GetPawn(null, Enums.Color.Light));
        AddPiece(game, "e5", PieceFactory.GetPawn(null, Enums.Color.Light));

        // Dark pieces
        AddPiece(game, "a1", PieceFactory.GetQueen(null, Enums.Color.Dark));
        AddPiece(game, "g2", PieceFactory.GetQueen(null, Enums.Color.Dark));
        AddPiece(game, "b4", PieceFactory.GetKnight(null, Enums.Color.Dark));
        AddPiece(game, "g5", PieceFactory.GetKing(null, Enums.Color.Dark));
        AddPiece(game, "f7", PieceFactory.GetPawn(null, Enums.Color.Dark));
        AddPiece(game, "g7", PieceFactory.GetPawn(null, Enums.Color.Dark));
        AddPiece(game, "h7", PieceFactory.GetPawn(null, Enums.Color.Dark));
        AddPiece(game, "g8", PieceFactory.GetKnight(null, Enums.Color.Dark));
        AddPiece(game, "h8", PieceFactory.GetRook(null, Enums.Color.Dark));

        Assert.True(game.Board.KingCanBeCaptured(Enums.Color.Light));
        Assert.False(game.Board.PlayerIsInCheckmate(Enums.Color.Light));
        Assert.False(game.Board.KingCanBeCaptured(Enums.Color.Dark));
    }

    [Fact]
    public void Test_IncorrectCheckmateReported_5()
    {
        Game game = new Game(new Board(null, null));

        // Light pieces
        AddPiece(game, "f1", PieceFactory.GetRook(null, Enums.Color.Light));
        AddPiece(game, "h1", PieceFactory.GetKnight(null, Enums.Color.Light));
        AddPiece(game, "d2", PieceFactory.GetKing(null, Enums.Color.Light));

        var piece = new Piece(null, Enums.Color.Light, Enums.PieceType.Other,
            2, Constants.UnlimitedMoves, 2, Constants.UnlimitedMoves,
            2, Constants.UnlimitedMoves, 2, Constants.UnlimitedMoves);
        AddPiece(game, "e2", piece);

        AddPiece(game, "f2", PieceFactory.GetPawn(null, Enums.Color.Light));
        AddPiece(game, "h2", PieceFactory.GetPawn(null, Enums.Color.Light));
        AddPiece(game, "f3", PieceFactory.GetPawn(null, Enums.Color.Light));
        AddPiece(game, "c5", PieceFactory.GetPawn(null, Enums.Color.Light));

        // Dark pieces
        AddPiece(game, "a2", PieceFactory.GetQueen(null, Enums.Color.Dark));
        AddPiece(game, "g6", PieceFactory.GetKnight(null, Enums.Color.Dark));
        AddPiece(game, "a7", PieceFactory.GetPawn(null, Enums.Color.Dark));
        AddPiece(game, "b7", PieceFactory.GetPawn(null, Enums.Color.Dark));
        AddPiece(game, "c7", PieceFactory.GetPawn(null, Enums.Color.Dark));
        AddPiece(game, "h7", PieceFactory.GetPawn(null, Enums.Color.Dark));
        AddPiece(game, "a8", PieceFactory.GetRook(null, Enums.Color.Dark));
        AddPiece(game, "b8", PieceFactory.GetKing(null, Enums.Color.Dark));
        AddPiece(game, "e8", PieceFactory.GetQueen(null, Enums.Color.Dark));

        Assert.True(game.Board.KingCanBeCaptured(Enums.Color.Light));
        Assert.False(game.Board.PlayerIsInCheckmate(Enums.Color.Light));
        Assert.False(game.Board.KingCanBeCaptured(Enums.Color.Dark));
    }

    private static void AddPiece(Game game, string squareShorthand, Piece piece)
    {
        game.Board.Squares[squareShorthand].Piece = piece;
    }
}