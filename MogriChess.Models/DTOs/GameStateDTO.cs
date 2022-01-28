using System.Collections.Generic;

namespace MogriChess.Models.DTOs;

public class GameStateDTO
{
    public ColorScheme BoardColorScheme { get; set; }
    public ColorScheme PieceColorScheme { get; set; }
    public Enums.Color CurrentPlayerColor { get; set; }
    public List<MoveHistoryDTO> MoveHistory { get; set; }
    public List<Square> Squares { get; set; } =
        new List<Square>();
}