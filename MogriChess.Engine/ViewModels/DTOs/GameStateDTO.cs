using System.Collections.Generic;
using MogriChess.Engine.Models;

namespace MogriChess.Engine.ViewModels.DTOs;

public class GameStateDTO
{
    public ColorScheme BoardColorScheme { get; set; }
    public ColorScheme PieceColorScheme { get; set; }
    public string CurrentPlayerColor { get; set; }
    public List<MoveHistoryDTO> MoveHistory { get; set; } =
        [];
    public List<SquareDTO> Squares { get; set; } =
        [];
}