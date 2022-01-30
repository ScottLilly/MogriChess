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
                game.Board.Squares.First(s => s.SquareShorthand.Equals(move.OriginationSquare));
            Square destinationSquare =
                game.Board.Squares.First(s => s.SquareShorthand.Equals(move.DestinationSquare));

            game.SelectSquare(originationSquare);
            game.SelectSquare(destinationSquare);
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

    private static void AddPiece(Game game, string squareShorthand, Piece piece)
    {
        game.Board.Squares.First(s => s.SquareShorthand.Equals(squareShorthand))
                .Piece = piece;
    }
}