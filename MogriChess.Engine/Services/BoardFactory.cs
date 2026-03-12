using System;
using System.Collections.Generic;
using System.Linq;
using MogriChess.Engine.Models;

namespace MogriChess.Engine.Services;

public static class BoardFactory
{
    public static Board GetNewGameBoard(GameConfig gameConfig)
    {
        ColorScheme boardColorScheme =
            gameConfig?.BoardColorScheme ??
            Constants.ColorSchemes.First(c =>
                c.Name.Equals(Constants.DefaultBoardColorSchemeName, StringComparison.InvariantCultureIgnoreCase));
        ColorScheme piecesColorScheme =
            gameConfig?.PieceColorScheme ??
            new ColorScheme(Constants.DefaultPieceColorSchemeName,
                Constants.DefaultLightPieceColorHex,
                Constants.DefaultDarkPieceColorHex);

        return new Board(boardColorScheme, piecesColorScheme);
    }

    public static void PopulateBoardWithStartingPieces(Board board)
    {
        var startingPiecePlacements =
            GetStartingPiecePlacements(board.PieceColorScheme);

        PlaceStartingPieces(board, startingPiecePlacements);
    }

    private static void PlaceStartingPieces(Board board, List<PiecePlacement> piecePlacements)
    {
        foreach (Square square in board.Squares.Values)
        {
            square.Piece = null;
        }

        foreach (PiecePlacement placement in piecePlacements)
        {
            board.PlacePieceOnSquare(placement.Piece, board.Squares[placement.Shorthand]);
        }
    }

    private static List<PiecePlacement> GetStartingPiecePlacements(ColorScheme pieceColorScheme)
    {
        List<PiecePlacement> placements =
            [
                new PiecePlacement(Constants.BackRankLight, 1, PieceFactory.GetRook(pieceColorScheme, Enums.Color.Light)),
                new PiecePlacement(Constants.BackRankLight, 2, PieceFactory.GetKnight(pieceColorScheme, Enums.Color.Light)),
                new PiecePlacement(Constants.BackRankLight, 3, PieceFactory.GetBishop(pieceColorScheme, Enums.Color.Light)),
                new PiecePlacement(Constants.BackRankLight, 4, PieceFactory.GetQueen(pieceColorScheme, Enums.Color.Light)),
                new PiecePlacement(Constants.BackRankLight, 5, PieceFactory.GetKing(pieceColorScheme, Enums.Color.Light)),
                new PiecePlacement(Constants.BackRankLight, 6, PieceFactory.GetBishop(pieceColorScheme, Enums.Color.Light)),
                new PiecePlacement(Constants.BackRankLight, 7, PieceFactory.GetKnight(pieceColorScheme, Enums.Color.Light)),
                new PiecePlacement(Constants.BackRankLight, 8, PieceFactory.GetRook(pieceColorScheme, Enums.Color.Light)),
                new PiecePlacement(Constants.PawnRankLight, 1, PieceFactory.GetPawn(pieceColorScheme, Enums.Color.Light)),
                new PiecePlacement(Constants.PawnRankLight, 2, PieceFactory.GetPawn(pieceColorScheme, Enums.Color.Light)),
                new PiecePlacement(Constants.PawnRankLight, 3, PieceFactory.GetPawn(pieceColorScheme, Enums.Color.Light)),
                new PiecePlacement(Constants.PawnRankLight, 4, PieceFactory.GetPawn(pieceColorScheme, Enums.Color.Light)),
                new PiecePlacement(Constants.PawnRankLight, 5, PieceFactory.GetPawn(pieceColorScheme, Enums.Color.Light)),
                new PiecePlacement(Constants.PawnRankLight, 6, PieceFactory.GetPawn(pieceColorScheme, Enums.Color.Light)),
                new PiecePlacement(Constants.PawnRankLight, 7, PieceFactory.GetPawn(pieceColorScheme, Enums.Color.Light)),
                new PiecePlacement(Constants.PawnRankLight, 8, PieceFactory.GetPawn(pieceColorScheme, Enums.Color.Light)),
                new PiecePlacement(Constants.BackRankDark, 1, PieceFactory.GetRook(pieceColorScheme, Enums.Color.Dark)),
                new PiecePlacement(Constants.BackRankDark, 2, PieceFactory.GetKnight(pieceColorScheme, Enums.Color.Dark)),
                new PiecePlacement(Constants.BackRankDark, 3, PieceFactory.GetBishop(pieceColorScheme, Enums.Color.Dark)),
                new PiecePlacement(Constants.BackRankDark, 4, PieceFactory.GetQueen(pieceColorScheme, Enums.Color.Dark)),
                new PiecePlacement(Constants.BackRankDark, 5, PieceFactory.GetKing(pieceColorScheme, Enums.Color.Dark)),
                new PiecePlacement(Constants.BackRankDark, 6, PieceFactory.GetBishop(pieceColorScheme, Enums.Color.Dark)),
                new PiecePlacement(Constants.BackRankDark, 7, PieceFactory.GetKnight(pieceColorScheme, Enums.Color.Dark)),
                new PiecePlacement(Constants.BackRankDark, 8, PieceFactory.GetRook(pieceColorScheme, Enums.Color.Dark)),
                new PiecePlacement(Constants.PawnRankDark, 1, PieceFactory.GetPawn(pieceColorScheme, Enums.Color.Dark)),
                new PiecePlacement(Constants.PawnRankDark, 2, PieceFactory.GetPawn(pieceColorScheme, Enums.Color.Dark)),
                new PiecePlacement(Constants.PawnRankDark, 3, PieceFactory.GetPawn(pieceColorScheme, Enums.Color.Dark)),
                new PiecePlacement(Constants.PawnRankDark, 4, PieceFactory.GetPawn(pieceColorScheme, Enums.Color.Dark)),
                new PiecePlacement(Constants.PawnRankDark, 5, PieceFactory.GetPawn(pieceColorScheme, Enums.Color.Dark)),
                new PiecePlacement(Constants.PawnRankDark, 6, PieceFactory.GetPawn(pieceColorScheme, Enums.Color.Dark)),
                new PiecePlacement(Constants.PawnRankDark, 7, PieceFactory.GetPawn(pieceColorScheme, Enums.Color.Dark)),
                new PiecePlacement(Constants.PawnRankDark, 8, PieceFactory.GetPawn(pieceColorScheme, Enums.Color.Dark)),
            ];

        return placements;
    }
}