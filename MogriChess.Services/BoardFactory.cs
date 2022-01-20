﻿using System.Collections.Generic;
using MogriChess.Models;

namespace MogriChess.Services
{
    public static class BoardFactory
    {
        public static Board GetNewGameBoard()
        {
            ColorScheme boardColorScheme = new ColorScheme("#DDDDDD", "#ADADAD");
            ColorScheme piecesColorScheme = new ColorScheme("#FFFFFF", "#000000");

            var piecePlacements =
                GetStartingPiecePlacements(piecesColorScheme);

            return new Board(boardColorScheme, piecesColorScheme, piecePlacements);
        }

        private static List<PiecePlacement> GetStartingPiecePlacements(ColorScheme colorScheme)
        {
            List<PiecePlacement> placements =
                new List<PiecePlacement>();

            placements.Add(new PiecePlacement(Constants.BackRankLight, 1, PieceFactory.GetRook(colorScheme, Enums.ColorType.Light)));
            placements.Add(new PiecePlacement(Constants.BackRankLight, 2, PieceFactory.GetKnight(colorScheme, Enums.ColorType.Light)));
            placements.Add(new PiecePlacement(Constants.BackRankLight, 3, PieceFactory.GetBishop(colorScheme, Enums.ColorType.Light)));
            placements.Add(new PiecePlacement(Constants.BackRankLight, 4, PieceFactory.GetQueen(colorScheme, Enums.ColorType.Light)));
            placements.Add(new PiecePlacement(Constants.BackRankLight, 5, PieceFactory.GetKing(colorScheme, Enums.ColorType.Light)));
            placements.Add(new PiecePlacement(Constants.BackRankLight, 6, PieceFactory.GetBishop(colorScheme, Enums.ColorType.Light)));
            placements.Add(new PiecePlacement(Constants.BackRankLight, 7, PieceFactory.GetKnight(colorScheme, Enums.ColorType.Light)));
            placements.Add(new PiecePlacement(Constants.BackRankLight, 8, PieceFactory.GetRook(colorScheme, Enums.ColorType.Light)));

            placements.Add(new PiecePlacement(Constants.PawnRankLight, 1, PieceFactory.GetPawn(colorScheme, Enums.ColorType.Light)));
            placements.Add(new PiecePlacement(Constants.PawnRankLight, 2, PieceFactory.GetPawn(colorScheme, Enums.ColorType.Light)));
            placements.Add(new PiecePlacement(Constants.PawnRankLight, 3, PieceFactory.GetPawn(colorScheme, Enums.ColorType.Light)));
            placements.Add(new PiecePlacement(Constants.PawnRankLight, 4, PieceFactory.GetPawn(colorScheme, Enums.ColorType.Light)));
            placements.Add(new PiecePlacement(Constants.PawnRankLight, 5, PieceFactory.GetPawn(colorScheme, Enums.ColorType.Light)));
            placements.Add(new PiecePlacement(Constants.PawnRankLight, 6, PieceFactory.GetPawn(colorScheme, Enums.ColorType.Light)));
            placements.Add(new PiecePlacement(Constants.PawnRankLight, 7, PieceFactory.GetPawn(colorScheme, Enums.ColorType.Light)));
            placements.Add(new PiecePlacement(Constants.PawnRankLight, 8, PieceFactory.GetPawn(colorScheme, Enums.ColorType.Light)));

            placements.Add(new PiecePlacement(Constants.BackRankDark, 1, PieceFactory.GetRook(colorScheme, Enums.ColorType.Dark)));
            placements.Add(new PiecePlacement(Constants.BackRankDark, 2, PieceFactory.GetKnight(colorScheme, Enums.ColorType.Dark)));
            placements.Add(new PiecePlacement(Constants.BackRankDark, 3, PieceFactory.GetBishop(colorScheme, Enums.ColorType.Dark)));
            placements.Add(new PiecePlacement(Constants.BackRankDark, 4, PieceFactory.GetQueen(colorScheme, Enums.ColorType.Dark)));
            placements.Add(new PiecePlacement(Constants.BackRankDark, 5, PieceFactory.GetKing(colorScheme, Enums.ColorType.Dark)));
            placements.Add(new PiecePlacement(Constants.BackRankDark, 6, PieceFactory.GetBishop(colorScheme, Enums.ColorType.Dark)));
            placements.Add(new PiecePlacement(Constants.BackRankDark, 7, PieceFactory.GetKnight(colorScheme, Enums.ColorType.Dark)));
            placements.Add(new PiecePlacement(Constants.BackRankDark, 8, PieceFactory.GetRook(colorScheme, Enums.ColorType.Dark)));

            placements.Add(new PiecePlacement(Constants.PawnRankDark, 1, PieceFactory.GetPawn(colorScheme, Enums.ColorType.Dark)));
            placements.Add(new PiecePlacement(Constants.PawnRankDark, 2, PieceFactory.GetPawn(colorScheme, Enums.ColorType.Dark)));
            placements.Add(new PiecePlacement(Constants.PawnRankDark, 3, PieceFactory.GetPawn(colorScheme, Enums.ColorType.Dark)));
            placements.Add(new PiecePlacement(Constants.PawnRankDark, 4, PieceFactory.GetPawn(colorScheme, Enums.ColorType.Dark)));
            placements.Add(new PiecePlacement(Constants.PawnRankDark, 5, PieceFactory.GetPawn(colorScheme, Enums.ColorType.Dark)));
            placements.Add(new PiecePlacement(Constants.PawnRankDark, 6, PieceFactory.GetPawn(colorScheme, Enums.ColorType.Dark)));
            placements.Add(new PiecePlacement(Constants.PawnRankDark, 7, PieceFactory.GetPawn(colorScheme, Enums.ColorType.Dark)));
            placements.Add(new PiecePlacement(Constants.PawnRankDark, 8, PieceFactory.GetPawn(colorScheme, Enums.ColorType.Dark)));

            return placements;
        }
    }
}