using MogriChess.Engine.Models;
using MogriChess.Engine.Services;
using Xunit;

namespace Test.MogriChess.ViewModels;

public class TestBoardAndGameEngine
{
    [Fact]
    public void PawnPromotion_LightPawnReachingBackRankIsPromoted()
    {
        Board board = new Board(null, null);

        var origin = board.Squares["e7"];
        var destination = board.Squares["e8"];

        origin.Piece = PieceFactory.GetPawn(null, Color.Light);

        board.MovePiece(origin, destination);

        Assert.Null(origin.Piece);
        Assert.NotNull(destination.Piece);
        Assert.True(destination.Piece!.IsPromoted);

        Assert.True(destination.Piece.Forward >= 1);
        Assert.True(destination.Piece.ForwardRight >= 1);
        Assert.True(destination.Piece.Right >= 1);
        Assert.True(destination.Piece.BackRight >= 1);
        Assert.True(destination.Piece.Back >= 1);
        Assert.True(destination.Piece.BackLeft >= 1);
        Assert.True(destination.Piece.Left >= 1);
        Assert.True(destination.Piece.ForwardLeft >= 1);
    }

    [Fact]
    public void PawnPromotion_DarkPawnReachingBackRankIsPromoted()
    {
        Board board = new Board(null, null);

        var origin = board.Squares["e2"];
        var destination = board.Squares["e1"];

        origin.Piece = PieceFactory.GetPawn(null, Color.Dark);

        board.MovePiece(origin, destination);

        Assert.Null(origin.Piece);
        Assert.NotNull(destination.Piece);
        Assert.True(destination.Piece!.IsPromoted);
    }

    [Fact]
    public void CapturePiece_KingDoesNotGainCapturedPieceMovement()
    {
        Board board = new Board(null, null);

        var kingSquare = board.Squares["e1"];
        var rookSquare = board.Squares["e2"];

        var king = PieceFactory.GetKing(null, Color.Light);
        var rook = PieceFactory.GetRook(null, Color.Dark);

        kingSquare.Piece = king;
        rookSquare.Piece = rook;

        board.MovePiece(kingSquare, rookSquare);

        var resultingPiece = rookSquare.Piece;

        Assert.Same(king, resultingPiece);
        Assert.Equal(1, resultingPiece!.Forward);
        Assert.Equal(1, resultingPiece.Back);
        Assert.Equal(1, resultingPiece.Left);
        Assert.Equal(1, resultingPiece.Right);
    }

    [Fact]
    public void CapturePiece_NonKingCombinesMovementWithCapturedPiece()
    {
        Board board = new Board(null, null);

        var attackerSquare = board.Squares["e1"];
        var victimSquare = board.Squares["e2"];

        var bishop = PieceFactory.GetBishop(null, Color.Light);
        var rook = PieceFactory.GetRook(null, Color.Dark);

        attackerSquare.Piece = bishop;
        victimSquare.Piece = rook;

        board.MovePiece(attackerSquare, victimSquare);

        var resultingPiece = victimSquare.Piece;

        Assert.Null(attackerSquare.Piece);
        Assert.NotNull(resultingPiece);

        Assert.Equal(Constants.UnlimitedMoves, resultingPiece!.Forward);
        Assert.Equal(Constants.UnlimitedMoves, resultingPiece.ForwardRight);
        Assert.Equal(Constants.UnlimitedMoves, resultingPiece.Right);
        Assert.Equal(Constants.UnlimitedMoves, resultingPiece.BackRight);
        Assert.Equal(Constants.UnlimitedMoves, resultingPiece.Back);
        Assert.Equal(Constants.UnlimitedMoves, resultingPiece.BackLeft);
        Assert.Equal(Constants.UnlimitedMoves, resultingPiece.Left);
        Assert.Equal(Constants.UnlimitedMoves, resultingPiece.ForwardLeft);
    }

    [Fact]
    public void IsKingInCheck_ReturnsTrueWhenOpponentsMoveAttacksKing()
    {
        Board board = new Board(null, null);

        var lightKingSquare = board.Squares["e1"];
        var darkRookSquare = board.Squares["e8"];

        lightKingSquare.Piece = PieceFactory.GetKing(null, Color.Light);
        darkRookSquare.Piece = PieceFactory.GetRook(null, Color.Dark);

        Assert.True(board.IsKingInCheck(Color.Light));
        Assert.False(board.IsKingInCheck(Color.Dark));
    }
}

