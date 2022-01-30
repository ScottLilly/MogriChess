﻿using System.Collections.Generic;

namespace MogriChess.Models.DTOs;

public class GameStateDTO
{
    public ColorScheme BoardColorScheme { get; set; }
    public ColorScheme PieceColorScheme { get; set; }
    public string CurrentPlayerColor { get; set; }
    public List<MoveHistoryDTO> MoveHistory { get; set; } =
        new List<MoveHistoryDTO>();
    public List<SquareDTO> Squares { get; set; } =
        new List<SquareDTO>();
}