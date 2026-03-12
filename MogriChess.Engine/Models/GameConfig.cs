namespace MogriChess.Engine.Models;

public class GameConfig
{
    public ColorScheme BoardColorScheme { get; set; }
    public ColorScheme PieceColorScheme { get; set; }
    public string SelectedSquareColor { get; set; }
    public string ValidDestinationSquareColor { get; set; }
}